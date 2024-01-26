using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Mail;

namespace DataIslemler.Mail
{
    public class DataIslem
    {
        private Musavir_DbDataContext data;

        public DataIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<BeyannameAraModel> MailDataKayitIslem(List<MailResModel> islemListesi,int modul, List<BeyannameAraModel> clientList)
        {
            var liste = new List<BeyannameAraModel>();

            

            try
            {
                foreach (var item in islemListesi)
                {
                    var model = new Mail_Gonderileri();
                    model.EvrakId = item.EvrakId;
                    model.EvrakTur = item.EvrakTur;
                    model.FirmaId = item.FirmaId;
                    model.KisiId = item.KisiId;
                    model.ModulId = modul;
                    model.Tarih = DateTime.Now;
                    model.TarihZaman = model.Tarih;

                    data.Mail_Gonderileris.InsertOnSubmit(model);
                    data.SubmitChanges();

                    var liste_model = clientList.Where(x => x.ID == model.EvrakId).SingleOrDefault();
                    liste_model.Mail_Gonderim_Baslik = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                    liste.Add(liste_model);
                }

               
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return liste;
        }
    }
}
