using DataIslemler.Data;
using DataIslemler.Helpers;
using System.Collections.Generic;

namespace DataIslemler.Listeler.Mukellef
{
    public class GibListeler
    {
        Musavir_DbDataContext data;

        public GibListeler(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<Tacmin.Model.Mukellef.FirmaGibModel> FirmaBeyannameSorguListe()
        {
            var liste = new List<Tacmin.Model.Mukellef.FirmaGibModel>();

            foreach (var item in data.FIRMA_TANIMLARIs)
            {
                var model = new Tacmin.Model.Mukellef.FirmaGibModel();

                model.Id = item.ID;
                model.Unvan = item.UNVAN;
                model.Vergi_No = item.VERGI_NO;
                model.Tc_No = item.TCKN;

                if (item.UNVAN.Contains("LTD") || item.UNVAN.Contains("LİMİTED") || item.UNVAN.Contains("A.Ş."))
                {
                    model.Firma_Durum = "Şirket";
                }
                else
                {
                    model.Firma_Durum = "Şahıs";
                }

                liste.Add(model);


            }

            return liste;
        }
    }
}
