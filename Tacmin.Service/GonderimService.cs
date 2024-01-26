using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Model;
using Tacmin.Model.Mail;
using Tacmin.Model.Sms;
using Tacmin.Model.Whatsapp;

namespace Tacmin.Service
{
    public class GonderimService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.Mail.ListeIslem mailListeIslem;
        private DataIslemler.Mail.DataIslem mailDataIslem;

        private DataIslemler.Sms.ListeIslem smsListeIslem;
        private DataIslemler.Sms.SmsDataIslem smsDataIslem;

        public GonderimService(IVirtualContext _vctx)
        {
            vctx = _vctx;

            mailListeIslem = new DataIslemler.Mail.ListeIslem(vctx.DataAdi);
            mailDataIslem = new DataIslemler.Mail.DataIslem(vctx.DataAdi);

            smsDataIslem = new DataIslemler.Sms.SmsDataIslem(vctx.DataAdi);
            smsListeIslem = new DataIslemler.Sms.ListeIslem(vctx.DataAdi);
        }

        public MailKullaniciModel MailBilgi()
        {
            var result = mailListeIslem.MailBilgi(vctx.UserId);

            return result;
        }

        public List<FirmaModel> MailBeyannaneGonderimIcerikOlustur(List<BeyannameAraModel> clientList, string gonderimTur)
        {
            

            var result = mailListeIslem.BeyannameGonderimIcerikOlustur(clientList, gonderimTur);

            return result;
        }

        public List<BeyannameAraModel> MailDataKayitIslem(List<MailResModel> islemListesi,int modul, List<BeyannameAraModel> clientList)
        {
            var result = mailDataIslem.MailDataKayitIslem(islemListesi,modul,clientList);

            return result;
        }

        public List<FirmaModel> SmsBeyannaneGonderimIcerikOlustur(List<BeyannameAraModel> clientList, string gonderimTur)
        {
            var result = smsListeIslem.BeyannaneGonderimIcerikOlustur(clientList, gonderimTur);

            return result;
        }

        public List<BeyannameAraModel> SmsDataKayitIslem(List<SmsResModel> islemListesi,int modul, List<BeyannameAraModel> clientList)
        {
            var result = smsDataIslem.SmsDataKayitIslem(islemListesi,modul, clientList);

            return result;
        }

        public SmsBilgiModel SmsBilgi()
        {
            var result = smsListeIslem.SmsBilgi(vctx.UserId);

            return result;
        }

        public List<FirmaModel> MailBildirgeGonderimIcerikOlustur(List<BildirgeAraModel> clientList, string gonderimTur)
        {
            var result = mailListeIslem.BildirgeGonderimIcerikOlustur(clientList, gonderimTur);

            return result;
        }

        public List<FirmaModel> SmsBildirgeGonderimIcerikOlustur(List<BildirgeAraModel> clientList, string gonderimTur)
        {

            var result = smsListeIslem.BildirgeGonderimIcerikOlustur(clientList, gonderimTur);

            return result;
        }
    }
}
