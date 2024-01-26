using DataIslemler.Islemler.Bildirge;
using DataIslemler.Models;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tacmin.Core.Extensions;
using Tacmin.Core.Interface;
using Tacmin.Core.Utilities;
using Tacmin.Data;
using Tacmin.Data.DbModel;
using Tacmin.Data.Repositories;
using Tacmin.Model;
using Tacmin.Model.Maliye;
using Tacmin.Model.MaliyeIslemleri.Beyanname;
using Tacmin.Model.Ortak;
using Tacmin.Model.Whatsapp;

namespace Tacmin.Service 
{
    public class BeyannameService
    {
        private static readonly string GIB_API_URL = "https://intvrg.gib.gov.tr/intvrg_server";
        private readonly string GIB_REQUEST_URL = $"{GIB_API_URL}/dispatch";

        //private readonly MainRepository<BEYANNAME_ICERIKLERI> _beyanService;
        //private readonly MainRepository<DOSYA> _dosyaService;
        //private readonly MainContext _ctx;
        //private readonly MainRepository<BILDIRGE_ICERIKLERI> _bildirgeService;
        //private readonly MainRepository<FIRMA_TANIMLARI> _firmaService;
        //private readonly MainRepository<FIRMA_BAGLANTILARI> _firmaBaglantiService;
        private readonly IVirtualContext _vctx;
        private DataIslemler.Islemler.Mukellef.MukellefIslem mukDataIslem;

        private DataIslem bildirgeIslem;
        private DataIslemler.Islemler.Mukellef.DataIslem mukellefDataIslem;

        private DataIslemler.Islemler.Beyanname.VergiDairesiIslem vergi_daires_islem;
        private DataIslemler.Islemler.Beyanname.DataIslem beyannameIslem;      
        private BeyannameOzetModel beyannameOzet;
        private ServiceIslem service;

       

        public BeyannameOzetModel BeyannameOzet
        {
            get
            {
                return beyannameOzet;
            }
        }

        public BeyannameService(
            //MainRepository<BEYANNAME_ICERIKLERI> beyanService,
            //MainRepository<DOSYA> dosyaService,
            //MainContext ctx,
            //MainRepository<BILDIRGE_ICERIKLERI> bildirgeService,
            //MainRepository<FIRMA_TANIMLARI> firmaService,
            //MainRepository<FIRMA_BAGLANTILARI> firmaBaglantiService,
            IVirtualContext vctx
            
            )
        {
            //_beyanService = beyanService;
            //_dosyaService = dosyaService;
            //_ctx = ctx;
            //_bildirgeService = bildirgeService;
            //_firmaService = firmaService;
            //_firmaBaglantiService = firmaBaglantiService;
            _vctx = vctx;
            
            bildirgeIslem = new DataIslem(_vctx.DataAdi);

            vergi_daires_islem = new DataIslemler.Islemler.Beyanname.VergiDairesiIslem(_vctx.DataAdi);

            mukDataIslem = new DataIslemler.Islemler.Mukellef.MukellefIslem(_vctx.DataAdi);
            beyannameIslem = new DataIslemler.Islemler.Beyanname.DataIslem(_vctx.DataAdi);
            mukellefDataIslem = new DataIslemler.Islemler.Mukellef.DataIslem(_vctx.DataAdi);

            beyannameOzet = new BeyannameOzetModel();
            beyannameOzet.BeyannameSayisi = 0;
            beyannameOzet.BeyannameTahakkuhSayisi = 0;
            beyannameOzet.BildirgeSayisi = 0;
            beyannameOzet.BildirgeTahakkuhSayisi = 0;
            beyannameOzet.MukellefSayisi = 0;
            beyannameOzet.IslemDurum = true;
            beyannameOzet.Mesaj = "";
        } 

        private string BeyannameRequestData(string token, string arsiv, string sayfaNo, BeyannameRequestModel client_veri,string ilkTarih,string sonTarih)
        {

            if (client_veri.Is_Vergi_No == true)
            {

                if (client_veri.Vergi_No.Length == 10)
                    client_veri.VergiTanim = "1";
                else
                    client_veri.TcTanim = "1";
            }

            if (client_veri.Is_Vergi_Tur == true)
                client_veri.BeyannameTurTanim = "1";

            if (client_veri.Is_Vergi_Dairesi == true)
                client_veri.VergiDairesiTanim = "1";

            var data = "cmd=beyannameService_beyannameAra&" +
                      "callid=1fb75eb799465-19&" +
                      "token=" + token + "&" +
                      "jp=%7B%22arsivde%22%3A" + arsiv + "%2C%22" +
                      "sorguTipiN%22%3A" + client_veri.VergiTanim.ToString() + "%2C%22" +
                      "vergiNo%22%3A%22" + client_veri.Vergi_No + "%22%2C%22" +
                      "sorguTipiT%22%3A" + client_veri.TcTanim.ToString() + "%2C%22" +
                      "tcKimlikNo%22%3A%22" + client_veri.Vergi_No + "%22%2C%22" +
                      "sorguTipiB%22%3A" + client_veri.BeyannameTurTanim.ToString() + "%2C%22" +
                      "beyannameTanim%22%3A%22" + client_veri.BeyannameKodu + "%22%2C%22" +
                      "sorguTipiP%22%3A0%2C%22" +
                      "donemBasAy%22%3A%2207%22%2C%22" +
                      "donemBasYil%22%3A%222018%22%2C%22" +
                      "donemBitAy%22%3A%2207%22%2C%22" +
                      "donemBitYil%22%3A%222018%22%2C%22" +
                      "sorguTipiV%22%3A" + client_veri.VergiDairesiTanim + "%2C%22" +
                      "vdKodu%22%3A%22" + client_veri.Vergi_Daire_Kodu + "%22%2C%22" +
                      "sorguTipiZ%22%3A1%2C%22" +
                      "tarihAraligi%22%3A%7B%22" +
                      "baslangicTarihi%22%3A%22" + ilkTarih + "%22%2C%22" +
                      "bitisTarihi%22%3A%22" + sonTarih + "%22%7D%2C%22" +
                      "sorguTipiD%22%3A1%2C%22" +
                      "durum%22%3A%7B%22" +
                      "radiob%22%3Afalse%2C%22" +
                      "radiob1%22%3Afalse%2C%22" +
                      "radiob2%22%3Atrue%2C%22" +
                      "radiob3%22%3Afalse%7D%2C%22" +
                      "pageNo%22%3A" + sayfaNo + "%7D";
            return data;
        }

        public int GibBeyannameSorgula(string gibToken, BeyannameRequestModel client_veri,bool is_aktif_cari, ServiceIslem _service,string ilkTarih,string sonTarih)
        {
            service = _service;

            try
            {
                var beyannameler = new List<string>();

                var beyannameIceriks = new List<BeyannameIcerik>();
                var rootBeyanname = new BeyannameResultModel();

                var arsiv = "false";
                /*if (ilkTarih.Year - DateTime.Now.Year > 1)
                {
                    arsiv = "true";
                }*/

                var SayfaSayisi = 0;
                var toplamBeyannameAdedi = 0;

                var data = BeyannameRequestData(gibToken, arsiv, "1", client_veri,ilkTarih,sonTarih);

                Console.WriteLine(data);

                var response = service.PostJsonRequest(GIB_REQUEST_URL, data);
                rootBeyanname = JsonConvert.DeserializeObject<BeyannameResultModel>(response);
                Console.WriteLine(rootBeyanname.Data);

                if (rootBeyanname.Data == null)
                    return 0;

                SayfaSayisi = (int)Math.Ceiling(Convert.ToDecimal(rootBeyanname.Data.Rowcount) / 25);
                toplamBeyannameAdedi = rootBeyanname.Data.Rowcount;

                Parallel.For(1, SayfaSayisi + 1, sayfa =>
                {
                    data = BeyannameRequestData(gibToken, arsiv, sayfa.ToString(), client_veri,ilkTarih,sonTarih);
                    response = service.PostJsonRequest(GIB_REQUEST_URL, data);
                    rootBeyanname = JsonConvert.DeserializeObject<BeyannameResultModel>(response);

                    if (rootBeyanname.Data != null)
                    {
                        Parallel.ForEach(rootBeyanname.Data.Data, item =>
                        {

                            if (beyannameIslem.BeyannameKontrol(item.BeyannameOid, is_aktif_cari, item.Unvan) <= 0)
                            {
                                var pdfBilgileri = BeyannamePdfleriIndir(gibToken, item.BeyannameOid, item.TahakkukOid, item.BeyannameKodu);

                                beyannameIceriks.Add(new BeyannameIcerik
                                {
                                    Unvan = item.Unvan,
                                    Subeno = item.Subeno,
                                    Donem = item.Donem,
                                    BeyannameKodu = item.BeyannameKodu,
                                    BeyannameOid = item.BeyannameOid,
                                    BeyannameTuru = item.BeyannameTuru,
                                    Durum = item.Durum,
                                    Ihbarnamekesildi = item.Ihbarnamekesildi,
                                    Mesajvar = item.Mesajvar,
                                    Onaylanabilir = item.Onaylanabilir,
                                    TahakkukOid = item.TahakkukOid,
                                    Tckn = item.Tckn,
                                    VergiDairesi = item.VergiDairesi,
                                    YuklenmeTarihi = pdfBilgileri.YuklenmeTarihi,
                                    beyfile = pdfBilgileri.beyfile,
                                    Vade = pdfBilgileri.Vade,
                                    tahfile = pdfBilgileri.tahfile,
                                    Tutar = pdfBilgileri.Tutar,
                                    fisNo = pdfBilgileri.fisNo
                                });
                            }

                        });
                    }
                });

                foreach (var dosyasiInmeyenBeyanname in beyannameIceriks.Where(x => x.beyfile.Length < 50 || x.tahfile.Length < 50).ToList())
                {
                    var index = beyannameIceriks.FindIndex(x => x.BeyannameOid == dosyasiInmeyenBeyanname.BeyannameOid && x.TahakkukOid == dosyasiInmeyenBeyanname.TahakkukOid && x.BeyannameKodu == dosyasiInmeyenBeyanname.BeyannameKodu);
                    var pdfBilgileri = BeyannamePdfleriIndir(gibToken, dosyasiInmeyenBeyanname.BeyannameOid, dosyasiInmeyenBeyanname.TahakkukOid, dosyasiInmeyenBeyanname.BeyannameKodu);
                    beyannameIceriks[index] = pdfBilgileri;
                }

                var eklenenVerginolar = new List<string>();
                foreach (var beyanname in beyannameIceriks)
                {
                    DOSYA beyanDosya = null;
                    DOSYA tahakDosya = null;

                    if (beyanname.beyfile != null && beyanname.beyfile.Length > 50)
                    {
                        beyanDosya = new DOSYA();
                        beyanDosya.DOSYA_ADI = $"{beyanname.Tckn}-{beyanname.BeyannameKodu}-{beyanname.Donem.Replace("/", "")}".SafeSubstring(96) + ".pdf";
                        beyanDosya.SIZE = beyanname.beyfile.Length;
                        beyanDosya.ICERIK = ZipUtils.CompressZlib(beyanname.beyfile);
                        beyanDosya.MIME_TYPE = "application/pdf";
                    }

                    if (beyanname.tahfile != null && beyanname.tahfile.Length > 50)
                    {
                        tahakDosya = new DOSYA();
                        tahakDosya.DOSYA_ADI = $"{beyanname.Tckn}-{beyanname.BeyannameKodu}-{beyanname.Donem.Replace("/", "")}".SafeSubstring(96) + ".pdf";
                        tahakDosya.SIZE = beyanname.tahfile.Length;
                        tahakDosya.ICERIK = ZipUtils.CompressZlib(beyanname.tahfile);
                        tahakDosya.MIME_TYPE = "application/pdf";
                    }



                    var firmaBaglantiKontrol = mukDataIslem.MukellefKontrol(beyanname.Tckn);

                    beyanname.VergiDairesi = vergi_daires_islem.GetVergiDairesi(beyanname.VergiDairesi);

                    if (firmaBaglantiKontrol == null)
                    {
                        var firmaTanim = new FIRMA_TANIMLARI
                        {
                            AKTIF = true,
                            UNVAN = beyanname.Unvan,
                            VERGI_DAIRESI = beyanname.VergiDairesi
                        };

                        if (beyanname.Tckn.Length == 11)
                            firmaTanim.TCKN = beyanname.Tckn;
                        else if (beyanname.Tckn.Length == 10)
                            firmaTanim.VERGI_NO = beyanname.Tckn;

                        if (firmaTanim.TCKN != null || firmaTanim.VERGI_NO != null)
                        {
                            firmaBaglantiKontrol = mukellefDataIslem.BeyannameYeniMukellefKayit(firmaTanim);
                        }

                        //_ctx.SaveChanges();

                        beyannameler.Add(beyanname.BeyannameOid);
                    }
                    try
                    {
                        beyannameIslem.BeyannameKayit(beyanname, beyanDosya, tahakDosya, firmaBaglantiKontrol);
                    }
                    catch (Exception ex)
                    {
                        var result = beyanname;
                        Console.WriteLine(ex.Message);
                    }
                }

                var toplamBildirgeAdedi = beyannameIceriks.Count(x => x.BeyannameKodu == "MUHSGK");

                var bildirgeIcerikleris = new List<EBildirgeIcerik>();

                Parallel.ForEach(beyannameIceriks.Where(x => x.BeyannameKodu == "MUHSGK"), beyan =>
                {
                    data = "cmd=sgkBildirgeIslemleri_bildirgeleriGetir&" +
                          "callid=293ffb6622f53-14&" +
                          "token=" + gibToken + "&" +
                          "jp=%7B%22" +
                          "beyannameOid%22%3A%22" + beyan.BeyannameOid + "%22%7D";
                    response = service.PostJsonRequest(GIB_REQUEST_URL, data);
                    var rootBildirge = JsonConvert.DeserializeObject<dynamic>(response);

                    if (((string)rootBildirge.data.bildirim_sayisi.Value).ToInt() > 1)
                    {
                        for (var ebild = 1; ebild <= ((string)rootBildirge.data.bildirim_sayisi.Value).ToInt(); ebild++)
                        {
                            if ((rootBildirge.data["thkhaberlesme" + ebild]).aciklama.Value == "Vergi")
                                continue;

                            var bildirge = new EBildirgeIcerik
                            {
                                MuhSgkBeyOid = beyan.BeyannameOid,
                                Tckn = beyan.Tckn,
                                Unvan = beyan.Unvan
                            };

                            var bilgiler = (string)(rootBildirge.data["thkhaberlesme" + ebild]).aciklama.Value;
                            var bilgilerArr = bilgiler.Split(",");
                            bildirge.Durum = bilgilerArr[0];
                            bildirge.BelgeCesidi = bilgilerArr[1];
                            bildirge.Kanun = bilgilerArr[2].Split('-')[0].Trim();
                            bildirge.SicilNo = bilgilerArr[4];
                            bildirge.MuhSgkTahOid = (rootBildirge.data["thkhaberlesme" + ebild]).thkoid.Value;

                            var donemStr = bilgilerArr[3].Replace(" ", "");
                            var donem = new DateTime(Convert.ToInt32(donemStr.SafeSubstring(3, 4)), Convert.ToInt32(donemStr.SafeSubstring(0, 2)), 1);
                            bildirge.Donem = donem;

                            var vadeTarih = donem.AddMonths(2);
                            bildirge.Vade = vadeTarih;

                            var pdfBilgileri = BildirgePdfleriIndir(gibToken, bildirge.MuhSgkBeyOid, bildirge.MuhSgkTahOid);
                            bildirge.tahfile = pdfBilgileri.tahfile;
                            bildirge.beyfile = pdfBilgileri.beyfile;
                            bildirge.Tutar = pdfBilgileri.Tutar;
                            //bildirge.DonemStr = pdfBilgileri.DonemStr;
                            //bildirge.BelgeCesidi = pdfBilgileri.BelgeCesidi;

                            bildirgeIcerikleris.Add(bildirge);
                        }
                    }
                });


                foreach (var bildirge in bildirgeIcerikleris)
                {
                    DOSYA beyanDosya = null;
                    DOSYA tahakDosya = null;

                    if (bildirge.beyfile != null && bildirge.beyfile.Length > 50)
                    {
                        beyanDosya = new DOSYA();
                        //beyanDosya.DOSYA_ADI = $"{bildirge.Tckn}-{bildirge.Kanun.Replace("/", "-")}-{bildirge.Donem:MMyyyy}".SafeSubstring(96) + ".pdf";
                        beyanDosya.DOSYA_ADI = $"{bildirge.Tckn}-{bildirge.Kanun.Replace("/", "-")}-{bildirge.Donem:MMyyyy}".SafeSubstring(96) + ".pdf";
                        beyanDosya.SIZE = bildirge.beyfile.Length;
                        beyanDosya.ICERIK = ZipUtils.CompressZlib(bildirge.beyfile);
                        beyanDosya.MIME_TYPE = "application/pdf";
                    }

                    if (bildirge.tahfile != null && bildirge.tahfile.Length > 50)
                    {
                        tahakDosya = new DOSYA();
                        //tahakDosya.DOSYA_ADI = $"{bildirge.Tckn}-{bildirge.Kanun.Replace("/", "-")}-{bildirge.Donem:MMyyyy}".SafeSubstring(96) + ".pdf";
                        tahakDosya.DOSYA_ADI = $"{bildirge.Tckn}-{bildirge.Kanun.Replace("/", "-")}-{bildirge.Donem:MMyyyy}".SafeSubstring(96) + ".pdf";
                        tahakDosya.SIZE = bildirge.tahfile.Length;
                        tahakDosya.ICERIK = ZipUtils.CompressZlib(bildirge.tahfile);
                        tahakDosya.MIME_TYPE = "application/pdf";
                    }

                    bildirgeIslem.dataKayit(bildirge, tahakDosya, beyanDosya);

                }

                Console.WriteLine("Bildirge Sayısı : ", bildirgeIcerikleris.Count);


                beyannameOzet.BildirgeSayisi = bildirgeIslem.BildirgeSayisi;
                beyannameOzet.BildirgeTahakkuhSayisi = bildirgeIslem.TahakkuhSayisi;
                beyannameOzet.BeyannameSayisi = beyannameIslem.BeyannameSayisi;
                beyannameOzet.BeyannameTahakkuhSayisi = beyannameIslem.TahakkuhSayisi;
                beyannameOzet.MukellefSayisi = mukellefDataIslem.YeniMukellefSayisi;

                return toplamBeyannameAdedi + toplamBildirgeAdedi;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*private byte[] GetFile(string url)
        {
            var memStream = new MemoryStream();
            var stream = service.GetResponseStream(url);
            stream.CopyTo(memStream, 16384);
            return memStream.ToArray();
        }*/

        private BeyannameIcerik BeyannamePdfleriIndir(string token, string beyOid, string TahOid, string beyKodu)
        {
            var bulunanBilgiler = new BeyannameIcerik();
            var is_tah_durum = true;

            try
            {
                var tahakkukUrl = $"{GIB_API_URL}/goruntuleme?cmd=IMAJ&subcmd=TAHAKKUKGORUNTULE&tahakkukOid=" + TahOid +
                            "&beyannameOid=" + beyOid + "&USERID=&inline=true&goruntuTip=1&token=" + token + "";

                var tahakkukFile = new byte[] { };
                var sayac = 0;

                if (beyKodu == "FORMBS" && beyKodu == "FORMBA")
                {
                    is_tah_durum = false;
                }

                if (is_tah_durum == true)
                {
                    while (tahakkukFile.Length < 50 && sayac < 10)
                    {
                        tahakkukFile = service.GetFileByte(tahakkukUrl).Result;
                        sayac += 1;
                        Task.Delay(3000);
                    }

                    bulunanBilgiler.tahfile = tahakkukFile;
                }

                if (beyKodu != "OTV4")
                {
                    var beyannameUrl = $"{GIB_API_URL}/goruntuleme?cmd=IMAJ&subcmd=BEYANNAMEGORUNTULE&beyannameOid=" + beyOid +
                                "&USERID=&inline=true&goruntuTip=1&token=" + token + "";

                    var beyannameFile = new byte[] { };
                    sayac = 0;

                    while (beyannameFile.Length < 50 && sayac < 10)
                    {
                        beyannameFile = service.GetFileByte(beyannameUrl).Result;
                        sayac += 1;
                        Task.Delay(3000);
                    }

                   

                    bulunanBilgiler.beyfile = beyannameFile;

                    var beyannameOku = new PdfReader(bulunanBilgiler.beyfile);
                    var beyanString = new StringBuilder();

                    for (var i = 1; i <= beyannameOku.NumberOfPages; i++)
                    {
                        beyanString.Append(PdfTextExtractor.GetTextFromPage(beyannameOku, i));
                    }

                    var beyanTxt = beyanString.ToString().Replace("\n", "");

                    bulunanBilgiler.YuklenmeTarihi = Convert.ToDateTime(beyanTxt.SafeSubstring(beyanTxt.IndexOf("Onay Zamanı : ") + 14, 10));
                }
                else
                {
                    bulunanBilgiler.beyfile = bulunanBilgiler.tahfile;
                    bulunanBilgiler.YuklenmeTarihi = DateTime.Now;
                }

                if (is_tah_durum == true)
                {
                    var PdfReader = new PdfReader(bulunanBilgiler.tahfile);
                    var text = new StringBuilder();

                    for (var i = 1; i <= PdfReader.NumberOfPages; i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(PdfReader, i));
                    }

                    var tahakkukTxt = text.ToString().Replace("\n", "");

                    if (beyKodu == "OTV4")
                    {
                        bulunanBilgiler.YuklenmeTarihi = Convert.ToDateTime(tahakkukTxt.SafeSubstring(tahakkukTxt.IndexOf("kabul tarihi Tarihi") + 19, 10));
                    }

                    if (tahakkukTxt.Contains("İhtirazi Kayıt"))
                    {
                        bulunanBilgiler.Tutar = (tahakkukTxt.SafeSubstring(tahakkukTxt.IndexOf("TOPLAM") + 7, tahakkukTxt.IndexOf("İhtirazi Kayıt") - (tahakkukTxt.IndexOf("TOPLAM") + 7))).ToDecimal();
                    }
                    else if (tahakkukTxt.Contains("TRZM"))
                    {
                        bulunanBilgiler.Tutar = 0;
                        var tutarBilgiArr = tahakkukTxt.Split("TOPLAM ");
                        if (tutarBilgiArr.Length > 1)
                        {
                            var toplamAlani = tutarBilgiArr[1];
                            var tutar = toplamAlani.SafeSubstring(0, toplamAlani.IndexOf(',') + 3);
                            bulunanBilgiler.Tutar = tutar.ToDecimal();
                        }
                    }
                    else
                    {
                        bulunanBilgiler.Tutar = (tahakkukTxt.SafeSubstring(tahakkukTxt.IndexOf("TOPLAM") + 7, tahakkukTxt.IndexOf("İşlem Türü") - (tahakkukTxt.IndexOf("TOPLAM") + 7))).ToDecimal();
                    }

                    bulunanBilgiler.fisNo = tahakkukTxt.SafeSubstring(tahakkukTxt.IndexOf("BAŞKANLIĞI") + 10, 20);
                    if (tahakkukTxt.IndexOf("BAKANLIĞI") <= 0)
                    {
                        bulunanBilgiler.fisNo = tahakkukTxt.SafeSubstring(tahakkukTxt.IndexOf("DEFTERDARLIĞI") + 13, 20);
                    }
                    bulunanBilgiler.Vade = Convert.ToDateTime(tahakkukTxt.Substring(tahakkukTxt.IndexOf("TOPLAM") - 10, 10));

                    if (bulunanBilgiler.Tutar == null)
                    {
                        bulunanBilgiler.Tutar = 0;
                        var tutarBilgiArr = tahakkukTxt.Split("TOPLAM ");
                        if (tutarBilgiArr.Length > 1)
                        {
                            var toplamAlani = tutarBilgiArr[1];
                            var tutar = toplamAlani.SafeSubstring(0, toplamAlani.IndexOf(',') + 3);
                            bulunanBilgiler.Tutar = tutar.ToDecimal();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }



            return bulunanBilgiler;
        }

        private EBildirgeIcerik BildirgePdfleriIndir(string token, string beyoid, string tahoid)
        {
            var bulunanBilgiler = new EBildirgeIcerik();

            var tahakkukUrl = $"{GIB_API_URL}/goruntuleme?cmd=IMAJ&subcmd=SGKTAHAKKUKGORUNTULE&sgkTahakkukOid={tahoid}&USERID=&inline=true&goruntuTip=1&token={token}";
            var tahakkukFile = new byte[] { };
            var sayac = 0;

            while (tahakkukFile.Length < 50 && sayac < 10)
            {
                tahakkukFile = service.GetFileByte(tahakkukUrl).Result;
                sayac += 1;
                Task.Delay(3000);
            }

           

            bulunanBilgiler.tahfile = tahakkukFile;

            var beyannameUrl = $"{GIB_API_URL}/goruntuleme?cmd=IMAJ&subcmd=SGKHIZMETGORUNTULE&sgkTahakkukOid={tahoid}&beyannameOid={beyoid}&USERID=&inline=true&goruntuTip=1&token={token}";
            var beyannameFile = new byte[] { };
            sayac = 0;

            while (beyannameFile.Length < 50 && sayac < 10)
            {
                beyannameFile = service.GetFileByte(beyannameUrl).Result;
                sayac += 1;
                Task.Delay(3000);
            }

            bulunanBilgiler.beyfile = beyannameFile;

            if (bulunanBilgiler.tahfile.Length > 0)
            {
                var tahakOku = new PdfReader(bulunanBilgiler.tahfile);
                var tahakString = new StringBuilder();

                for (var i = 1; i <= tahakOku.NumberOfPages; i++)
                {
                    tahakString.Append(PdfTextExtractor.GetTextFromPage(tahakOku, i));
                }

                var tahakTxt = tahakString.ToString().Replace("\n", "");

                

                if (tahakTxt.IndexOf("AİT OLDUĞU YIL / AY :") > 0)
                {
                    bulunanBilgiler.Tutar = tahakTxt.SafeSubstring(tahakTxt.IndexOf("ÖDENECEK NET TUTAR") + 19, tahakTxt.IndexOf("Sayfa içeriği bilgilerle") - tahakTxt.IndexOf("ÖDENECEK NET TUTAR") - 19).ToDecimal();
                    //bulunanBilgiler.BelgeCesidi = tahakTxt.SafeSubstring(tahakTxt.IndexOf("TAHAKKUK FİŞİ") + 20, tahakTxt.IndexOf(" Belge türü") - tahakTxt.IndexOf("TAHAKKUK FİŞİ") - 20).Trim();
                    //bulunanBilgiler.DonemStr = tahakTxt.SafeSubstring(tahakTxt.IndexOf("AİT OLDUĞU YIL / AY") + 21, tahakTxt.IndexOf("PRİME ESAS KAZANÇ PRİM ORANINo") - tahakTxt.IndexOf("AİT OLDUĞU YIL / AY") - 21).Trim();

                }
            }

            return bulunanBilgiler;
        }

        

        public (byte[] file, string fileName) BeyannamePdfGor(int? id)
        {
            var beyanname = beyannameIslem.GetBeyanname(id);
            if(beyanname != null)
            {
                var unzipfile = ZipUtils.DecompressZlib(beyanname.ICERIK);
                return (unzipfile, beyanname.DOSYA_ADI);
            }
            

            return (new byte[] { }, "");
        }

        public (byte[] file, string fileName) TahakkukPdfGor(int? id)
        {
            var beyanname = beyannameIslem.GetTahakkuh(id); 
            if (beyanname != null)
            {
                var unzipfile = ZipUtils.DecompressZlib(beyanname.ICERIK);
                return (unzipfile, beyanname.DOSYA_ADI);
            }
           

            return (new byte[] { }, "");
        }

        public (byte[] file, string fileName) BeyannameZipOlustur(List<BeyannameAraModel> beyannameListesi, BeyannameBelgeModel belgeModel)
        {
            //var beyannameler = GetBeyanname<BEYANNAME_ICERIKLERI>(req).Data as List<BEYANNAME_ICERIKLERI>;
            var zipfiles = new Dictionary<string, byte[]>();

            var dosyaIslem = new DataIslemler.Islemler.Beyanname.DosyaIslem(_vctx.DataAdi);

            if(belgeModel.isKod == true)
            {
                var kodGroupList = (from m in beyannameListesi group m by m.KODU into gr select new { Kodu = gr.Key }).ToList();

                foreach (var kod in kodGroupList)
                {
                    var liste = beyannameListesi.Where(x => x.KODU == kod.Kodu).ToList();

                    foreach (var beyan in liste)
                    {
                        var beyanfile = dosyaIslem.GetDosya(beyan.FILE_ID.Value);
                        var tahakfile = dosyaIslem.GetDosya(beyan.TAH_FILE_ID.Value);

                        var donem = beyan.DONEM_BAS?.ToString("MM-yy") + "-" + beyan.DONEM_SON?.ToString("MM-yy");
                        

                        if (beyanfile != null && beyanfile.ICERIK.Length > 0 && belgeModel.isBeyanname)
                        {
                            var filename = $"{kod.Kodu}/{beyan.UNVAN.SafeSubstring(20)}-{beyan.TAH_FIS_NO}-Bey.pdf";
                            if (!zipfiles.ContainsKey(filename))
                                zipfiles.Add(filename, ZipUtils.DecompressZlib(beyanfile.ICERIK));

                            
                        }
                        if (tahakfile != null && beyanfile.ICERIK.Length > 0 && belgeModel.isTahakkuh)
                        {
                            //var filename = $"{kod.Kodu}/{beyan.UNVAN.SafeSubstring(20)}-{donem.ToString()}-Tah.pdf";
                            var filename = $"{kod.Kodu}/{beyan.UNVAN.SafeSubstring(20)}-{beyan.TAH_FIS_NO}-Tah.pdf";
                            if (!zipfiles.ContainsKey(filename))
                                zipfiles.Add(filename, ZipUtils.DecompressZlib(tahakfile.ICERIK));
                        }
                    }
                }
            }

            if (belgeModel.isKod == false)
            {
                foreach (var beyan in beyannameListesi)
                {

                    var beyanfile = dosyaIslem.GetDosya(beyan.FILE_ID.Value);
                    var tahakfile = dosyaIslem.GetDosya(beyan.TAH_FILE_ID.Value);

                    var ilkdonemay = beyan.DONEM_BAS.ToDateTime().Month.ToString().PadLeft(2, '0');
                    var sondonemay = beyan.DONEM_SON.ToDateTime().Month.ToString().PadLeft(2, '0');

                    if (beyanfile != null && beyanfile.ICERIK.Length > 0 && belgeModel.isBeyanname)
                    {
                        var filename = $"{beyan.UNVAN.SafeSubstring(25)}-{beyan.TCKN}/{beyan.KODU}/{beyan.DONEM_BAS.ToDateTime().Year}/{ilkdonemay}-{sondonemay}-{beyan.KODU}-Beyanname.pdf";

                        if (!zipfiles.ContainsKey(filename))
                            zipfiles.Add(filename, ZipUtils.DecompressZlib(beyanfile.ICERIK));
                    }

                    if (tahakfile != null && tahakfile.ICERIK.Length > 0 && belgeModel.isTahakkuh)
                    {
                        var filename = $"{beyan.UNVAN.SafeSubstring(25)}-{beyan.TCKN}/{beyan.KODU}/{beyan.DONEM_BAS.ToDateTime().Year}/{ilkdonemay}-{sondonemay}-{beyan.KODU}-Tahakkuk.pdf";

                        if (!zipfiles.ContainsKey(filename))
                            zipfiles.Add(filename, ZipUtils.DecompressZlib(tahakfile.ICERIK));
                    }


                }
            }

            

            var zip = ZipUtils.CreateZipFolder(zipfiles);

            

            var fName = $"File-{Guid.NewGuid()}.zip";

            return (zip, fName);
        }

        public (byte[] file, string fileName) BeyannameTopluPdfOlustur(List<BeyannameAraModel> beyannameler, string pdftype)
        {
            //var beyannameler = GetBeyanname<BEYANNAME_ICERIKLERI>(req).Data as List<BEYANNAME_ICERIKLERI>;
            var files = new List<byte[]>();
            var dosyaIslem = new DataIslemler.Islemler.Beyanname.DosyaIslem(_vctx.DataAdi);
            foreach (var beyan in beyannameler)
            {
                if (pdftype == "beyanname")
                {
                    //var beyanfile = _dosyaService.Get(x => x.ID == beyan.FILE_ID);
                    var beyanfile = dosyaIslem.GetDosya(beyan.FILE_ID.Value); 

                    if (beyanfile != null && beyanfile.ICERIK.Length > 0)
                        files.Add(ZipUtils.DecompressZlib(beyanfile.ICERIK));
                }
                else
                {
                    //var tahakfile = _dosyaService.Get(x => x.ID == beyan.TAH_FILE_ID);
                    var tahakfile = dosyaIslem.GetDosya(beyan.TAH_FILE_ID.Value);
                    if (tahakfile != null && tahakfile.ICERIK.Length > 0)
                        files.Add(ZipUtils.DecompressZlib(tahakfile.ICERIK));
                }
            }

            var pdf = PdfUtils.MergePdfs(files);
            var fName = $"File-{Guid.NewGuid()}.pdf";

            return (pdf, fName);
        }

        public List<FirmaModel> GonderimIcerikOlustur(List<BeyannameAraModel> clientList, string gonderimTur)
        {

            var islem = new DataIslemler.Whatsapp.BeyannameIslem(_vctx.DataAdi); 

            var result = islem.GonderimIcerikOlustur(clientList, gonderimTur);

            return result;
        }

        public List<BeyannameAraModel> GonderimDataKayit(List<FirmaModel> clientList,List<BeyannameAraModel> beyannameListesi)
        {

            var islem = new DataIslemler.Whatsapp.BeyannameIslem(_vctx.DataAdi);

            return islem.GonderimDataKayit(clientList, beyannameListesi); 
        }

        public bool BeyannameIndirmeKontrol()
        {
            var islem = new DataIslemler.KontrolIslem.DataIslem(_vctx.DataAdi);

            return islem.BeyannameKontrolIslem();
        }

        public List<TarihListeModel> BeyannameTarihListesi(DateTime? _ilkTarih,DateTime? _sonTarih)
        {

            var liste = new List<TarihListeModel>();

            var sonTarih = _sonTarih.Value;
            var ilkTarih = _ilkTarih.Value;


            while (ilkTarih < sonTarih)
            {
                var model = new TarihListeModel();
                model.BaslangicTarih = ilkTarih.ToString("yyyyMMdd");
                var tarih = ilkTarih.AddDays(5);
                if (tarih > sonTarih)
                {
                    model.BitisTarih = sonTarih.ToString("yyyyMMdd");
                    liste.Add(model);
                    break;
                }
                if (tarih < sonTarih)
                {
                    model.BitisTarih = tarih.ToString("yyyyMMdd");
                    liste.Add(model);
                    ilkTarih = tarih;
                }
                else
                {
                    model.BitisTarih = sonTarih.ToString("yyyyMMdd");
                    liste.Add(model);
                    break;
                }

                ilkTarih = ilkTarih.AddDays(1);
            }

            return liste;
        }
    }
}