using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Model.Rapor;
using Tacmin.Model.Whatsapp;

namespace Tacmin.Service
{
    public class RaporService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.Whatsapp.RaporIslem raporIslem;
        private DataIslemler.Rapor.ListeIslem raporListeIslem;

        public RaporService(IVirtualContext _vctx)
        {
            vctx = _vctx;
            raporIslem = new DataIslemler.Whatsapp.RaporIslem(vctx.DataAdi);

            raporListeIslem = new DataIslemler.Rapor.ListeIslem(vctx.DataAdi);
        }

        public List<GonderimBilgi> Whats_App_GonderimRaporu()
        {
            var result = raporIslem.GonderimRaporu();

            return result;
        }
        public List<GonderimBilgi> Sms_GonderimRaporu()
        {
            var result = raporIslem.SmsGonderimRaporu();

            return result;
        }

        public List<GonderimBilgi>Mail_GonderimRaporu()
        {
            var result = raporIslem.MailGonderimRaporu();

            return result;
        }
        public List<GonderimBilgi> Whats_App_GonderimRaporuTarih(DateTime? ilkTarih, DateTime? sonTarih)
        {
            var result = raporIslem.GonderimRaporuTarih(ilkTarih, sonTarih);

            return result;
        }

        public List<GonderimBilgi> Mail_GonderimRaporuTarih(DateTime? ilkTarih, DateTime? sonTarih)
        {
            var result = raporIslem.MailGonderimRaporuTarih(ilkTarih, sonTarih);

            return result;
        }

        public List<GonderimBilgi>Sms_GonderimRaporuTarih(DateTime? ilkTarih, DateTime? sonTarih)
        {
            var result = raporIslem.SmsGonderimRaporuTarih(ilkTarih, sonTarih);

            return result;
        }

        public List<GunlukIslemModel> GunlukIslemListesi()
        {
            var result = raporListeIslem.GunlukIslemListesi();

            return result;
        }

        public List<RaporFirmaModel> SgkBilgisiEksikFirmalar()
        {

            var result = raporListeIslem.SgkBilgisiEksikFirmalar();

            return result;
        }

        public List<RaporFirmaModel> GibBilgisiEksikFirmalar()
        {
            var result = raporListeIslem.GibBilgisiEksikFirmalar();

            return result;
        }

        public List<RaporFirmaModel> IslemHataListesi()
        {
            var result = raporListeIslem.IslemHataListesi();

            return result;
        }
    }
}
