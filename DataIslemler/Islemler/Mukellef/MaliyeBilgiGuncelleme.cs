using DataIslemler.Data;
using DataIslemler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataIslemler.Islemler.Mukellef
{
    public class MaliyeBilgiGuncelleme
    {
        Musavir_DbDataContext data;

        List<FIRMA_TANIMLARI> data_firma_list;

        public MaliyeBilgiGuncelleme(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_firma_list = data.FIRMA_TANIMLARIs.ToList();
        }

        public void FirmaBilgiGuncelle(Tacmin.Model.Mukellef.MaliyeBilgiModel client_data)
        {

            foreach (var item in client_data.data.sozlesme)
            {
                DataGuncelle(item);
            }
        }

        private void DataGuncelle(Tacmin.Model.Mukellef.MaliyeSozlesme client_data)
        {

            try
            {

                var data_model = data_firma_list.Where(x => x.VERGI_NO.ToString() == client_data.mukvkn).SingleOrDefault();

                if (data_model != null)
                {

                    data_model.VERGI_DAIRESI = client_data.mukellefvdkodutext;
                    data_model.TCKN = client_data.muktckn;

                    data.SubmitChanges();

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }


    }
}
