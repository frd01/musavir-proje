using DataIslemler.Data;
using DataIslemler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Tacmin.Model.Mukellef;

namespace DataIslemler.Listeler.Mukellef
{
    public class BeyannameTakip
    {
        Musavir_DbDataContext data;
        List<FIRMA_TANIMLARI> firma_listesi;
        List<Firma_Beyanname_Takip> data_beyanname_takip_listesi;
        List<BeyannameDonemModel> beyanname_donem_listesi;

        public BeyannameTakip(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            firma_listesi = data.FIRMA_TANIMLARIs.Where(x => x.AKTIF == true).ToList();
            data_beyanname_takip_listesi = data.Firma_Beyanname_Takips.ToList();

            beyanname_donem_listesi = (from m in data.Beyanname_Donemlers select new BeyannameDonemModel { Id = m.Id, Donem_Adi = m.Donem }).ToList();
        }

        public List<BeyannameTakipModel> BeyannameTakipListesi()
        {

            var liste = new List<BeyannameTakipModel>();

            foreach (var item in firma_listesi)
            {
                var model = new BeyannameTakipModel();

                model.Id = item.ID;
                model.Unvan = item.UNVAN;
                model.Kdv1 = GetDonem(BeyannameTurEnums.KDV1, item.ID);
                model.Kdv2 = GetDonem(BeyannameTurEnums.KDV2, item.ID);
                model.Kdv4 = GetDonem(BeyannameTurEnums.KDV4, item.ID);
                model.Muh_Sgk = GetDonem(BeyannameTurEnums.MUHSGK, item.ID);
                model.Muh_Sgk2 = GetDonem(BeyannameTurEnums.MUHSGK2, item.ID);
                model.G_Gecici = GetDonem(BeyannameTurEnums.GGECICI, item.ID);
                model.K_Gecici = GetDonem(BeyannameTurEnums.KGECICI, item.ID);
                model.Ba = GetDonem(BeyannameTurEnums.Ba, item.ID);
                model.Gelir = GetDonem(BeyannameTurEnums.GELIR, item.ID);
                model.Gelir_1001E = GetDonem(BeyannameTurEnums.GELIR1001E, item.ID);
                model.Kurumlar = GetDonem(BeyannameTurEnums.KURUMLAR, item.ID);
                model.Otv1 = GetDonem(BeyannameTurEnums.OTV1, item.ID);
                model.Otv3A = GetDonem(BeyannameTurEnums.OTV3A, item.ID);
                model.Otv3B = GetDonem(BeyannameTurEnums.OTV3B, item.ID);
                model.Otv4 = GetDonem(BeyannameTurEnums.OTV4, item.ID);
                model.Oiv = GetDonem(BeyannameTurEnums.OIV, item.ID);
                model.Turizm = GetDonem(BeyannameTurEnums.TURIZM, item.ID);
                model.Poset = GetDonem(BeyannameTurEnums.POSET, item.ID);
                model.Damga = GetDonem(BeyannameTurEnums.DAMGA, item.ID);
                model.Bs = GetDonem(BeyannameTurEnums.FORMBS, item.ID);

                liste.Add(model);
            }

            return liste;
        }

        private BeyannameDonemModel GetDonem(BeyannameTurEnums beyanname_tur, int? firma_id)
        {
            var model = new BeyannameDonemModel();

            var tur_id = Convert.ToInt32(beyanname_tur);

            try
            {
                var takip_data = data_beyanname_takip_listesi.Where(x => x.Firma_Id == firma_id && x.Beyanname_Tur_Id == tur_id).SingleOrDefault();

                if (takip_data != null)
                {

                    model = beyanname_donem_listesi.Where(x => x.Id == takip_data.Beyanname_Donem_Id).SingleOrDefault();
                    return model;
                }

                model.Id = 1;
                model.Donem_Adi = "YOK";

                if (firma_id == 1171 && beyanname_tur == BeyannameTurEnums.KDV1)
                {
                    model.Id = 4;
                    model.Donem_Adi = "YILLIK";
                }
            }
            catch
            {

                model = new BeyannameDonemModel();
            }

            return model;

        }
    }
}
