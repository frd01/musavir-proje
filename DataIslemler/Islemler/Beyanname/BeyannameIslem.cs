using DataIslemler.Data;
using DataIslemler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataIslemler.Islemler.Beyanname
{
    public class BeyannameIslem
    {
        Musavir_DbDataContext data;

        public BeyannameIslem(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public void BeyannameSil(List<int> beyanname_listesi)
        {

            var data_list = new List<Data.BEYANNAME_ICERIKLERI>();

            foreach (var id in beyanname_listesi)
            {
                var model = data.BEYANNAME_ICERIKLERIs.Where(x => x.ID == id).SingleOrDefault();

               if(model.FILE_ID!=null) DosyaSil(model.FILE_ID.Value);
                if (model.TAH_FILE_ID != null)
                    DosyaSil(model.TAH_FILE_ID.Value);

                BildirgeSil(model.OID);

                data_list.Add(model);
            }

            data.BEYANNAME_ICERIKLERIs.DeleteAllOnSubmit(data_list);
            data.SubmitChanges();
        }

        private void DosyaSil(int id)
        {
            var dosya_list = data.DOSYAs.Where(x => x.ID == id).ToList();

            if (dosya_list.Count()>0)
            {
                var model = dosya_list[0];

                data.DOSYAs.DeleteOnSubmit(model);
                data.SubmitChanges();
            }
        }

        private void BildirgeSil(string beyannameId)
        {

            var bildirge_list = data.BILDIRGE_ICERIKLERIs.Where(x => x.OID == beyannameId).ToList();

            data.BILDIRGE_ICERIKLERIs.DeleteAllOnSubmit(bildirge_list);
            data.SubmitChanges();
        }

        public string Get_Beyanname_Kodu(int id)
        {

            var kod = "";

            try
            {
                var model = data.Maliye_Vergi_Turleris.Where(x => x.Id == id).SingleOrDefault();

                if (model != null)
                {

                    kod = model.Beyanname_Kodu;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return kod;
        }
    }
}
