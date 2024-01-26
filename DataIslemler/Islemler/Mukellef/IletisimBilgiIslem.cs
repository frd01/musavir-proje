using DataIslemler.Data;
using DataIslemler.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DataIslemler.Islemler.Mukellef
{
    public class IletisimBilgiIslem
    {
        Musavir_DbDataContext data;

        public IletisimBilgiIslem(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public void ListeKayit(List<Tacmin.Data.DbModel.Firma_Iletisim> iletisim_listesi, int? firma_id)
        {

            iletisim_listesi_sil(firma_id);

            var data_liste = new List<Firma_Iletisim>();

            foreach (var item in iletisim_listesi)
            {
                var model = new Firma_Iletisim();

                model.Adi = item.Adi;
                model.Telefon = item.Telefon;
                model.Mail = item.Mail;
                model.Firma_Id = firma_id;

                data_liste.Add(model);
            }

            data.Firma_Iletisims.InsertAllOnSubmit(data_liste);
            data.SubmitChanges();
        }

        private void iletisim_listesi_sil(int? firma_id)
        {

            var data_list = data.Firma_Iletisims.Where(x => x.Firma_Id == firma_id).ToList();

            data.Firma_Iletisims.DeleteAllOnSubmit(data_list);
            data.SubmitChanges();
        }
    }
}
