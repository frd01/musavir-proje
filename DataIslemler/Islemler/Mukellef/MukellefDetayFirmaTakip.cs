using DataIslemler.Data;
using DataIslemler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Tacmin.Model.Mukellef;

namespace DataIslemler.Islemler.Mukellef
{
    public class MukellefDetayFirmaTakip
    {
        Musavir_DbDataContext data;

        List<Beyanname_Donemler> data_beyanname_donem_listesi;
        List<Get_Firma_Beyanname_Takip_ListesiResult> data_beyanname_takip_listesi;

        public MukellefDetayFirmaTakip(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public BeyannameTakipModel GetMukellefFirmaBilgiModel(int? mukellef_id)
        {


            var model = new BeyannameTakipModel();

            if (mukellef_id != null)
            {
                data_beyanname_donem_listesi = data.Beyanname_Donemlers.ToList();
                data_beyanname_takip_listesi = data.Get_Firma_Beyanname_Takip_Listesi().ToList();

                var data_firma = data.FIRMA_TANIMLARIs.Where(x => x.ID == mukellef_id).SingleOrDefault();

                model.Id = data_firma.ID;
                model.Unvan = data_firma.UNVAN;
            }

            model.Ba = GetDonemModel(mukellef_id, BeyannameTurEnums.Ba);
            model.Bs = GetDonemModel(mukellef_id, BeyannameTurEnums.FORMBS);
            model.Damga = GetDonemModel(mukellef_id, BeyannameTurEnums.DAMGA);
            model.Gelir = GetDonemModel(mukellef_id, BeyannameTurEnums.GELIR);
            model.Gelir_1001E = GetDonemModel(mukellef_id, BeyannameTurEnums.GELIR1001E);
            model.G_Gecici = GetDonemModel(mukellef_id, BeyannameTurEnums.GGECICI);
            model.Kdv1 = GetDonemModel(mukellef_id, BeyannameTurEnums.KDV1);
            model.Kdv2 = GetDonemModel(mukellef_id, BeyannameTurEnums.KDV2);
            model.Kdv4 = GetDonemModel(mukellef_id, BeyannameTurEnums.KDV4);
            model.Kurumlar = GetDonemModel(mukellef_id, BeyannameTurEnums.KURUMLAR);
            model.K_Gecici = GetDonemModel(mukellef_id, BeyannameTurEnums.KGECICI);
            model.Muh_Sgk = GetDonemModel(mukellef_id, BeyannameTurEnums.MUHSGK);
            model.Muh_Sgk2 = GetDonemModel(mukellef_id, BeyannameTurEnums.MUHSGK2);
            model.Oiv = GetDonemModel(mukellef_id, BeyannameTurEnums.OIV);
            model.Otv1 = GetDonemModel(mukellef_id, BeyannameTurEnums.OTV1);
            model.Otv3A = GetDonemModel(mukellef_id, BeyannameTurEnums.OTV3A);
            model.Otv3B = GetDonemModel(mukellef_id, BeyannameTurEnums.OTV3B);
            model.Otv4 = GetDonemModel(mukellef_id, BeyannameTurEnums.OTV4);
            model.Poset = GetDonemModel(mukellef_id, BeyannameTurEnums.POSET);
            model.Turizm = GetDonemModel(mukellef_id, BeyannameTurEnums.TURIZM);


            return model;
        }

        private BeyannameDonemModel GetDonemModel(int? mukellef_id, BeyannameTurEnums beyanname_tur)
        {
            var tur_id = Convert.ToInt32(beyanname_tur);
            if (mukellef_id != null)
            {
                var beyanname_takip = data_beyanname_takip_listesi.Where(x => x.Firma_Id == mukellef_id && x.Beyanname_Tur_Id == tur_id).SingleOrDefault();

                if (beyanname_takip == null)
                    return Get_Bos_Donem_Model();

                return Get_Dolu_Donem_Model(beyanname_takip.Beyanname_Donem_Id);
            }

            return Get_Bos_Donem_Model();


        }

        private BeyannameDonemModel Get_Bos_Donem_Model()
        {

            var model = new BeyannameDonemModel();
            model.Id = 1;
            model.Donem_Adi = "YOK";

            return model;
        }

        private BeyannameDonemModel Get_Dolu_Donem_Model(int? donem_id)
        {

            var model = new BeyannameDonemModel();

            var data_model = data_beyanname_donem_listesi.Where(x => x.Id == donem_id).SingleOrDefault();

            model.Id = data_model.Id;
            model.Donem_Adi = data_model.Donem;

            return model;
        }


    }
}
