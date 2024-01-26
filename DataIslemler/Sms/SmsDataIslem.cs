using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Helpers;
using DataIslemler.Data;
using Tacmin.Model;
using Tacmin.Model.Sms;

namespace DataIslemler.Sms
{
    public class SmsDataIslem
    {
        private Musavir_DbDataContext data;

        public SmsDataIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<BeyannameAraModel> SmsDataKayitIslem(List<SmsResModel> islemListesi,int modul, List<BeyannameAraModel> clientList)
        {
            var liste = new List<BeyannameAraModel>();

            try
            {
                foreach (var item in islemListesi)
                {
                    var model = new Sms_Gonderileri();
                    model.EvrakId = item.EvrakId;
                    model.EvrakTur = item.EvrakTur;
                    model.FirmaId = item.FirmaId;
                    model.KisiId = item.KisiId;
                    model.SmsFirmaId = item.SmsFirmaId;
                    model.Tarih = DateTime.Now;
                    model.TarihZaman = model.Tarih;
                    model.ModulId = modul;

                    data.Sms_Gonderileris.InsertOnSubmit(model);
                    data.SubmitChanges();

                    var liste_model = clientList.Where(x => x.ID == model.EvrakId).SingleOrDefault();
                    liste_model.Sms_Baslik = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
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
