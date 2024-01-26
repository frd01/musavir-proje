using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Maliye;

namespace DataIslemler.MaliyeGelenFatura
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;

        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<MaliyeGelenFaturaListeModel> GelenFaturaListesi(DateTime? ilkTarih, DateTime? sonTarih, Tacmin.Model.LocalData.FirmaModel firma)
        {

            if (ilkTarih == null)
            {
                sonTarih = DateTime.Now;
                ilkTarih = new DateTime(sonTarih.Value.Year, sonTarih.Value.Month, 1);
            }

            var liste = new List<MaliyeGelenFaturaListeModel>();

            foreach (var item in data.Maliye_Gelen_Fatura_Listesi_Tarih(ilkTarih, sonTarih))
            {
                var model = new MaliyeGelenFaturaListeModel();

                model.FaturaNo = item.FaturaNo;
                model.GonderimSekli = item.GonderimSekli;
                model.Id = item.Id;
                model.Odenecek = item.Odenecek;
                model.ParaBirimi = item.ParaBirimi;
                model.Tarih = item.Tarih;
                model.TesisatNo = item.TesisatNo;
                model.Toplam = item.Toplam;
                model.Unvan = item.Unvan;
                model.Vergi = item.Vergi;
                model.VergiNo = item.VergiNo;
                model.MukellefUnvan = item.MukellefUnvan;
                model.FirmaId = item.FirmaId;

                liste.Add(model);
            }


            if (firma != null)
            {
                if (firma.Id != null)
                {
                    liste = liste.Where(x => x.FirmaId == firma.Id).ToList();
                }
            }

            return liste;
        }

        public List<MaliyeGelenFaturaListeModel> SorguGelenFaturaListesi(DateTime? tarih)
        {

            var liste = new List<MaliyeGelenFaturaListeModel>();

            foreach (var item in data.Maliye_Gelen_Fatura_Listesi_Sorgu_Tarih(tarih))
            {
                var model = new MaliyeGelenFaturaListeModel();

                model.FaturaNo = item.FaturaNo;
                model.GonderimSekli = item.GonderimSekli;
                model.Id = item.Id;
                model.Odenecek = item.Odenecek;
                model.ParaBirimi = item.ParaBirimi;
                model.Tarih = item.Tarih;
                model.TesisatNo = item.TesisatNo;
                model.Toplam = item.Toplam;
                model.Unvan = item.Unvan;
                model.Vergi = item.Vergi;
                model.VergiNo = item.VergiNo;
                model.MukellefUnvan = item.MukellefUnvan;

                liste.Add(model);
            }

            return liste;
        }
    }
}
