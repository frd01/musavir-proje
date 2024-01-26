using DataIslemler.Models;
using ExcelDataReader;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Tacmin.Core;
using Tacmin.Core.Extensions;
using Tacmin.Core.Interface;
using Tacmin.Data;
using Tacmin.Data.DbModel;
using Tacmin.Data.Repositories;
using Tacmin.Model;
using Tacmin.Model.Maliye;
using Tacmin.Model.Mukellef;

namespace Tacmin.Service
{
    public class MukellefService
    {
        private readonly MainContext _ctx;
        private readonly MainRepository<FIRMA_TANIMLARI> _firmaService;
        private readonly IVirtualContext _vctx;
        private readonly MainRepository<FIRMA_BAGLANTILARI> _firmaBagService;
        private readonly MainRepository<FIRMA_SUBE_TANIMLARI> _firmaSubeService;
        private DataIslemler.Mukellef.ExcelIslem excelIslem;
        private IExcelDataReader excelReader;
        private DataIslemler.Mukellef.ListeIslem listeIslem;
        private DataIslemler.Mukellef.DataIslem dataIslem;

        public MukellefService(
            MainContext ctx,
            MainRepository<FIRMA_TANIMLARI> firmaService,
            IVirtualContext vctx,
            MainRepository<FIRMA_BAGLANTILARI> firmaBagService,
            MainRepository<FIRMA_SUBE_TANIMLARI> firmaSubeService
            
            )
        {
            _ctx = ctx;
            _firmaService = firmaService;
            _vctx = vctx;
            _firmaBagService = firmaBagService;
            _firmaSubeService = firmaSubeService;
            excelIslem = new DataIslemler.Mukellef.ExcelIslem(_vctx.DataAdi);
            listeIslem = new DataIslemler.Mukellef.ListeIslem(vctx.DataAdi);
            dataIslem = new DataIslemler.Mukellef.DataIslem(vctx.DataAdi);
           
        }

        public DataSourceResult GetMukellefList(DataSourceRequest req)
        {
            var sql = $@"
                SELECT firma.*
                FROM FIRMA_BAGLANTILARI bag
                INNER JOIN FIRMA_TANIMLARI firma ON bag.FIRMA_ID = firma.ID
				
                WHERE 1 = 1
                    AND bag.USER_ID = {_vctx.UserId}
            ";

            var result = _ctx.Database.SqlQuery<FIRMA_TANIMLARI>(sql).ToDataSourceResult(req, x => Engine.Instance.AutoMap.Map<MukellefAraModel>(x));

            return result;
        }

        public MukellefKayitModel GetMukellef(int? id)
        {
            var entity = _firmaBagService.Get(x => x.FIRMA_ID == id && x.USER_ID == _vctx.UserId);
            var model = new MukellefKayitModel();

            if (entity != null)
                model = Engine.Instance.AutoMap.Map<MukellefKayitModel>(entity.FIRMA);

            return model;
        }

        public int AddMukellef(MukellefKayitModel model)
        {
            var firmaentity = Engine.Instance.AutoMap.Map<FIRMA_TANIMLARI>(model);

            var firmabag = new FIRMA_BAGLANTILARI
            {
                USER_ID = _vctx.UserId,
                FIRMA = firmaentity
            };

            _firmaBagService.Add(firmabag);
            _ctx.SaveChanges();

            return firmabag.FIRMA.ID;
        }

        public void UpdateMukellef(MukellefKayitModel model)
        {
            var firmabag = _firmaBagService.Get(x => x.FIRMA_ID == model.ID && x.USER_ID == _vctx.UserId);
            if (firmabag != null)
            {
                Engine.Instance.AutoMap.Map(model, firmabag.FIRMA);

                _firmaBagService.Update(firmabag);
                _ctx.SaveChanges();
            }
        }

        public void DestroyMukellef(int id)
        {
            var firmabag = _firmaBagService.Get(x => x.FIRMA_ID == id && x.USER_ID == _vctx.UserId);

            if (firmabag != null)
            {
                _firmaService.Delete(x => x.ID == id);
                _ctx.SaveChanges();
            }
        }

        public DataSourceResult FirmaFaaliyetKodlari(DataSourceRequest req, int? firmaId)
        {
            var res = _ctx.FIRMA_FAALIYET_KODLARI.Where(x => x.FIRMA_ID == firmaId)
                .ToDataSourceResult(req, x => Engine.Instance.AutoMap.Map<FaaliyetKoduAraModel>(x));

            return res;
        }

        public DataSourceResult FirmaSubeTanimlari(DataSourceRequest req, int? firmaId)
        {
            var res = new List<FirmaSubeKayitModel>().ToDataSourceResult(req);
            if (firmaId != null)
            {
                var firmabag = _firmaBagService.Get(x => x.FIRMA_ID == firmaId && x.USER_ID == _vctx.UserId);

                if (firmabag != null && firmabag.FIRMA.SUBE_TANIMLARI != null)
                {
                    res = firmabag.FIRMA.SUBE_TANIMLARI.ToDataSourceResult(req, x => Engine.Instance.AutoMap.Map<FirmaSubeKayitModel>(x));
                }
            }

            return res;
        }

        public int? MukellefSubeAdd(FirmaSubeKayitModel model)
        {
            var firmabag = _firmaBagService.Get(x => x.FIRMA_ID == model.FIRMA_ID && x.USER_ID == _vctx.UserId);
            if (firmabag != null)
            {
                var subeentity = Engine.Instance.AutoMap.Map<FIRMA_SUBE_TANIMLARI>(model);
                subeentity.FIRMA = firmabag.FIRMA;
                _firmaSubeService.Add(subeentity);

                _ctx.SaveChanges();
                model.ID = subeentity.ID;
            }

            return model.ID;
        }

        public void MukellefSubeUpdate(FirmaSubeKayitModel model)
        {
            var firmabag = _firmaBagService.Get(x => x.FIRMA_ID == model.FIRMA_ID && x.USER_ID == _vctx.UserId);
            if (firmabag != null)
            {
                var subeentity = firmabag.FIRMA.SUBE_TANIMLARI.FirstOrDefault(x => x.ID == model.ID);
                Engine.Instance.AutoMap.Map(model, subeentity);
                _firmaSubeService.Update(subeentity);

                _ctx.SaveChanges();
            }
        }

        public void MukellefSubeDestroy(FirmaSubeKayitModel model)
        {
            var firmabag = _firmaBagService.Get(x => x.FIRMA_ID == model.FIRMA_ID && x.USER_ID == _vctx.UserId);
            if (firmabag != null)
            {
                var subeentity = firmabag.FIRMA.SUBE_TANIMLARI.FirstOrDefault(x => x.ID == model.ID);
                _firmaSubeService.Delete(subeentity);

                _ctx.SaveChanges();
            }
        }

        public MaliyeBilgiResponse MaliyeBilgiGuncelleme(string token)
        {

            var islem_durum = new MaliyeBilgiResponse();
            islem_durum.IslemDurum = true;

            try
            {

                var data_str = "cmd=EBynYetkiIslemleri_sozlesmeListele&callid=93dd807eb6a01-15&token=" + token + "&jp=%7B%22VERGINO%22%3A%226550145747%22%2C%22mukellefListe%22%3A%220%22%7D";
                var service_url = "https://intvrg.gib.gov.tr/intvrg_server/dispatch";

                var response = GibRequestService.PostJsonRequest(service_url, data_str);

                var result = JsonConvert.DeserializeObject<MaliyeBilgiModel>(response);

                new DataIslemler.Islemler.Mukellef.MaliyeBilgiGuncelleme(_vctx.DataAdi).FirmaBilgiGuncelle(result);

                islem_durum.IslemDurum = true;
            }
            catch
            {

                islem_durum.IslemDurum = false;

            }

            return islem_durum;
        }

        public List<Tacmin.Data.DbModel.Firma_Iletisim> FirmaIletisimListesi(int firmaId)
        {

            var islem = new DataIslemler.Listeler.Mukellef.Listeler(_vctx.DataAdi);

            return islem.FirmaIletisimListesi(firmaId);
        }

        public List<BankaHacizModel> BankaHacizListeVergiDairesiGuncelle(List<BankaHacizModel> clientList)
        {

            var islem = new DataIslemler.Maliye.ListeIslem(_vctx.DataAdi);

            return islem.BankaHacizListeVergiDairesiGuncelle(clientList);
        }

        public List<AracHacizModel> AracHacizListeGuncelle(List<AracHacizModel> clientList)
        {

            var islem = new DataIslemler.Maliye.ListeIslem(_vctx.DataAdi);

            return islem.AracHacizListeGuncelle(clientList);


        }

        public GibExcelModel MukellefGibGuncellemeExcelListesi()
        {
            var model = new GibExcelModel();

            model.FirmaListesi = new DataIslemler.LocalData.ListeIslem(_vctx.DataAdi).LocalListeler().FirmaListesi;
            model.DosyaAdi = _vctx.AdSoyad + ".xlsx";

            return model;
        }

        public KayitResModel MukellefGibBilgiGuncelleme(Stream excelFile)
        {

           

            var counter = 0;

            excelReader = ExcelReaderFactory.CreateBinaryReader(excelFile);

            var liste = new List<Model.LocalData.FirmaModel>();

            while (excelReader.Read())
            {
                counter++;

                if (counter>1)
                {
                    if (excelReader.IsDBNull(0))
                        break;

                    var model = new Model.LocalData.FirmaModel();

                    model.Id = Convert.ToInt32(excelReader.GetDouble(0));
                    model.Unvan = ExcelGetString(1);
                    model.GibKodu = ExcelGetString(2);
                    model.GibParola = ExcelGetString(3);
                    model.GibSifre = ExcelGetString(4);

                    liste.Add(model);
                }
            }

            var result = excelIslem.MukellefGibBilgileriGuncelle(liste);

            return result;


        }

        private string ExcelGetString(int satirNo)
        {
            var value = "";

            if (!excelReader.IsDBNull(satirNo))
            {
                if (excelReader.GetFieldType(satirNo).Name == "String")
                {
                    value = excelReader.GetString(satirNo);
                }
                if (excelReader.GetFieldType(satirNo).Name != "String")
                {
                    value = excelReader.GetDouble(satirNo).ToString();
                }
            }

            return value;
        }

        public List<SgkListeModel> MusteriSgkSubeListesi(int? firmaId)
        {
            var result = listeIslem.MusteriSgkSubeListesi(firmaId); 

            return result;
        }

        public List<BildirgeTakipModel> BilgiTakipList(int? subeId)
        {
            var result = listeIslem.BilgiTakipList(subeId);

            return result;
        }

        public KayitResModel MukellefSgkSubeKaydet(SgkListeModel clientData, int? firmaId)
        {
            var result = dataIslem.MukellefSgkSubeKaydet(clientData, firmaId);
            return result;
        }
    }
}
