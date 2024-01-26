using DataIslemler.Data;
using DataIslemler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataIslemler.Islemler.Beyanname
{
    public class VergiDairesiIslem
    {
        Musavir_DbDataContext data;
        List<Vergi_Daireleri> vergi_daire_listesi;


        public VergiDairesiIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            vergi_daire_listesi = data.Vergi_Daireleris.ToList();
        }

        public string GetVergiDairesi(string _vergi_daire_kodu)
        {

            var vergi_daire_kodu = Convert.ToInt32(_vergi_daire_kodu).ToString();

            var vergi_dairesi = "";

            try
            {
                var model = vergi_daire_listesi.Where(x => x.Kodu == vergi_daire_kodu).SingleOrDefault();

                vergi_dairesi = model.Daire_Adi;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return vergi_dairesi;
        }
    }
}
