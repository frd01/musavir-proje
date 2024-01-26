using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;

namespace DataIslemler.Islemler.Beyanname
{
    public class PdfIslem
    {
        private Musavir_DbDataContext data;

        public PdfIslem(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public Tacmin.Data.DbModel.DOSYA GetPdfDosya(int id)
        {

            var model = new Tacmin.Data.DbModel.DOSYA();

            var kontrol = data.DOSYAs.Where(x => x.ID == id).Count();

            if (kontrol > 0)
            {

                var data_model = data.DOSYAs.Where(x => x.ID == id).SingleOrDefault();

                model.DOSYA_ADI = data_model.DOSYA_ADI;
                model.ICERIK = data_model.ICERIK.ToArray();
                model.ID = data_model.ID;
                model.MIME_TYPE = data_model.MIME_TYPE;
                model.SIZE = data_model.SIZE;

            }

            return model;
        }
    }
}
