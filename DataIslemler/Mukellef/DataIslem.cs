using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Mukellef;

namespace DataIslemler.Mukellef
{
    public class DataIslem
    {
        private Musavir_DbDataContext data;

        public DataIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public KayitResModel MukellefSgkSubeKaydet(SgkListeModel item,int? firmaId)
        {
            var res_model = new KayitResModel();

            res_model.IslemDurum = true;

            if(item.Id == null)
            {
                res_model = SubeKayit(item, firmaId);
            }
            if (item.Id != null)
            {
                res_model = SubeGuncelle(item,firmaId);
            }

            return res_model;
        }

        private KayitResModel SubeKayit(SgkListeModel item,int? firmaId)
        {
            var res_model = new KayitResModel();

            res_model.IslemDurum = true;

            var sube_adi = data.Firma_Sgk_Bilgileris.Where(x => x.FirmaId == firmaId && x.SubeAdi == item.SubeAdi).ToList();

            if (sube_adi.Count>0)
            {
                res_model.IslemDurum = false;
                res_model.Mesaj = "Bu Şube Adı Daha Önce Kullanılmış Kontrol Ediniz.";

                return res_model;
            }

            try
            {
                var model = new Firma_Sgk_Bilgileri();

                model.AcilisTarihi = item.AcilisTarihi;
                model.Adres = item.Adres;
                model.Aktif = item.Aktif;
                model.FirmaId = firmaId;
                model.IsyeriSifresi = item.IsyeriSifresi;
                model.KapanisTarihi = item.KapanisTarihi;
                model.KullaniciAdi_2 = item.KullaniciAdi_2;
                model.KullanıciAdi_1 = item.KullaniciAdi_1;
                model.Notlar = item.Notlar;
                model.SistemSifresi = item.SistemSifresi;
                model.SubeAdi = item.SubeAdi;
                model.M = item.M;
                model.IsKoluKodu = item.IsKoluKodu;
                model.Yeni = item.Yeni;
                model.Eski = item.Yeni;
                model.IsYeriSıraNo = item.IsYeriSiraNo;
                model.IlKodu = item.IlKodu;
                model.IlceKodu = item.IlceKodu;
                model.KontrolNo = item.KontrolNo;
                model.AraciKodu = item.AraciKodu;

                data.Firma_Sgk_Bilgileris.InsertOnSubmit(model);
                data.SubmitChanges();

                SgkTakipListesiKayit(item.TakipListesi, firmaId, model.Id);

                res_model.IslemDurum = true;
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        private KayitResModel SubeGuncelle(SgkListeModel item,int? firmaId)
        {
            var res_model = new KayitResModel();

            res_model.IslemDurum = true;

            try
            {
                var model = data.Firma_Sgk_Bilgileris.Where(x =>  x.Id == item.Id).SingleOrDefault();

                model.AcilisTarihi = item.AcilisTarihi;
                model.Adres = item.Adres;
                model.Aktif = item.Aktif;
                model.IsyeriSifresi = item.IsyeriSifresi;
                model.KapanisTarihi = item.KapanisTarihi;
                model.KullaniciAdi_2 = item.KullaniciAdi_2;
                model.KullanıciAdi_1 = item.KullaniciAdi_1;
                model.Notlar = item.Notlar;
                model.SistemSifresi = item.SistemSifresi;
                model.SubeAdi = item.SubeAdi;
                model.M = item.M;
                model.IsKoluKodu = item.IsKoluKodu;
                model.Yeni = item.Yeni;
                model.Eski = item.Yeni;
                model.IsYeriSıraNo = item.IsYeriSiraNo;
                model.IlKodu = item.IlKodu;
                model.IlceKodu = item.IlceKodu;
                model.KontrolNo = item.KontrolNo;
                model.AraciKodu = item.AraciKodu;

                data.SubmitChanges();

                if (item.TakipListesi != null)
                {
                    SgkTakipListesiKayit(item.TakipListesi, firmaId, model.Id);
                }

                res_model.IslemDurum = true;
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message + firmaId.ToString();
            }

            return res_model;
        }

        private void SgkTakipListesiKayit(List<BildirgeTakipModel> takipListesi,int? firmaId,int? subeId)
        {
            SgkTakipListesiSil(firmaId, subeId);

            foreach (var item in takipListesi)
            {
                var model = new Firma_Bildirge_Takip();

                model.BildirgeTurId = item.Id;
                model.FirmaId = firmaId;
                model.FirmaSgkId = subeId;

                data.Firma_Bildirge_Takips.InsertOnSubmit(model);
                data.SubmitChanges();

            }
        }

        private void SgkTakipListesiSil(int? firmaId,int? subeId)
        {
            var liste = data.Firma_Bildirge_Takips.Where(x => x.FirmaId == firmaId && x.FirmaSgkId == subeId).ToList();
            if (liste.Count > 0)
            {
                data.Firma_Bildirge_Takips.DeleteAllOnSubmit(liste);
                data.SubmitChanges();
            }
           
        }
    }
}
