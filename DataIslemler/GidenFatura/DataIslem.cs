using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.EArsiv;

namespace DataIslemler.GidenFatura
{
    public class DataIslem
    {
        Musavir_DbDataContext data;
        private DataIslemler.Islemler.DosyaIslem dosyaIslem;
        private List<FIRMA_TANIMLARI> data_firma_listesi;

        decimal kdvToplam = 0;

        public DataIslem(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
            dosyaIslem = new Islemler.DosyaIslem(); 
        }

        public List<ArsivFaturaModel> FaturaKayitIslem(List<ArsivFaturaModel> clientFaturaListesi)
        {

            foreach (var item in clientFaturaListesi)
            {
                kdvToplam = 0;
                var model = faturaKontrol(item.belgeNo,item.tarih,item.aliciVergiNo);

                if (model == null)
                    model = new Giden_Faturalar();

                model.Belge_Turu = item.belgeTuru;
                model.Ettn_No = item.ettn;
                model.Fatura_No = item.belgeNo;
                model.Fatura_Tipi = item.tip;
                model.Firma_Id = item.firmaId;
                model.Iskonta_Toplam = item.iskontoToplam ?? 0;
                model.KDV_0 = item.hesaplananKdv_0 ?? 0;
                model.Kdv_1 = item.hesaplananKdv_1 ?? 0;
                model.Kdv_10 = item.hesaplananKdv_10 ?? 0;
                model.Kdv_18 = item.hesaplananKdv_18 ?? 0;
                model.Kdv_20 = item.hesaplananKdv_20 ?? 0;
                model.Kdv_8 = item.hesaplananKdv_8 ?? 0;
                model.Mal_Hizmet_Toplami = item.malHizmetToplam ?? 0;
                model.Odenecek_Toplam = item.odenecek ?? 0;
                model.Onay_Durumu = item.onayDurumu;
                model.Senorya = item.senaryo;
                model.Tarih = Convert.ToDateTime(item.tarih);

                model.Vergi_No = item.aliciVergiNo;
                model.Unvan = item.aliciUnvan;
                model.Vergiler_Dahil_Toplam = item.vergidahilToplam;
                model.ParaBirimi = item.paraBirimi;
                model.Matrah_0 = item.kdvMatrah_0 ?? 0;
                model.Matrah_1 = item.kdvMatrah_1 ?? 0;
                model.Matrah_10 = item.kdvMatrah_10 ?? 0;
                model.Matrah_18 = item.kdvMatrah_18 ?? 0;
                model.Matrah_20 = item.kdvMatrah_20 ?? 0;
                model.Matrah_8 = item.kdvMatrah_8 ?? 0;

                if (item.hesaplananKdv_1 != null)
                    kdvToplam += item.hesaplananKdv_1.Value;

                if (item.hesaplananKdv_10 != null)
                    kdvToplam += item.hesaplananKdv_10.Value;

                if (item.hesaplananKdv_18 != null)
                    kdvToplam += item.hesaplananKdv_18.Value;

                if (item.hesaplananKdv_20 != null)
                    kdvToplam += item.hesaplananKdv_20.Value;

                if (item.hesaplananKdv_8 != null)
                    kdvToplam += item.hesaplananKdv_8.Value;

                model.KdvToplam = model.Kdv_1 + model.Kdv_10 + model.Kdv_18 + model.Kdv_20 + model.Kdv_8;

                item.KdvToplam = model.KdvToplam;

                model.TevfikatKdvToplam = 0;

                if (item.tip == "TEVKIFAT")
                {
                    model.TevfikatKdvToplam = model.Vergiler_Dahil_Toplam - model.Odenecek_Toplam;
                }

                if (model.Id > 0)
                {
                    data.SubmitChanges();
                }

                else
                {
                    data.Giden_Faturalars.InsertOnSubmit(model);
                    data.SubmitChanges();
                }

                //model.Vergiler_Dahil_Toplam = model.KdvToplam - model.TevfikatKdvToplam;

                item.Id = model.Id;
                item.Unvan = GetUnvan(model.Firma_Id.Value);

                var pdfModel = new FaturaDosyaModel();
                pdfModel.DosyaAdi = item.ettn + ".pdf";
                pdfModel.FaturaId = model.Id;
                pdfModel.MimeType = "application/pdf";
                pdfModel.Icerik = dosyaIslem.HtmlToPdfKayit(item.htmlText);
                pdfModel.Tur = "Pdf";
                GidenDosyaKaydet(pdfModel);

                var html_model = new FaturaDosyaModel();
                html_model.DosyaAdi = item.ettn + ".html";
                html_model.FaturaId = model.Id;
                html_model.MimeType = "text/html";
                html_model.Icerik = Encoding.UTF8.GetBytes(item.htmlText);
                html_model.Tur = "Html";
                GidenDosyaKaydet(html_model);

            }

            return clientFaturaListesi;
        }

        private Giden_Faturalar faturaKontrol(string faturaNo,DateTime? tarih,string vergiNo)
        {

            var dtFaturaList = data.Giden_Faturalars.Where(x => x.Fatura_No == faturaNo 
            && x.Tarih == tarih && x.Vergi_No == vergiNo).ToList();
            if (dtFaturaList.Count > 0)
            {
                return dtFaturaList[0];
            }

            return null;
        }

        private void GidenDosyaKaydet(FaturaDosyaModel fatura)
        {
            var kontrol = data.ArsivFaturalars.Where(x => x.FaturaId == fatura.FaturaId && x.Tur == fatura.Tur).ToList();

            var model = new ArsivFaturalar();

            if (kontrol.Count > 0)
            {
                model = kontrol[0];
            }

            model.FaturaId = fatura.FaturaId;
            model.Boyut = fatura.Boyut;
            model.DosyaAdi = fatura.DosyaAdi;
            model.Icerik = fatura.Icerik;
            model.MimeType = fatura.MimeType;
            model.Tur = fatura.Tur;

            if (kontrol.Count>0)
            {
                data.SubmitChanges();
            }

            if (kontrol.Count<=0)
            {
                data.ArsivFaturalars.InsertOnSubmit(model);
                data.SubmitChanges();
            }
        }

        private string GetUnvan(int id)
        {

            if(data_firma_listesi == null)
            {
                data_firma_listesi = data.FIRMA_TANIMLARIs.ToList();
            }

            return data_firma_listesi.Where(x => x.ID == id).SingleOrDefault().UNVAN;
        }
    }
}
