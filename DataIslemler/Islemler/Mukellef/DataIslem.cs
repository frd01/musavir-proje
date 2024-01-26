using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;

namespace DataIslemler.Islemler.Mukellef
{
    public class DataIslem
    {
        Musavir_DbDataContext data;

        private int yeni_mukellef_sayisi = 0;

        public int YeniMukellefSayisi
        {
            get
            {
                return yeni_mukellef_sayisi;
            }
        }

        public DataIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public int? YeniMukellefKaydet(MukellefKayitModel item)
        {

            var model = new FIRMA_TANIMLARI();
            model.ACILIS = item.ACILIS;
            model.ADRES = item.ADRES;
            model.AKTIF = item.AKTIF;
            model.EDEVLET_KODU = item.EDEVLET_KODU;
            model.EDEVLET_SIFRE = item.EDEVLET_SIFRE;
            model.GIB_KODU = item.GIB_KODU;
            model.GIB_PAROLA = item.GIB_PAROLA;
            model.GIB_SIFRE = item.GIB_SIFRE;
            model.GRUP_KODU = item.GRUP_KODU;
            model.IL = item.IL;
            model.ILCE = item.ILCE;
            model.Ilce_Id = item.Ilce_Id;
            model.KAPANIS = item.KAPANIS;
            model.MUH_BASLANGIC = item.MUH_BASLANGIC;
            model.MUH_BITIS = item.MUH_BITIS;
            model.Sehir_Id = item.Sehir_Id;
            model.TCKN = item.TCKN;
            model.UNVAN = item.UNVAN;
            model.VERGI_DAIRESI = item.VERGI_DAIRESI;
            model.Vergi_Daire_Id = item.Vergi_Daire_Id;
            model.VERGI_NO = item.VERGI_NO;

            data.FIRMA_TANIMLARIs.InsertOnSubmit(model);
            data.SubmitChanges();

            return model.ID;

        }

        public void MukellefGuncelle(MukellefKayitModel item)
        {

            var model = data.FIRMA_TANIMLARIs.Where(x => x.ID == item.ID).SingleOrDefault();
            model.ACILIS = item.ACILIS;
            model.ADRES = item.ADRES;
            model.AKTIF = item.AKTIF;
            model.EDEVLET_KODU = item.EDEVLET_KODU;
            model.EDEVLET_SIFRE = item.EDEVLET_SIFRE;
            model.GIB_KODU = item.GIB_KODU;
            model.GIB_PAROLA = item.GIB_PAROLA;
            model.GIB_SIFRE = item.GIB_SIFRE;
            model.GRUP_KODU = item.GRUP_KODU;
            model.IL = item.IL;
            model.ILCE = item.ILCE;
            model.Ilce_Id = item.Ilce_Id;
            model.KAPANIS = item.KAPANIS;
            model.MUH_BASLANGIC = item.MUH_BASLANGIC;
            model.MUH_BITIS = item.MUH_BITIS;
            model.Sehir_Id = item.Sehir_Id;
            model.TCKN = item.TCKN;
            model.UNVAN = item.UNVAN;
            model.VERGI_DAIRESI = item.VERGI_DAIRESI;
            model.Vergi_Daire_Id = item.Vergi_Daire_Id;
            model.VERGI_NO = item.VERGI_NO;

            data.SubmitChanges();
        }

        public int BeyannameYeniMukellefKayit(Tacmin.Data.DbModel.FIRMA_TANIMLARI item)
        {

            var model = new FIRMA_TANIMLARI();
            model.AKTIF = item.AKTIF;
            model.UNVAN = item.UNVAN;
            model.VERGI_DAIRESI = item.VERGI_DAIRESI;
            model.TCKN = item.TCKN;
            model.VERGI_NO = item.VERGI_NO;

            data.FIRMA_TANIMLARIs.InsertOnSubmit(model);
            data.SubmitChanges();

            yeni_mukellef_sayisi += 1;

            return model.ID;
        }
    }
}
