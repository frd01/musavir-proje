using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Bildirge;

namespace DataIslemler.Bildirge
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;

        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<ViziteListeModel> SorguViziteListe(DateTime? tarih,string vaka)
        {
            var liste = new List<ViziteListeModel>();

            var data_list = data.FirmaViziteListesi_Sorgu_Tarih(tarih).Where(x=>x.Vaka == vaka).ToList();

            foreach (var item in data_list)
            {
                var model = new ViziteListeModel();

                model.AdSoyAd = item.Adi + " " + item.Soy_Adi;
                model.Id = item.Id;
                model.IsBasiKontrolTarihi = item.Is_Bas_Kont_Tar;
                model.RaporBaslamaTarihi = item.Poliklinik_Tar;
                model.RaporSiraNo = item.Rapor_Sira_No;
                model.RaporTakipNo = item.Rapor_Takip_No;
                model.TcNo = item.Tc_No;
                model.VakaAdi = item.Vaka_Adi;
                model.Vaka = item.Vaka;
                model.MedulaId = item.Medula_Rapor_Id;
                model.KullaniciKodu = item.KullanıciAdi_1;
                model.IsyeriKodu = item.KullaniciAdi_2;
                model.IsyeriSifresi = item.IsyeriSifresi;
                model.Unvan = item.UNVAN;
                model.SubeAdi = item.SubeAdi;
                model.FirmaId = item.FirmaId;
                model.SubeId = item.Sube_Id;

                liste.Add(model);
            }

            return liste;
        }

        public List<ViziteListeModel> ViziteListesi(bool? durum,DateTime? _ilkTarih,DateTime? _sonTarih)
        {

            var ilkTarih = _ilkTarih.Value.AddMonths(-1);
            var sonTarih = _sonTarih.Value.AddMonths(1);

            var liste = new List<ViziteListeModel>();

            var data_list = data.FirmaViziteListesi(ilkTarih,sonTarih).Where(x => x.Onayli == durum).ToList();

           

            foreach (var item in data_list)
            {
                var model = new ViziteListeModel();

                model.AdSoyAd = item.Adi + " " + item.Soy_Adi;
                model.Id = item.Id;
                model.IsBasiKontrolTarihi = item.Is_Bas_Kont_Tar;
                model.RaporBaslamaTarihi = item.Poliklinik_Tar;
                model.RaporSiraNo = item.Rapor_Sira_No;
                model.RaporTakipNo = item.Rapor_Takip_No;
                model.TcNo = item.Tc_No;
                model.VakaAdi = item.Vaka_Adi;
                model.Vaka = item.Vaka;
                model.MedulaId = item.Medula_Rapor_Id;
                model.KullaniciKodu = item.KullanıciAdi_1;
                model.IsyeriKodu = item.KullaniciAdi_2;
                model.IsyeriSifresi = item.IsyeriSifresi;
                model.Unvan = item.UNVAN;
                model.SubeAdi = item.SubeAdi;
                model.FirmaId = item.FirmaId;
                model.SubeId = item.Sube_Id;

                liste.Add(model);
            }

            return liste;
        }

        private Firma_Sgk_Bilgileri GetSubeBilgi(int? firmaId,int? subeId)
        {
            var model = data.Firma_Sgk_Bilgileris.Where(x => x.FirmaId == firmaId && x.Id == subeId).SingleOrDefault();

            return model;
        }

        public List<ViziteFirmaSubeModel> FirmaSubeListesi()
        {
            var liste = new List<ViziteFirmaSubeModel>();

            foreach (var item in data.Firma_Sgk_Bilgileris)
            {
                var model = new ViziteFirmaSubeModel();

                model.Id = item.Id;
                model.FirmaId = item.FirmaId;
                model.IsYeriKodu = item.KullaniciAdi_2;
                model.IsYeriSifresi = item.IsyeriSifresi;
                model.KullaniciKodu = item.KullanıciAdi_1;
                model.SistemSifresi = item.SistemSifresi;

                liste.Add(model);

            }

            return liste;
        }

        public List<IseGirisListeModel> SorguListe(DateTime? tarih,string durum)
        {
            var liste = new List<IseGirisListeModel>();

            var data_list = data.FirmaIseGirisCikisListesi_Sorgu_Tarih(tarih).ToList();

            if (durum == "çıkış")
            {
                data_list = data_list.Where(x => x.Durum == "çıkış").ToList();
            }

            if (durum == "giriş")
            {
                data_list = data_list.Where(x => x.Durum == "giriş").ToList();
            }


            foreach (var item in data_list)
            {
                var model = new IseGirisListeModel();

                model.AdiSoyadi = item.AdiSoyAdi;
                model.Durum = item.Durum;
                model.FirmaId = item.FirmaId;
                model.Id = item.Id;
                model.IslemTarihi = item.IslemTarihi;
                model.ReferansKodu = item.ReferansKodu;
                model.SubeAdi = item.SubeAdi;
                model.SubeId = item.SubeId;
                model.TcNo = item.TcNo;
                model.Unvan = item.UNVAN;

                if (model.Durum == "çıkış")
                {
                    model.Tarih = item.CikisTarihi;
                }
                else
                {
                    model.Tarih = item.GirisTarihi;
                }

                liste.Add(model);
            }

            return liste;
        }

        public List<IseGirisListeModel> IseGirisCikisListesi(DateTime? ilkTarih,DateTime? sonTarih)
        {
            var liste = new List<IseGirisListeModel>(); 

            foreach (var item in data.FirmaIseGirisCikisListesi(ilkTarih,sonTarih))
            {
                var model = new IseGirisListeModel();

                model.AdiSoyadi = item.AdiSoyAdi;
                model.Durum = item.Durum;
                model.FirmaId = item.FirmaId;
                model.Id = item.Id;
                model.IslemTarihi = item.IslemTarihi;
                model.ReferansKodu = item.ReferansKodu;
                model.SubeAdi = item.SubeAdi;
                model.SubeId = item.SubeId;
                model.TcNo = item.TcNo;
                model.Unvan = item.UNVAN;

                if (model.Durum == "çıkış")
                {
                    model.Tarih = item.CikisTarihi;
                }
                else
                {
                    model.Tarih = item.GirisTarihi;
                }

                liste.Add(model);
            }

            return liste;
        }

        public Tacmin.Model.DosyaModel GetPdfEvrak(int id)
        {
            var model = new Tacmin.Model.DosyaModel();

            var item = data.IseGirisCikisDosyalars.Where(x => x.Id == id).SingleOrDefault();

            model.DosyaAdi = item.DosyaAdi;
            model.Icerik = item.Icerik.ToArray();

            return model;
        }
    }
}
