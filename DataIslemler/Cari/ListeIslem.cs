using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Cari;

namespace DataIslemler.Cari
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;

        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<CariListeModel> CariListesi()
        {
            var liste = new List<CariListeModel>();

            var data_list = data.FIRMA_TANIMLARIs.Where(x => x.AKTIF == true).ToList();

            foreach (var item in data_list)
            {
                var model = new CariListeModel();

                model.Id = item.ID;
                model.Unvan = item.UNVAN;
                model.Telefon = "";
                model.Borc = BorcToplam(item.ID);
                model.Alacak = AlacakToplam(item.ID);
                model.AlacakBakiye = 0;
                model.BorcBakiye = 0;

                var bakiye = model.Borc - model.Alacak;

                if (bakiye<0)
                {
                    model.AlacakBakiye = bakiye;
                }
                if (bakiye>0)
                {
                    model.BorcBakiye = bakiye;
                }

                liste.Add(model);

            }

            return liste;
        }

        private decimal? BorcToplam(int cariId)
        {
            decimal? toplam = 0;

            foreach (var item in data.BankaCariOdemeler_CariId(cariId))
            {
                if (item.Tutar != null)
                {
                    toplam += item.Tutar.Value;
                }
            }

            foreach (var item in data.KasaOdemeListesi_CariId(cariId))
            {
                if (item.Tutar != null)
                {
                    toplam += item.Tutar.Value;
                }
            }

            return toplam;
        }

        private decimal? AlacakToplam(int cariId)
        {
            decimal? toplam = 0;

            foreach (var item in data.BankaCariTahsilatlar_CariId(cariId))
            {
                if (item.Tutar != null)
                {
                    toplam += item.Tutar.Value;
                }
            }

            foreach (var item in data.KasaTahsilatListesi_CariId(cariId))
            {
                if (item.Tutar != null)
                {
                    toplam += item.Tutar.Value;
                }
            }

            return toplam;
        }

        public List<CariHareketDetay> CariHareketListesi(int cariId,DateTime? ilkTarih,DateTime? sonTarih)
        {
            if (ilkTarih == null)
            {
                sonTarih = DateTime.Now;
                ilkTarih = new DateTime(sonTarih.Value.Year, sonTarih.Value.Month, 1);
            }
            var liste = new List<CariHareketDetay>();

            var id = 1;

            #region Alacak Listesi

            foreach (var item in data.BankaCariTahsilatlar_CariId_Tarih(cariId,ilkTarih,sonTarih))
            {
                var model = new CariHareketDetay();
                model.Id = id;
                model.ModulAdi = "Banka";
                model.ModulId = (int)CariModulEnums.BANKA;
                model.Tarih = item.Tarih;
                model.Alacak = item.Tutar ?? 0;
                model.Borc = 0;
                model.Bakiye = 0;
                model.Aciklama = item.IslemAciklama;

                liste.Add(model);
                id++;
            }

            foreach (var item in data.KasaTahsilatListesi_CariId_Tarih(cariId,ilkTarih,sonTarih))
            {
                var model = new CariHareketDetay();
                model.Id = id;
                model.ModulAdi = "Kasa";
                model.ModulId = (int)CariModulEnums.KASA;
                model.Tarih = item.Tarih;
                model.Alacak = item.Tutar ?? 0;
                model.Borc = 0;
                model.Bakiye = 0;
                model.Aciklama = item.Aciklama;

                liste.Add(model);
                id++;
            }

            #endregion

            #region Borç Listesi

            foreach (var item in data.BankaCariOdemeler_CariId_Tarih(cariId, ilkTarih, sonTarih))
            {
                var model = new CariHareketDetay();
                model.Id = id;
                model.ModulAdi = "Banka";
                model.ModulId = (int)CariModulEnums.BANKA;
                model.Tarih = item.Tarih;
                model.Borc = item.Tutar ?? 0;
                model.Alacak = 0;
                model.Bakiye = 0;
                model.Aciklama = item.IslemAciklama;

                liste.Add(model);
                id++;
            }

            foreach (var item in data.KasaOdemeListesi_CariId_Tarih(cariId, ilkTarih, sonTarih))
            {
                var model = new CariHareketDetay();
                model.Id = id;
                model.ModulAdi = "Kasa";
                model.ModulId = (int)CariModulEnums.KASA;
                model.Tarih = item.Tarih;
                model.Borc = item.Tutar ?? 0;
                model.Alacak = 0;
                model.Bakiye = 0;
                model.Aciklama = item.Aciklama;

                liste.Add(model);
                id++;
            }

            #endregion


            liste = (from m in liste orderby m.Tarih ascending select m).ToList();

            decimal? bakiye = 0;

            foreach (var item in liste)
            {
                bakiye += (item.Borc - item.Alacak);
                item.Bakiye = bakiye;
            }


            return liste;
        }
    }
}
