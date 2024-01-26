using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;

namespace DataIslemler.Islemler.Beyanname
{
    public class DosyaIslem
    {
        private Musavir_DbDataContext data;

        private List<DOSYA> data_dosya_list;

        public DosyaIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_dosya_list = data.DOSYAs.ToList();
        }

        public Tacmin.Data.DbModel.DOSYA GetDosya(int id)
        {

            var data_model = data_dosya_list.Where(x => x.ID == id).SingleOrDefault();

            var model = new Tacmin.Data.DbModel.DOSYA();

            model.ID = data_model.ID;
            model.DOSYA_ADI = data_model.DOSYA_ADI;
            model.ICERIK = data_model.ICERIK.ToArray();
            model.MIME_TYPE = data_model.MIME_TYPE;
            model.SIZE = data_model.SIZE;


            return model;
        }
    }
}
