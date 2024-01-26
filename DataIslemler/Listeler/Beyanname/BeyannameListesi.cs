using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.MaliyeIslemleri.Beyanname;

namespace DataIslemler.Listeler.Beyanname
{
    public class BeyannameListesi
    {
        Musavir_DbDataContext data;

        private List<GetBeyannameListesiResult> beyanname_liste;
        private List<GetBeyannameListesi_TarihResult> beyanname_liste_tarih;
        private List<Whats_App_Gonderileri> data_gonderim_listesi;
        private List<Mail_Gonderileri> data_mail_gonderi_listesi;
        private List<Sms_Gonderileri> data_sms_gonderi_listesi;

        public BeyannameListesi(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_gonderim_listesi = data.Whats_App_Gonderileris.ToList();
            data_mail_gonderi_listesi = data.Mail_Gonderileris.ToList();
            data_sms_gonderi_listesi = data.Sms_Gonderileris.Where(x => x.ModulId == (int)ModulEnums.BEYANNAME).ToList();
        }

        public List<BeyannameAraModel> GetBeyannameListesi(DateTime? tarih=null)
        {
            var sonTarih = DateTime.Now;

            var ilkTarih = new DateTime(sonTarih.Year, sonTarih.Month, 1);

            if (tarih !=null)
            {
                sonTarih = tarih.Value;
                ilkTarih = tarih.Value;
            }

            var data_list = data.GetBeyannameListesi_Tarih(ilkTarih, sonTarih);

            var liste = new List<BeyannameAraModel>();

            foreach (var item in data_list)
            {
                var model = new BeyannameAraModel();  

                model.ID = item.ID;
                model.DONEM_BAS = item.DONEM_BAS;
                model.DONEM_SON = item.DONEM_SON;
                model.FILE_ID = item.FILE_ID;
                model.GONDERIM_TARIHI = item.GONDERIM_TARIHI;
                model.KODU = item.KODU;
                model.TAH_FILE_ID = item.TAH_FILE_ID;
                model.TAH_FIS_NO = item.TAH_FIS_NO;
                model.TAH_TUTAR = item.TAH_TUTAR;
                model.TAH_VADE = item.TAH_VADE;
                model.TCKN = item.TCKN;
                model.TUR = item.TUR;
                model.UNVAN = item.UNVAN;
                model.VERGI_DAIRESI = item.VERGI_DAIRESI;
                model.FIRMA_ID = item.FIRMA_ID;
                model.OID = item.OID;
                model.TAH_OID = item.TAH_OID;
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

            return liste;

        }

        public List<BeyannameAraModel> GetDataSorguBeyannameListesi(BeyannameDataRequestModel client_data)
        {

            var liste = new List<BeyannameAraModel>();

            if(client_data.IlkTarih == null || client_data.SonTarih == null)
            {
                beyanname_liste = data.GetBeyannameListesi().ToList();

                if (client_data.Cariler.Count <= 0 && client_data.BeyannameTurler.Count>0)
                {
                    beyanname_liste = beyanname_liste.Where(x => client_data.BeyannameTurler.Select(b => b).Contains(x.KODU)).ToList();
                }
                if (client_data.Cariler.Count >0 && client_data.BeyannameTurler.Count <= 0)
                {
                    beyanname_liste = beyanname_liste.Where(x => client_data.Cariler.Select(c => c).Contains(x.FIRMA_ID.Value)).ToList();
                }
                if (client_data.Cariler.Count > 0 && client_data.BeyannameTurler.Count > 0)
                {
                    beyanname_liste = beyanname_liste.Where(x => client_data.Cariler.Select(c => c).Contains(x.FIRMA_ID.Value)
                    && client_data.BeyannameTurler.Select(b => b).Contains(x.KODU)
                    ).ToList();
                }

                foreach (var item in beyanname_liste)
                {
                    var model = new BeyannameAraModel();

                    model.ID = item.ID;
                    model.DONEM_BAS = item.DONEM_BAS;
                    model.DONEM_SON = item.DONEM_SON;
                    model.FILE_ID = item.FILE_ID;
                    model.GONDERIM_TARIHI = item.GONDERIM_TARIHI;
                    model.KODU = item.KODU;
                    model.TAH_FILE_ID = item.TAH_FILE_ID;
                    model.TAH_FIS_NO = item.TAH_FIS_NO;
                    model.TAH_TUTAR = item.TAH_TUTAR;
                    model.TAH_VADE = item.TAH_VADE;
                    model.TCKN = item.TCKN;
                    model.TUR = item.TUR;
                    model.UNVAN = item.UNVAN;
                    model.VERGI_DAIRESI = item.VERGI_DAIRESI;
                    model.OID = item.OID;
                    model.FIRMA_ID = item.FIRMA_ID;
                    model.TAH_OID = item.TAH_OID;
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

            if (client_data.IlkTarih != null && client_data.SonTarih != null)
            {
                beyanname_liste_tarih = data.GetBeyannameListesi_Tarih(client_data.IlkTarih,client_data.SonTarih).ToList();

                if (client_data.Cariler.Count <= 0 && client_data.BeyannameTurler.Count > 0)
                {
                    beyanname_liste_tarih = beyanname_liste_tarih.Where(x => client_data.BeyannameTurler.Select(b => b).Contains(x.KODU)).ToList();
                }
                if (client_data.Cariler.Count > 0 && client_data.BeyannameTurler.Count <= 0)
                {
                    beyanname_liste_tarih = beyanname_liste_tarih.Where(x => client_data.Cariler.Select(c => c).Contains(x.FIRMA_ID.Value)).ToList();
                }
                if (client_data.Cariler.Count > 0 && client_data.BeyannameTurler.Count > 0)
                {
                    beyanname_liste_tarih = beyanname_liste_tarih.Where(x => client_data.Cariler.Select(c => c).Contains(x.FIRMA_ID.Value)
                    && client_data.BeyannameTurler.Select(b => b).Contains(x.KODU)
                    ).ToList();
                }

                foreach (var item in beyanname_liste_tarih)
                {
                    var model = new BeyannameAraModel();

                    model.ID = item.ID;
                    model.DONEM_BAS = item.DONEM_BAS;
                    model.DONEM_SON = item.DONEM_SON;
                    model.FILE_ID = item.FILE_ID;
                    model.GONDERIM_TARIHI = item.GONDERIM_TARIHI;
                    model.KODU = item.KODU;
                    model.TAH_FILE_ID = item.TAH_FILE_ID;
                    model.TAH_FIS_NO = item.TAH_FIS_NO;
                    model.TAH_TUTAR = item.TAH_TUTAR;
                    model.TAH_VADE = item.TAH_VADE;
                    model.TCKN = item.TCKN;
                    model.TUR = item.TUR;
                    model.UNVAN = item.UNVAN;
                    model.VERGI_DAIRESI = item.VERGI_DAIRESI;
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

            if (kontrol.Count >0)
            {
                return true;
            }

            return false;
        }

        private string WhatsAppGonderimZamani(int? id)
        {
            var liste = data_gonderim_listesi.Where(x => x.EvrakId == id).ToList();

            if (liste.Count>0)
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

            if (kontrol.Count>0)
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
