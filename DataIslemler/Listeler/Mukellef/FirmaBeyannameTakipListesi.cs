using DataIslemler.Data;
using DataIslemler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Tacmin.Model.Mukellef;

namespace DataIslemler.Listeler.Mukellef
{
    public class FirmaBeyannameTakipListesi
    {
        Musavir_DbDataContext data;
        List<BEYANNAME_ICERIKLERI> data_beyanname_listesi;
        List<Beyanname_Tur_Donem_Aylar> data_beyanname_tur_donem_ay_listesi;
        List<Firma_Beyanname_Takip> data_firma_beyanname_takip_listesi;
        List<FIRMA_TANIMLARI> data_firma_listesi;
        List<Beyanname_Turleri> data_beyanname_tur_listesi;

        private int gecerli_ay;
        private int gecerli_yil;
        public FirmaBeyannameTakipListesi(int ay,string dataAdi)
        {
            this.gecerli_ay = ay;
            this.gecerli_yil = DateTime.Now.Year;
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext();

            data_beyanname_listesi = data.BEYANNAME_ICERIKLERIs.Where(x => x.GONDERIM_TARIHI.Value.Month == gecerli_ay && x.GONDERIM_TARIHI.Value.Year == gecerli_yil).ToList();
            data_firma_beyanname_takip_listesi = data.Firma_Beyanname_Takips.Where(x => x.Beyanname_Donem_Id != 1).ToList();
            data_beyanname_tur_donem_ay_listesi = data.Beyanname_Tur_Donem_Aylars.ToList();
            data_firma_listesi = data.FIRMA_TANIMLARIs.Where(x => x.AKTIF == true).ToList();
            data_beyanname_tur_listesi = data.Beyanname_Turleris.ToList();
        }

        public List<BeyannameTakipModel> Firma_Beyanname_Takip_Listesi()
        {

            var liste = new List<BeyannameTakipModel>();

            foreach (var item in data_firma_listesi)
            {
                var model = new BeyannameTakipModel();
                model.Id = item.ID;
                model.Unvan = item.UNVAN;
                model.Ba = Get_Takip_Model(item.ID, BeyannameTurEnums.Ba);
                model.Damga = Get_Takip_Model(item.ID, BeyannameTurEnums.DAMGA);
                model.Bs = Get_Takip_Model(item.ID, BeyannameTurEnums.FORMBS);
                model.Gelir = Get_Takip_Model(item.ID, BeyannameTurEnums.GELIR);
                model.Gelir_1001E = Get_Takip_Model(item.ID, BeyannameTurEnums.GELIR1001E);
                model.G_Gecici = Get_Takip_Model(item.ID, BeyannameTurEnums.GGECICI);
                model.K_Gecici = Get_Takip_Model(item.ID, BeyannameTurEnums.KGECICI);
                model.Kurumlar = Get_Takip_Model(item.ID, BeyannameTurEnums.KURUMLAR);
                model.Muh_Sgk = Get_Takip_Model(item.ID, BeyannameTurEnums.MUHSGK);
                model.Muh_Sgk2 = Get_Takip_Model(item.ID, BeyannameTurEnums.MUHSGK2);
                model.Oiv = Get_Takip_Model(item.ID, BeyannameTurEnums.OIV);
                model.Otv1 = Get_Takip_Model(item.ID, BeyannameTurEnums.OTV1);
                model.Otv3A = Get_Takip_Model(item.ID, BeyannameTurEnums.OTV3A);
                model.Otv3B = Get_Takip_Model(item.ID, BeyannameTurEnums.OTV3B);
                model.Otv4 = Get_Takip_Model(item.ID, BeyannameTurEnums.OTV4);
                model.Poset = Get_Takip_Model(item.ID, BeyannameTurEnums.POSET);
                model.Turizm = Get_Takip_Model(item.ID, BeyannameTurEnums.TURIZM);
                model.Kdv1 = Get_Takip_Model(item.ID, BeyannameTurEnums.KDV1);
                model.Kdv2 = Get_Takip_Model(item.ID, BeyannameTurEnums.KDV2);
                model.Kdv4 = Get_Takip_Model(item.ID, BeyannameTurEnums.KDV4);

                var liste_durum = true;

                if (
                     model.Ba.Donem_Adi == "YOK" && model.Damga.Donem_Adi == "YOK" && model.Bs.Donem_Adi == "YOK" && model.Gelir.Donem_Adi == "YOK"
                     && model.Gelir_1001E.Donem_Adi == "YOK" && model.G_Gecici.Donem_Adi == "YOK" && model.K_Gecici.Donem_Adi == "YOK"
                     && model.Kurumlar.Donem_Adi == "YOK" && model.Muh_Sgk.Donem_Adi == "YOK" && model.Muh_Sgk2.Donem_Adi == "YOK" && model.Oiv.Donem_Adi == "YOK"
                     && model.Otv1.Donem_Adi == "YOK" && model.Otv3A.Donem_Adi == "YOK" && model.Otv3B.Donem_Adi == "YOK" && model.Otv4.Donem_Adi == "YOK"
                     && model.Poset.Donem_Adi == "YOK" && model.Turizm.Donem_Adi == "YOK" && model.Kdv1.Donem_Adi == "YOK" && model.Kdv4.Donem_Adi == "YOK"
                     )
                {
                    liste_durum = false;
                }

                if (liste_durum == true)
                    liste.Add(model);
            }

            return liste;
        }

        private BeyannameDonemModel Get_Takip_Model(int? firma_id, BeyannameTurEnums beyanname_tur)
        {
            var model = new BeyannameDonemModel();
            model.Id = 1;
            model.Donem_Adi = "YOK";

            var takip_model = Get_Firma_Beyanname_Takip(firma_id, beyanname_tur);

            if (takip_model == null)
            {
                model.Donem_Adi = "YOK";
                return model;
            }

            var beyanname_tur_key = Get_Beyanname_Tur_Key(beyanname_tur);

            if (takip_model.Beyanname_Donem_Id == 3 || takip_model.Beyanname_Donem_Id == 4)
            {
                var donem_ay = data_beyanname_tur_donem_ay_listesi.Where(x => x.Beyanname_Donem_Id == takip_model.Beyanname_Donem_Id && takip_model.Beyanname_Tur_Id == x.Beyanname_Tur_Id && x.Ay == gecerli_ay).Count();

                if (donem_ay <= 0)
                {
                    model.Donem_Adi = "YOK";
                    return model;
                }

                var donem_ay_model = data_beyanname_tur_donem_ay_listesi.Where(x => x.Beyanname_Donem_Id == takip_model.Beyanname_Donem_Id && takip_model.Beyanname_Tur_Id == x.Beyanname_Tur_Id && x.Ay == gecerli_ay).SingleOrDefault();



                var beyanname_durum = data_beyanname_listesi.Where(x => x.KODU == beyanname_tur_key).Count();

                if (beyanname_durum <= 0)
                {
                    model.Donem_Adi = "Verilmedi";
                }

                else
                {
                    var bey_model = (from m in data_beyanname_listesi where m.KODU == beyanname_tur_key && m.FIRMA_ID == firma_id orderby m.GONDERIM_TARIHI descending select m).ToList()[0];
                    model.Donem_Adi = string.Format("{0:dd/MM/yy}", bey_model.GONDERIM_TARIHI);
                    model.Beyanname_Id = bey_model.ID;
                }


            }

            else
            {
                var beyanname_durum = data_beyanname_listesi.Where(x => x.KODU == beyanname_tur_key && x.FIRMA_ID == firma_id).Count();

                if (beyanname_durum <= 0)
                {
                    model.Donem_Adi = "Verilmedi";
                }

                else
                {
                    var bey_model = (from m in data_beyanname_listesi where m.KODU == beyanname_tur_key && m.FIRMA_ID == firma_id orderby m.GONDERIM_TARIHI descending select m).ToList()[0];
                    model.Donem_Adi = string.Format("{0:dd/MM/yy}", bey_model.GONDERIM_TARIHI);
                    model.Beyanname_Id = bey_model.ID;
                }
            }





            return model;
        }

        private Firma_Beyanname_Takip Get_Firma_Beyanname_Takip(int? firma_id, BeyannameTurEnums beyanname_tur)
        {

            Firma_Beyanname_Takip model = null;

            try
            {
                var tur_id = Convert.ToInt32(beyanname_tur);
                var result = data_firma_beyanname_takip_listesi.Where(x => x.Firma_Id == firma_id && x.Beyanname_Tur_Id == tur_id).SingleOrDefault();

                if (result != null)
                {
                    model = result;
                }
            }
            catch
            {
                model = null;
                return model;
            }


            return model;
        }

        private string Get_Beyanname_Tur_Key(BeyannameTurEnums beyanname_tur)
        {

            var model = data_beyanname_tur_listesi.Where(x => x.Id == Convert.ToInt32(beyanname_tur)).SingleOrDefault();

            return model.Tur_Key;
        }


    }
}
