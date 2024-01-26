using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;

namespace DataIslemler.Listeler.Bildirge
{
    public class Listeler
    {
        private Musavir_DbDataContext data;
        private List<Whats_App_Gonderileri> data_gonderim_listesi;
        private List<Mail_Gonderileri> data_mail_gonderi_listesi;
        private List<Sms_Gonderileri> data_sms_gonderi_listesi;

        public Listeler(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
            data_gonderim_listesi = data.Whats_App_Gonderileris.ToList();
            data_mail_gonderi_listesi = data.Mail_Gonderileris.ToList();
            data_sms_gonderi_listesi = data.Sms_Gonderileris.Where(x => x.ModulId == (int)ModulEnums.BILDIRGE).ToList();
        }

        public List<BildirgeAraModel> GetBildirgeListesi(DateTime? tarih=null)
        {

            var liste = new List<BildirgeAraModel>();

            if (tarih==null)
            {
                foreach (var item in data.GetBildirgeListesi())
                {
                    var model = new BildirgeAraModel();

                    model.BELGE_CESIDI = item.BELGE_CESIDI;
                    model.BELGE_DURUM = item.BELGE_DURUM;
                    model.DONEM = item.DONEM;
                    model.ID = item.ID;
                    model.KANUN_NO = item.KANUN_NO;
                    model.SICIL_NO = item.SICIL_NO;
                    model.TCKN = item.TCKN;
                    model.TUTAR = item.TUTAR;
                    model.UNVAN = item.UNVAN;
                    model.VADE = item.VADE;
                    model.FILE_ID = item.FILE_ID;
                    model.TAH_FILE_ID = item.TAH_FILE_ID;
                    model.OID = item.OID;
                    model.TAH_OID = item.TAH_OID;
                    model.FIRMA_ID = item.FIRMA_ID;
                    model.Gonderim_Whats_App = WhatsAppGonderimDurum(item.ID);
                    model.Whats_App_Css = "whats_app_pasif";
                    if (model.Gonderim_Whats_App == true)
                        model.Whats_App_Css = "whats_app_aktif";
                    model.Whats_App_Gonderim_Baslik = WhatsAppGonderimZamani(item.ID);

                    model.Gonderim_Mail = MailGonderimDurum(item.ID);
                    model.Mail_Css = "mail_pasif";
                    model.Mail_Gonderim_Baslik = MailGonderimZamani(item.ID);
                    if (model.Gonderim_Mail == true)
                    {
                        model.Mail_Css = "mail_aktif";
                    }
                    model.Gonderim_Sms = SmsGonderimDurum(item.ID);
                    model.Sms_Css = "sms_pasif";
                    model.Sms_Baslik = "";
                    if (model.Gonderim_Sms == true)
                    {
                        model.Sms_Baslik = SmsGonderimZamani(item.ID);
                        model.Sms_Css = "sms_aktif";
                    }

                    liste.Add(model);
                }
            }

            if (tarih!=null)
            {

                var data_list = data.BILDIRGE_ICERIKLERIs.Where(x => x.SorguKayitTarihi == tarih).ToList();

                foreach (var item in data_list)
                {
                    var model = new BildirgeAraModel();

                    model.BELGE_CESIDI = item.BELGE_CESIDI;
                    model.BELGE_DURUM = item.BELGE_DURUM;
                    model.DONEM = item.DONEM;
                    model.ID = item.ID;
                    model.KANUN_NO = item.KANUN_NO;
                    model.SICIL_NO = item.SICIL_NO;
                    model.TCKN = item.TCKN;
                    model.TUTAR = item.TUTAR;
                    model.UNVAN = item.UNVAN;
                    model.VADE = item.VADE;
                    model.FILE_ID = item.FILE_ID;
                    model.TAH_FILE_ID = item.TAH_FILE_ID;
                    model.OID = item.OID;
                    model.TAH_OID = item.TAH_OID;
                    model.FIRMA_ID = item.FIRMA_ID;
                    model.Gonderim_Whats_App = WhatsAppGonderimDurum(item.ID);
                    model.Whats_App_Css = "whats_app_pasif";
                    if (model.Gonderim_Whats_App == true)
                        model.Whats_App_Css = "whats_app_aktif";
                    model.Whats_App_Gonderim_Baslik = WhatsAppGonderimZamani(item.ID);

                    model.Gonderim_Mail = MailGonderimDurum(item.ID);
                    model.Mail_Css = "mail_pasif";
                    model.Mail_Gonderim_Baslik = MailGonderimZamani(item.ID);
                    if (model.Gonderim_Mail == true)
                    {
                        model.Mail_Css = "mail_aktif";
                    }
                    model.Gonderim_Sms = SmsGonderimDurum(item.ID);
                    model.Sms_Css = "sms_pasif";
                    model.Sms_Baslik = "";
                    if (model.Gonderim_Sms == true)
                    {
                        model.Sms_Baslik = SmsGonderimZamani(item.ID);
                        model.Sms_Css = "sms_aktif";
                    }

                    liste.Add(model);
                }
            }

            return liste;
        }

        private bool WhatsAppGonderimDurum(int? id)
        {

            var kontrol = data_gonderim_listesi.Where(x => x.EvrakId == id).ToList();

            if (kontrol.Count > 0)
            {
                return true;
            }

            return false;
        }

        private string WhatsAppGonderimZamani(int? id)
        {
            var liste = data_gonderim_listesi.Where(x => x.EvrakId == id).ToList();

            if (liste.Count > 0)
            {
                var tarih = liste[0].TarihZaman;

                return tarih.Value.ToString("dd-MM-yyyy HH:mm");
            }

            return "";
        }

        private string MailGonderimZamani(int? id)
        {
            var liste = data_mail_gonderi_listesi.Where(x => x.EvrakId == id).ToList();

            if (liste.Count > 0)
            {
                var tarih = liste[0].TarihZaman;

                return tarih.Value.ToString("dd-MM-yyyy HH:mm");
            }

            return "";
        }
        private bool MailGonderimDurum(int? id)
        {
            var durum = false;

            var kontrol = data_mail_gonderi_listesi.Where(x => x.EvrakId == id).ToList();

            if (kontrol.Count > 0)
            {
                durum = true;
            }

            return durum;
        }

        private string SmsGonderimZamani(int? id)
        {
            var liste = data_sms_gonderi_listesi.Where(x => x.EvrakId == id).ToList();

            if (liste.Count > 0)
            {
                var tarih = liste[0].TarihZaman;

                return tarih.Value.ToString("dd-MM-yyyy HH:mm");
            }

            return "";
        }
        private bool SmsGonderimDurum(int? id)
        {
            var durum = false;

            var kontrol = data_sms_gonderi_listesi.Where(x => x.EvrakId == id).ToList();

            if (kontrol.Count > 0)
            {
                durum = true;
            }

            return durum;
        }
    }
}
