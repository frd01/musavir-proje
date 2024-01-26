using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;

namespace DataIslemler.KontrolIslem
{
    public class DataIslem
    {
        private Musavir_DbDataContext data;

        public DataIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public bool BeyannameKontrolIslem()
        {

            var beyanname_kontrol = BeyannameKontrol();

            if (beyanname_kontrol == false)
            {
                return false;
            }
            

            return BildirgeKontrol();
        }

        private bool BeyannameKontrol()
        {
            var liste = data.BEYANNAME_ICERIKLERIs.Where(x => x.KayitTarihi == DateTime.Now).ToList();

            liste = liste.Where(x => x.GONDERIM_TARIHI == null).ToList();

            if (liste.Count>0)
            {
                // bildirgeleri sil
                foreach (var item in liste)
                {
                    var data_list = data.BILDIRGE_ICERIKLERIs.Where(x => x.OID == item.OID).ToList();

                    data.BILDIRGE_ICERIKLERIs.DeleteAllOnSubmit(data_list);
                    data.SubmitChanges();
                }

                // beyannameleri sil
                foreach (var item in liste)
                {
                    var data_model = data.BEYANNAME_ICERIKLERIs.Where(x => x.ID == item.ID).SingleOrDefault();

                    data.BEYANNAME_ICERIKLERIs.DeleteOnSubmit(data_model);
                    data.SubmitChanges();
                }

                return false;
            }

            return true;
        }

        private bool BildirgeKontrol()
        {
            var liste = data.BILDIRGE_ICERIKLERIs.Where(x => x.KayitTarihi == DateTime.Now).ToList();

            liste = liste.Where(x => x.TUTAR == null).ToList();

            if (liste.Count>0)
            {
                //bildirge sil
                foreach (var item in liste)
                {
                    var data_model = data.BILDIRGE_ICERIKLERIs.Where(x => x.ID == item.ID).SingleOrDefault();

                    data.BILDIRGE_ICERIKLERIs.DeleteOnSubmit(data_model);
                    data.SubmitChanges();
                }

                //beyanname sil
                foreach (var item in liste)
                {
                    var data_list = data.BEYANNAME_ICERIKLERIs.Where(x => x.OID == item.OID).ToList();

                    data.BEYANNAME_ICERIKLERIs.DeleteAllOnSubmit(data_list);
                    data.SubmitChanges();
                }

                return false;
            }

            return true;
        }
    }
}
