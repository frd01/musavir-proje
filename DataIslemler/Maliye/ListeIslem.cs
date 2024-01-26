using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Maliye;

namespace DataIslemler.Maliye
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;
        private List<Vergi_Daireleri> data_vergi_daire_listesi;

        public ListeIslem(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_vergi_daire_listesi = data.Vergi_Daireleris.ToList();
        }

        public List<BankaHacizModel> BankaHacizListeVergiDairesiGuncelle(List<BankaHacizModel> clientList)
        {
            var liste = new List<BankaHacizModel>();

            foreach (var item in clientList)
            {
                item.vdkod = item.vdkod.Substring(1, item.vdkod.Length - 1);
                item.vergiDairesi = GetVergiDairesi(item.vdkod);
                liste.Add(item);

            }

            return liste;
        }

        public List<AracHacizModel> AracHacizListeGuncelle(List<AracHacizModel> clientList)
        {

            var liste = new List<AracHacizModel>();

            foreach (var item in clientList)
            {
                item.vdkod = item.vdkod.Substring(1, item.vdkod.Length - 1);
                item.vergiDairesi = GetVergiDairesi(item.vdkod);
                liste.Add(item);
            }

            return liste;
        }

        private string GetVergiDairesi(string vdKodu)
        {

            var model = data_vergi_daire_listesi.Where(x => x.Kodu == vdKodu).SingleOrDefault();

            if (model != null)
            {
                return model.Daire_Adi;
            }

            return "";
        }

       


    }
}
