using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Data.DbModel;
using DataIslemler.Data;

namespace DataIslemler.Islemler.Mukellef
{
    public class VergiLevhaIslem
    {

        Musavir_DbDataContext data;
        public VergiLevhaIslem(string dataAdi)
        {

            var con_str = new Helpers.Baglanti(dataAdi).ConStr;
            data = new Musavir_DbDataContext(con_str);


        }
        public void DataKayit(List<Tacmin.Data.DbModel.Vergi_Levhalari> vergi_levha_listesi)
        {

            var data_list = new List<Data.Vergi_Levhalari>();

            foreach (var item in vergi_levha_listesi)
            {
                var model = new Data.Vergi_Levhalari();

                model.Beyan = item.Beyan;
                model.Dosya = item.Dosya;
                model.FirmaId = item.FirmaId;
                model.Matrah = item.Matrah;
                model.Yil = item.Yil;

               if(kayit_kontrol(item.FirmaId)) data_list.Add(model);

            }

            data.Vergi_Levhalaris.InsertAllOnSubmit(data_list);
            data.SubmitChanges();
        }

        private bool kayit_kontrol(int firma_id)
        {

            var durum = true;

            try
            {
                var model = data.Vergi_Levhalaris.Where(x => x.FirmaId == firma_id).SingleOrDefault();

                if (model != null)
                    durum = false;
            }
            catch 
            {

                durum = true;
            }

            return durum;
        }
    }
}
