using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Model.MaliyeIslemleri.Beyanname;
using DataIslemler.Data;
using DataIslemler.Helpers;

namespace DataIslemler.Listeler.Beyanname
{
    public class Listeler
    {

        private Musavir_DbDataContext data;

        public Listeler(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<BeyannameTurModel> GetBeyannameTurListesi()
        {

            var liste = new List<BeyannameTurModel>();

            var id = 1;

            foreach (var item in data.GetBeyannameTurGroupList())
            {
                var model = new BeyannameTurModel();

                model.Id = id;
                model.Kod = item.KODU;

                liste.Add(model);

                id++;
            }

            return liste;
        }
    }
}
