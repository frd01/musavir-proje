using DataIslemler.Data;
using DataIslemler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Tacmin.Model.Mukellef;

namespace DataIslemler.Islemler.Mukellef
{
    public class BeyannameTakipIslem
    {

        Musavir_DbDataContext data;
        List<Firma_Beyanname_Takip> data_beyanname_takip_listesi;

        public BeyannameTakipIslem(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_beyanname_takip_listesi = data.Firma_Beyanname_Takips.ToList();
        }

        public void MukellefTakipBilgiKayit(BeyannameTakipModel item)
        {
            dataIslem(item, BeyannameTurEnums.Ba);
            dataIslem(item, BeyannameTurEnums.DAMGA);
            dataIslem(item, BeyannameTurEnums.FORMBS);
            dataIslem(item, BeyannameTurEnums.GELIR);
            dataIslem(item, BeyannameTurEnums.GELIR1001E);
            dataIslem(item, BeyannameTurEnums.GGECICI);
            dataIslem(item, BeyannameTurEnums.KDV1);
            dataIslem(item, BeyannameTurEnums.KDV2);
            dataIslem(item, BeyannameTurEnums.KDV4);
            dataIslem(item, BeyannameTurEnums.KGECICI);
            dataIslem(item, BeyannameTurEnums.KURUMLAR);
            dataIslem(item, BeyannameTurEnums.MUHSGK);
            dataIslem(item, BeyannameTurEnums.MUHSGK2);
            dataIslem(item, BeyannameTurEnums.OIV);
            dataIslem(item, BeyannameTurEnums.OTV1);
            dataIslem(item, BeyannameTurEnums.OTV3A);
            dataIslem(item, BeyannameTurEnums.OTV3B);
            dataIslem(item, BeyannameTurEnums.OTV4);
            dataIslem(item, BeyannameTurEnums.POSET);
            dataIslem(item, BeyannameTurEnums.TURIZM);

        }
        public void TakipListesiKayit(List<BeyannameTakipModel> beyanname_takip_listesi)
        {

            foreach (var item in beyanname_takip_listesi)
            {
                dataIslem(item, BeyannameTurEnums.Ba);
                dataIslem(item, BeyannameTurEnums.DAMGA);
                dataIslem(item, BeyannameTurEnums.FORMBS);
                dataIslem(item, BeyannameTurEnums.GELIR);
                dataIslem(item, BeyannameTurEnums.GELIR1001E);
                dataIslem(item, BeyannameTurEnums.GGECICI);
                dataIslem(item, BeyannameTurEnums.KDV1);
                dataIslem(item, BeyannameTurEnums.KDV2);
                dataIslem(item, BeyannameTurEnums.KDV4);
                dataIslem(item, BeyannameTurEnums.KGECICI);
                dataIslem(item, BeyannameTurEnums.KURUMLAR);
                dataIslem(item, BeyannameTurEnums.MUHSGK);
                dataIslem(item, BeyannameTurEnums.MUHSGK2);
                dataIslem(item, BeyannameTurEnums.OIV);
                dataIslem(item, BeyannameTurEnums.OTV1);
                dataIslem(item, BeyannameTurEnums.OTV3A);
                dataIslem(item, BeyannameTurEnums.OTV3B);
                dataIslem(item, BeyannameTurEnums.OTV4);
                dataIslem(item, BeyannameTurEnums.POSET);
                dataIslem(item, BeyannameTurEnums.TURIZM);

            }
        }

        private void dataIslem(BeyannameTakipModel item, BeyannameTurEnums beyanname_tur)
        {

            var model = Get_Takip_Id(item.Id, beyanname_tur);

            model.Firma_Id = item.Id;

            model.Beyanname_Tur_Id = Convert.ToInt32(beyanname_tur);
            if (beyanname_tur == BeyannameTurEnums.Ba)
                model.Beyanname_Donem_Id = item.Ba.Id;
            if (beyanname_tur == BeyannameTurEnums.DAMGA)
                model.Beyanname_Donem_Id = item.Damga.Id;
            if (beyanname_tur == BeyannameTurEnums.FORMBS)
                model.Beyanname_Donem_Id = item.Bs.Id;
            if (beyanname_tur == BeyannameTurEnums.GELIR)
                model.Beyanname_Donem_Id = item.Gelir.Id;
            if (beyanname_tur == BeyannameTurEnums.GELIR1001E)
                model.Beyanname_Donem_Id = item.Gelir_1001E.Id;
            if (beyanname_tur == BeyannameTurEnums.GGECICI)
                model.Beyanname_Donem_Id = item.G_Gecici.Id;
            if (beyanname_tur == BeyannameTurEnums.KDV1)
                model.Beyanname_Donem_Id = item.Kdv1.Id;
            if (beyanname_tur == BeyannameTurEnums.KDV2)
                model.Beyanname_Donem_Id = item.Kdv2.Id;
            if (beyanname_tur == BeyannameTurEnums.KDV4)
                model.Beyanname_Donem_Id = item.Kdv4.Id;
            if (beyanname_tur == BeyannameTurEnums.KGECICI)
                model.Beyanname_Donem_Id = item.K_Gecici.Id;
            if (beyanname_tur == BeyannameTurEnums.KURUMLAR)
                model.Beyanname_Donem_Id = item.Kurumlar.Id;
            if (beyanname_tur == BeyannameTurEnums.MUHSGK)
                model.Beyanname_Donem_Id = item.Muh_Sgk.Id;
            if (beyanname_tur == BeyannameTurEnums.MUHSGK2)
                model.Beyanname_Donem_Id = item.Muh_Sgk2.Id;
            if (beyanname_tur == BeyannameTurEnums.OIV)
                model.Beyanname_Donem_Id = item.Oiv.Id;
            if (beyanname_tur == BeyannameTurEnums.OTV1)
                model.Beyanname_Donem_Id = item.Otv1.Id;
            if (beyanname_tur == BeyannameTurEnums.OTV3A)
                model.Beyanname_Donem_Id = item.Otv3A.Id;
            if (beyanname_tur == BeyannameTurEnums.OTV3B)
                model.Beyanname_Donem_Id = item.Otv3B.Id;
            if (beyanname_tur == BeyannameTurEnums.OTV4)
                model.Beyanname_Donem_Id = item.Otv4.Id;
            if (beyanname_tur == BeyannameTurEnums.POSET)
                model.Beyanname_Donem_Id = item.Poset.Id;
            if (beyanname_tur == BeyannameTurEnums.TURIZM)
                model.Beyanname_Donem_Id = item.Turizm.Id;


            if (model.Id == 0)
            {
                data.Firma_Beyanname_Takips.InsertOnSubmit(model);
                data.SubmitChanges();
            }
            if (model.Id != 0)
            {
                data.SubmitChanges();
            }


        }

        private Firma_Beyanname_Takip Get_Takip_Id(int? firma_id, BeyannameTurEnums beyanname_tur)
        {

            var model = new Firma_Beyanname_Takip();
            model.Id = 0;
            try
            {
                var tur_id = Convert.ToInt32(beyanname_tur);
                var data_model = (from m in data_beyanname_takip_listesi where m.Firma_Id == firma_id && m.Beyanname_Tur_Id == tur_id select m).SingleOrDefault();

                if (data_model != null)
                {

                    model = data_model;
                }
            }
            catch
            {

                model = new Firma_Beyanname_Takip();
                model.Id = 0;
            }

            return model;
        }

    }
}
