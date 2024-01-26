using DataIslemler.Islemler.Mukellef;
using DataIslemler.Models;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tacmin.Core.Extensions;
using Tacmin.Core.Interface;
using Tacmin.Data;
using Tacmin.Data.DbModel;
using Tacmin.Data.Repositories;
using Tacmin.Service.VergiBaglantilar;

namespace Tacmin.Service.Maliye
{
    public class VergiLevhaService
    {
        GibService gibService;

        private string servisUrl = "https://ivd.gib.gov.tr/tvd_server/dispatch";
        private readonly IVirtualContext vctx;
        private readonly MainContext ctx;
        private readonly MainRepository<FIRMA_TANIMLARI> firmaService;
        private readonly MainRepository<Vergi_Levhalari> vergiLevhaService;
      
       

        public VergiLevhaService(IVirtualContext vctx, MainContext ctx, MainRepository<FIRMA_TANIMLARI> firmaService, MainRepository<Vergi_Levhalari> vergiLevhaService)
        {

            this.vctx = vctx;
            this.ctx = ctx;
            this.firmaService = firmaService;
            this.vergiLevhaService = vergiLevhaService;
           
        }

        public void VergiLevhasiIslem(DataIslemler.Models.Mukellef.VergiLevhaRequestModel fatura_bilgi)
        {
            gibService = new GibService();

            var icerikListesi = new List<Vergi_Levhalari>();

            var index = 1;
            foreach (var firma in fatura_bilgi.FirmaListesi)
            {
                if (firma.GibKodu.Length > 0)
                {
                    var token = getToken(firma.GibKodu, firma.GibSifre, firma.GibParola);

                    Parallel.For(0, index, x =>
                    {
                        var onayKodu = VergiLevhasiOlustur(token).data.ceEVergiLevhaOnayKoduTx;
                        if (onayKodu.Length > 0)
                        {
                            var icerik = VergiLevhasiIndir(onayKodu, "kirmizi", token);
                            if (icerik != null)
                            {
                                icerik.FirmaId = firma.Id;
                                icerikListesi.Add(icerik);
                            }
                        }
                    });
                }
                index++;
            }

            new VergiLevhaIslem(vctx.DataAdi).DataKayit(icerikListesi);


        }

        private Vergi_Levhalari VergiLevhasiIndir(string onayKodu, string renk, string token)
        {

            var icerik = new Vergi_Levhalari();

            var levha_url = "https://ivd.gib.gov.tr/tvd_server/goruntuleme?cmd=IMAJ&subcmd=IVD_VRG_LVH_GORUNTULE&onayKodu=" + onayKodu + "&vrgLvhBoyut=buyuk&vrgLvhRenk=" + renk + "&goruntuTip=2&token=" + token;

            var file_levha = new byte[] { };
            var sayac = 0;

            while (file_levha.Length < 50 && sayac < 10)
            {
                file_levha = GetFile(levha_url);
                sayac += 1;
                Task.Delay(3000);
            }

            icerik.Dosya = file_levha;

            var levhaReader = new PdfReader(icerik.Dosya);
            var levhaStr = new StringBuilder();

            for (var i = 1; i <= levhaReader.NumberOfPages; i++)
            {
                levhaStr.Append(PdfTextExtractor.GetTextFromPage(levhaReader, i));
            }

            var levhaTxt = levhaStr.ToString().Replace("\n", "");

            var takvim_yil = levhaTxt.SafeSubstring(levhaTxt.IndexOf("TAKVİM YILI") + 71, 5);
            var web_adr_index = levhaTxt.IndexOf("https://ivd.gib.gov.tr");
            var takvim_index = levhaTxt.IndexOf("TAKVİM YILI");
            var beyan_index = takvim_index + 76;
            var beyan_uzunluk = web_adr_index - beyan_index;

            var beyan_vergi = levhaTxt.SafeSubstring(beyan_index, beyan_uzunluk);

            var onay_index = levhaTxt.IndexOf("ONAY KODU");

            var tah_vergi = levhaTxt.SafeSubstring(onay_index + 9, (beyan_index - 14) - onay_index);

            icerik.Yil = Convert.ToInt32(takvim_yil);
            icerik.Beyan = beyan_vergi;
            icerik.Matrah = tah_vergi;


            return icerik;


        }

        private byte[] GetFile(string url)
        {
            var memStream = new MemoryStream();
            var stream = GibRequestService.GetResponseStream(url);
            stream.CopyTo(memStream, 16384);
            return memStream.ToArray();
        }

        private string getToken(string gibKodu, string gibSifre, string gibParalo)
        {

            var token = this.gibService.GibGiris(gibKodu, gibSifre, gibParalo);

            return token.Token;
        }

        private DataOnayModel VergiLevhasiOlustur(string token)
        {
            var model = new DataOnayModel();
            model.data.ceEVergiLevhaOnayKoduTx = "";

            try
            {
                var data_str = "cmd=vergiLevhasiDetay_olustur&callid=81dd4a39f92ef-26&pageName=P_EVERGI_LEVHA&token=" + token + "&jp=%7B%22islemTip%22%3A0%7D";
                var response = GibRequestService.PostJsonRequest(servisUrl, data_str);
                var result = JsonConvert.DeserializeObject<DataOnayModel>(response);

                model.data.ceEVergiLevhaOnayKoduTx = result.data.ceEVergiLevhaOnayKoduTx;
            }
            catch
            {

                model.data.ceEVergiLevhaOnayKoduTx = "";
            }

            return model;
        }

        public DataSourceResult GetVergiLevhaListesi<T>(DataSourceRequest req)
        {

            var sql = $@"
                SELECT
                V.Id,
                F.UNVAN,
                F.VERGI_DAIRESI,
                F.VERGI_NO,
                V.Yil,
                V.Beyan,
                V.Matrah
                FROM Vergi_Levhalari V
                INNER JOIN FIRMA_TANIMLARI F ON F.ID = V.FirmaId
                INNER JOIN FIRMA_BAGLANTILARI B ON B.FIRMA_ID = F.ID
                WHERE 1 = 1
                    AND B.USER_ID = {vctx.UserId}
                
            ";


            var result = ctx.Database.SqlQuery<T>(sql).ToDataSourceResult(req, x => x);

            return result;
        }

        public (byte[] file, string fileName) LevhaPdfGor(int? id)
        {
            var levha = vergiLevhaService.Get(x => x.Id == id);
            if (levha != null)
            {
                /*var unzipfile = ZipUtils.DecompressZlib(levha.Dosya);
                return (unzipfile, "Vergi Levhası");*/

                return (levha.Dosya, "Vergi Levhası");

            }

            return (new byte[] { }, "");
        }


    }

    public class DataOnayModel
    {
        public DataOnayModel()
        {
            this.data = new OnayModel();
        }
        public OnayModel data { get; set; }
    }
    public class OnayModel
    {
        public string ceEVergiLevhaOnayKoduTx { get; set; }
    }
}
