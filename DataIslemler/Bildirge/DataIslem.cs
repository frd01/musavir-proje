using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Bildirge;


namespace DataIslemler.Bildirge
{
    public class DataIslem
    {
        private Musavir_DbDataContext data;
        private List<Firma_IseGirisCikislar> data_ise_giris_listesi;
        private List<Firma_Sgk_Bilgileri> data_firma_sgk_liste;

        public DataIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public KayitResModel IseGirisCikisKaydet(List<IseGirisResModel> clientList)
        {
            var res_model = new KayitResModel();
            res_model.IslemDurum = true;

            try
            {
                if (data_ise_giris_listesi == null)
                {
                    data_ise_giris_listesi = data.Firma_IseGirisCikislars.ToList();
                }

                foreach (var item in clientList)
                {
                    var kontrol = data_ise_giris_listesi.Where(x => x.FirmaId == item.firmaId &&
                    x.SubeId == item.subeId && x.ReferansKodu == item.referans_kodu).ToList();

                    if (kontrol.Count <= 0)
                    {
                        var model = new Firma_IseGirisCikislar();

                        model.AdiSoyAdi = item.adi_soyadi;
                        model.CikisTarihi = item.isten_cikis_tarihi;
                        model.Durum = item.tur;
                        model.FirmaId = item.firmaId;
                        model.GirisTarihi = item.giris_tarihi;
                        model.IslemTarihi = item.islem_tarihi;
                        model.ReferansKodu = item.referans_kodu;
                        model.SubeId = item.subeId;
                        model.TcNo = item.tc_no;

                        data.Firma_IseGirisCikislars.InsertOnSubmit(model);
                        data.SubmitChanges();

                        if (item.tur == "çıkış")
                        {
                            GetCikisEvrak(item.subeId, item.referans_kodu, model.Id);
                        }
                        else
                        {
                            GetGirisEvrak(item.subeId, item.referans_kodu, model.Id);
                        }
                    }
                }

                res_model.IslemDurum = true;

            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }
            
            return res_model;
        }

        public KayitResModel ViziteKayitIslem(List<ViziteResponseModel> clientList)
        {
            var res_model = new KayitResModel();
            res_model.IslemDurum = true;

            var kayit_tur = "ham";

            try
            {
                foreach (var item in clientList)
                {
                    if (dataKayitKontrol(item) <=0)
                    {
                        viziteKaydet(item);
                        kayit_tur = "kaydet";
                    }
                    if (dataKayitKontrol(item) >0)
                    {
                        viziteGuncelle(item);
                        kayit_tur = "güncelle";
                    }
                }

                res_model.IslemDurum = true;
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message + " " + kayit_tur;
            }

            return res_model;
        }

        private void viziteKaydet(ViziteResponseModel item)
        {
            var model = new Firma_Viziteler();
            model.FirmaId = item.firmaId;
            model.Sube_Id = item.subeId;
            model.Adi = item.adi.Trim();
            model.Soy_Adi = item.soy_adi.Trim();
            model.Tc_No = item.tc_no;
            model.Vaka = item.vaka;
            model.Vaka_Adi = item.vaka_adi;
            model.Yatis_Rapor_Baslangic_Tarihi = GetTarih(item.yatis_rapor_baslangic_tarihi);
            model.A_Baslangic_Tarihi = GetTarih(item.a_baslangic_tarihi);
            model.A_Bitis_Tarihi = GetTarih(item.a_bitis_tarihi);
            model.Dogum_Baslangic_Tarihi = GetTarih(item.dogum_baslangic_tarihi);
            model.Is_Bas_Kont_Tar = GetTarih(item.isbasi_tarihi);
            model.Is_Kazasi_Tarihi = GetTarih(item.Is_Kazasi_Tarihi);
            model.Medula_Rapor_Id = item.medula_rapor_id;
            model.Onayli = item.onayli;
            model.Poliklinik_Tar = GetTarih(item.poliklinik_tarihi);
            model.Rapor_Sira_No = GetIntValue(item.rapor_sira_no);
            model.Rapor_Takip_No = item.rapor_takip_no;
            model.Yeni_Kayit = true;

            data.Firma_Vizitelers.InsertOnSubmit(model);
            data.SubmitChanges();
        }

        private void viziteGuncelle(ViziteResponseModel item)
        {
            var model = data.Firma_Vizitelers.Where(x => x.Medula_Rapor_Id == item.medula_rapor_id
                && x.FirmaId == item.firmaId && x.Sube_Id == item.subeId).SingleOrDefault();

            if (model != null)
            {
                model.FirmaId = item.firmaId;
                model.Sube_Id = item.subeId;
                model.Adi = item.adi.Trim();
                model.Soy_Adi = item.soy_adi.Trim();
                model.Tc_No = item.tc_no;
                model.Vaka = item.vaka;
                model.Vaka_Adi = item.vaka_adi;
                model.Yatis_Rapor_Baslangic_Tarihi = GetTarih(item.yatis_rapor_baslangic_tarihi);
                model.A_Baslangic_Tarihi = GetTarih(item.a_baslangic_tarihi);
                model.A_Bitis_Tarihi = GetTarih(item.a_bitis_tarihi);
                model.Dogum_Baslangic_Tarihi = GetTarih(item.dogum_baslangic_tarihi);
                model.Is_Bas_Kont_Tar = GetTarih(item.isbasi_tarihi);
                model.Is_Kazasi_Tarihi = GetTarih(item.Is_Kazasi_Tarihi);
                //model.Medula_Rapor_Id = item.medula_rapor_id;
                model.Onayli = item.onayli;
                model.Poliklinik_Tar = GetTarih(item.poliklinik_tarihi);
                model.Rapor_Sira_No = GetIntValue(item.rapor_sira_no);
                model.Rapor_Takip_No = item.rapor_takip_no;
                model.Yeni_Kayit = true;

                data.SubmitChanges();
            }
        }

        private int dataKayitKontrol(ViziteResponseModel item)
        {
            var kontrol = data.Firma_Vizitelers.Where(x => x.Medula_Rapor_Id == item.medula_rapor_id
                && x.FirmaId == item.firmaId && x.Sube_Id == item.subeId).ToList();

            return kontrol.Count;
        }

        private DateTime? GetTarih(string value)
        {

            DateTime? tarih = null;

            try
            {
                tarih = Convert.ToDateTime(value);
            }
            catch 
            {

                tarih = null;
            }

            return tarih;
        }

        private int? GetIntValue(string value)
        {
            int? deger = null;

            try
            {
                deger = Convert.ToInt32(value);
            }
            catch 
            {

                deger = null;
            }

            return deger;
        }

        private Firma_Sgk_Bilgileri GetSubeBilgi(int? subeId)
        {
            if (data_firma_sgk_liste == null)
            {
                data_firma_sgk_liste = data.Firma_Sgk_Bilgileris.ToList();
            }

            var model = data_firma_sgk_liste.Where(x => x.Id == subeId).SingleOrDefault();

            return model;
        }

        private void GetCikisEvrak(int? subeId,string referansKodu,int kayitId)
        {
            var sube = GetSubeBilgi(subeId);

            var kullanici = new DataIslemler.IseCikisService.kullaniciBilgileri();
            kullanici.isyeriKodu = sube.KullaniciAdi_2;
            kullanici.isyeriSicil = "22120010111926760201029000";
            kullanici.isyeriSifre = sube.IsyeriSifresi;
            kullanici.kullaniciAdi = sube.KullanıciAdi_1;
            kullanici.sistemSifre = sube.SistemSifresi;

            var client = new DataIslemler.IseCikisService.WS_SgkIstenCikisClient();
            client.Open();

            var ref_code = Convert.ToInt32(referansKodu);

            var pdf_file = client.istenCikisPdfDokum(kullanici, ref_code).pdfByteArray;

            client.Close();

            var dosya_model = new IseGirisCikisDosyalar();
            dosya_model.BelgeId = kayitId;
            dosya_model.Icerik = pdf_file;
            dosya_model.DosyaAdi = referansKodu + ".pdf";
            //dosya_model.Boyut = pdf_file.Length / 1000;

            data.IseGirisCikisDosyalars.InsertOnSubmit(dosya_model);
            data.SubmitChanges();
        }

        private void GetGirisEvrak(int? subeId, string referansKodu,int kayitId)
        {
            var sube = GetSubeBilgi(subeId);

            var kullanici = new DataIslemler.IseGirisService.kullaniciBilgileri();
            kullanici.isyeriKodu = sube.KullaniciAdi_2;
            kullanici.isyeriSicil = "22120010111926760201029000";
            kullanici.isyeriSifre = sube.IsyeriSifresi;
            kullanici.kullaniciAdi = sube.KullanıciAdi_1;
            kullanici.sistemSifre = sube.SistemSifresi;

            var client = new DataIslemler.IseGirisService.WS_SgkIseGirisClient();
            client.Open();

            var ref_code = Convert.ToInt32(referansKodu);

            var pdf_file = client.iseGirisPdfDokum(kullanici, ref_code).pdfByteArray;

            client.Close();

            var dosya_model = new IseGirisCikisDosyalar();
            dosya_model.BelgeId = kayitId;
            dosya_model.Icerik = pdf_file;
            dosya_model.DosyaAdi = referansKodu + ".pdf";
            //dosya_model.Boyut = pdf_file.Length / 1000;

            data.IseGirisCikisDosyalars.InsertOnSubmit(dosya_model);
            data.SubmitChanges();
        }
    }
}
