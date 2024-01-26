using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Mail;
using Tacmin.Model.Whatsapp;

namespace DataIslemler.Mail
{
    public class ListeIslem
    {
        private DataIslemler.AdminData.Yonetim_DbDataContext adminData;

        private string dataAdi = "";

        public ListeIslem(string _dataAdi)
        {
            dataAdi = _dataAdi;

            var adm_str = new Baglanti(dataAdi).AdmConStr;

            adminData = new AdminData.Yonetim_DbDataContext(adm_str);
        }

        public MailKullaniciModel MailBilgi(int kullaniciId)
        {
            var item = adminData.KULLANICI_TANIMLARIs.Where(x => x.ID == kullaniciId).SingleOrDefault();

            if (item == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(item.MailKullaniciAdi) || string.IsNullOrEmpty(item.MailSifre))
            {
                return null;
            }

            var model = new MailKullaniciModel();

            model.Unvan = item.Unvan;
            model.MailKullaniciAdi = item.MailKullaniciAdi;
            model.MailSifre = item.MailSifre;
            model.Id = item.ID;
            if (model.MailKullaniciAdi.Contains("hotmail"))
            {
                model.MailTur = (int)MailTurEnums.HOTMAIL;
            }
            else if (model.MailKullaniciAdi.Contains("gmail"))
            {
                model.MailTur = (int)MailTurEnums.GMAIL;
            }
            else
            {
                model.MailTur = (int)MailTurEnums.OZEL;
            }

            return model;
        }

        public List<FirmaModel> BeyannameGonderimIcerikOlustur(List<BeyannameAraModel> clientList, string gonderimTur)
        {
           
            var islem = new DataIslemler.Whatsapp.BeyannameIslem(dataAdi);

            var liste = islem.GonderimIcerikOlustur(clientList, gonderimTur);

            return liste;
        }

        public List<FirmaModel> BildirgeGonderimIcerikOlustur(List<BildirgeAraModel> clientList, string gonderimTur)
        {

            var islem = new DataIslemler.Whatsapp.BildirgeIslem(dataAdi);

            var liste = islem.GonderimIcerikOlustur(clientList, gonderimTur);

            return liste;
        }
    }
}
