using DataIslemler.Data;
using DataIslemler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Tacmin.Model.Mukellef;

namespace DataIslemler.Listeler.Mukellef
{
    public class BeyannameTakipTabloListesi
    {

        Musavir_DbDataContext data;
        List<BEYANNAME_ICERIKLERI> data_beyanname_listesi;
        List<Get_Firma_Beyanname_Takip_ListesiResult> data_beyanname_takip_listesi;
        List<Beyanname_Tur_Donem_Aylar> data_beyanname_tur_donem_ay_listesi;
        List<FIRMA_TANIMLARI> data_firma_listesi;

        public BeyannameTakipTabloListesi(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_beyanname_listesi = data.BEYANNAME_ICERIKLERIs.Where(x => x.GONDERIM_TARIHI.Value.Year == DateTime.Now.Year).ToList();
            data_beyanname_takip_listesi = data.Get_Firma_Beyanname_Takip_Listesi().ToList();
            data_beyanname_tur_donem_ay_listesi = data.Beyanname_Tur_Donem_Aylars.ToList();
            data_firma_listesi = data.FIRMA_TANIMLARIs.Where(x => x.AKTIF == true).ToList();

        }

        public List<BeyannameTakipTabloModel> GetBeyannameTabloTakipListesi()
        {

            var liste = new List<BeyannameTakipTabloModel>();

            foreach (var item in data.BeyannameTakipListesi())
            {
                var model = GetTakipModel(item.Id, item.Tur_Key);
                model.Beyanname_Tur = item.Tur_Adi;

                if (model.Toplam > 0)
                    liste.Add(model);
            }

            return liste;
        }

        private BeyannameTakipTabloModel GetTakipModel(int? beyanname_tur_id, string tur_key)
        {
            var model = new BeyannameTakipTabloModel();
            model.Toplam = 0;
            model.Kalan = 0;
            model.Onaylanan = 0;
            model.OnayFirmaListesi = new List<TakipFirma>();
            model.KalanFirmaListesi = new List<TakipFirma>();
            model.ToplamFirmaListesi = new List<TakipFirma>();


            var liste = data_beyanname_takip_listesi.Where(x => x.Beyanname_Tur_Id == beyanname_tur_id).ToList();

            foreach (var item in liste)
            {
                if (item.Beyanname_Donem_Id == 3 || item.Beyanname_Donem_Id == 4)
                {
                    var donem_varmi = ay_yil_kontrol(item.Beyanname_Donem_Id, item.Beyanname_Tur_Id);

                    if (donem_varmi == true)
                    {
                        model.Toplam += 1;
                        model.ToplamFirmaListesi.Add(new TakipFirma { Id = item.Firma_Id, Unvan = get_unvan(item.Firma_Id) });

                        var beyanname_durum = beyanname_kontrol(tur_key, item.Firma_Id);
                        if (beyanname_durum > 0)
                        {
                            model.Onaylanan += 1;
                            model.OnayFirmaListesi.Add(new TakipFirma { Id = item.Firma_Id, Unvan = get_unvan(item.Firma_Id) });
                        }
                        else
                        {
                            model.KalanFirmaListesi.Add(new TakipFirma { Id = item.Firma_Id, Unvan = get_unvan(item.Firma_Id) });
                        }

                    }

                }

                if (item.Beyanname_Donem_Id == 2)
                {
                    model.Toplam += 1;
                    model.ToplamFirmaListesi.Add(new TakipFirma { Id = item.Firma_Id, Unvan = get_unvan(item.Firma_Id) });
                    var beyanname_durum = beyanname_kontrol(tur_key, item.Firma_Id);
                    if (beyanname_durum > 0)
                    {
                        model.Onaylanan += 1;
                        model.OnayFirmaListesi.Add(new TakipFirma { Id = item.Firma_Id, Unvan = get_unvan(item.Firma_Id) });
                    }
                    else
                    {
                        model.KalanFirmaListesi.Add(new TakipFirma { Id = item.Firma_Id, Unvan = get_unvan(item.Firma_Id) });
                    }


                }
            }

            model.Kalan = model.Toplam - model.Onaylanan;

            return model;
        }

        private bool ay_yil_kontrol(int? donem_id, int? tur_id)
        {

            var durum = true;

            try
            {
                var result = (from m in data_beyanname_tur_donem_ay_listesi where m.Beyanname_Donem_Id == donem_id && tur_id == m.Beyanname_Tur_Id && m.Ay == DateTime.Now.Month select m).Count();

                if (result <= 0)
                    durum = false;

            }
            catch
            {

                durum = false;
            }

            return durum;
        }

        private int beyanname_kontrol(string tur_key, int? firma_id)
        {

            return (from m in data_beyanname_listesi where m.FIRMA_ID == firma_id && m.KODU == tur_key select m).Count();
        }

        private string get_unvan(int? id)
        {

            return data_firma_listesi.Where(x => x.ID == id).SingleOrDefault().UNVAN;
        }
    }
}
