using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;

namespace DataIslemler.Listeler.Mukellef
{
    public class Listeler
    {
        Musavir_DbDataContext data;

        public Listeler(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<Tacmin.Data.DbModel.FIRMA_TANIMLARI> GetMukellefListesi()
        {

            var liste = new List<Tacmin.Data.DbModel.FIRMA_TANIMLARI>();

            foreach (var item in data.FIRMA_TANIMLARIs)
            {
                var model = new Tacmin.Data.DbModel.FIRMA_TANIMLARI();

                model.ACILIS = item.ACILIS;
                model.ADRES = item.ADRES;
                model.AKTIF = item.AKTIF;
                model.EDEVLET_KODU = item.EDEVLET_KODU;
                model.EDEVLET_SIFRE = item.EDEVLET_SIFRE;
                model.GIB_KODU = item.GIB_KODU;
                model.GIB_PAROLA = item.GIB_PAROLA;
                model.GIB_SIFRE = item.GIB_SIFRE;
                model.GRUP_KODU = item.GRUP_KODU;
                model.ID = item.ID;
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

                liste.Add(model);
            }

            return liste;
        }

        public MukellefKayitModel GetMukellef(int id)
        {

            var data_model = data.FIRMA_TANIMLARIs.Where(x => x.ID == id).SingleOrDefault();

            var model = new MukellefKayitModel(); 

            model.ACILIS = data_model.ACILIS; 
            model.ADRES = data_model.ADRES;
            model.AKTIF = data_model.AKTIF;
            model.EDEVLET_KODU = data_model.EDEVLET_KODU;
            model.EDEVLET_SIFRE = data_model.EDEVLET_SIFRE;
            model.Firma_Iletisim = FirmaIletisimListesi(id);
            model.GIB_KODU = data_model.GIB_KODU;
            model.GIB_PAROLA = data_model.GIB_PAROLA;
            model.GIB_SIFRE = data_model.GIB_SIFRE;
            model.GRUP_KODU = data_model.GRUP_KODU;
            model.ID = data_model.ID;
            model.IL = data_model.IL;
            model.ILCE = data_model.ILCE;
            model.Ilce_Id = data_model.Ilce_Id;
            model.KAPANIS = data_model.KAPANIS;
            model.MUH_BASLANGIC = data_model.MUH_BASLANGIC;
            model.MUH_BITIS = data_model.MUH_BITIS;
            model.Sehir_Id = data_model.Sehir_Id;
            model.TCKN = data_model.TCKN;
            model.UNVAN = data_model.UNVAN;
            model.VERGI_DAIRESI = data_model.VERGI_DAIRESI;
            model.Vergi_Daire_Id = data_model.Vergi_Daire_Id;
            model.VERGI_NO = data_model.VERGI_NO;
           

            return model;
        }

        public List<Tacmin.Data.DbModel.Firma_Iletisim> FirmaIletisimListesi(int firmaId)
        {

            var liste = new List<Tacmin.Data.DbModel.Firma_Iletisim>();

            foreach (var item in data.Firma_Iletisims.Where(x=>x.Firma_Id == firmaId))
            {
                var model = new Tacmin.Data.DbModel.Firma_Iletisim();

                model.Adi = item.Adi;
                model.Firma_Id = item.Firma_Id;
                model.Id = item.Id;
                model.Mail = item.Mail;
                model.Telefon = item.Telefon;

                liste.Add(model);
            }

            return liste;
        }
    }
}
