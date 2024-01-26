using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Banka;

namespace DataIslemler.Banka
{
    public class RaporIslem
    {
        private Musavir_DbDataContext data;
        private BuyukHarfIslem harfIslem;

        public RaporIslem(string dataAdi, bool is_buyuk_harf)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            harfIslem = new BuyukHarfIslem(is_buyuk_harf);
        }

        public List<BankaIslemListeModel> BankaIslemListesi(int? bankaId)
        {
            var liste = new List<BankaIslemListeModel>();

            var rapor_id = 1;

            //Cari Ödemeler

            foreach (var item in data.BankaCariOdemeler_BankaId(bankaId))
            {
                var model = new BankaIslemListeModel();

                model.Aciklama = item.UNVAN + " - " + item.IslemAciklama;
                model.CariId = item.CariId;
                model.FisNo = item.FisNo;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);

                model.BankaAdi = harfIslem.GetValueStr(item.BankaAdi);
                model.BankaKodu = item.BankaKodu;
                model.CariUnvan = harfIslem.GetValueStr(item.UNVAN);
                model.BankaId = item.BankaId;
                model.CariKodu = item.CariKodu;
                model.TabloAciklama = harfIslem.GetValueStr(item.IslemAciklama);
                model.KasaKodu = "";
                model.KasaAdi = "";
                model.CariId = item.CariId;


                model.Id = rapor_id;
                model.Islem = harfIslem.GetValueStr(item.Islem);
                model.IslemTuru = harfIslem.GetValueStr("Giden Havale");
                model.TabloId = item.Id;
                model.Tarih = item.Tarih;
                model.Cekilen = item.Tutar;



                liste.Add(model);

                rapor_id++;
            }

            //Cari Tahsilatlar

            foreach (var item in data.BankaCariTahsilatlar_BankaId(bankaId))
            {
                var model = new BankaIslemListeModel();

                model.Aciklama = item.UNVAN + " - " + item.IslemAciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.CariId = item.CariId;
                model.FisNo = item.FisNo;

                model.BankaAdi = harfIslem.GetValueStr(item.BankaAdi);
                model.BankaKodu = item.BankaKodu;
                model.CariUnvan = harfIslem.GetValueStr(item.UNVAN);
                model.BankaId = item.BankaId;
                model.CariKodu = item.CariKodu;
                model.TabloAciklama = harfIslem.GetValueStr(item.IslemAciklama);
                model.KasaKodu = "";
                model.KasaAdi = "";
                model.CariId = item.CariId;


                model.Id = rapor_id;
                model.Islem = harfIslem.GetValueStr(item.Islem);
                model.IslemTuru = harfIslem.GetValueStr("Gelen Havale");
                model.TabloId = item.Id;
                model.Tarih = item.Tarih;
                model.Yatan = item.Tutar;



                liste.Add(model);

                rapor_id++;
            }

            //Kasa Çekilenler

            foreach (var item in data.BankaKasaCekilenler_BankaId(bankaId))
            {
                var model = new BankaIslemListeModel();

                model.Aciklama = item.KasaAdi + " - " + item.IslemAciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.KasaId = item.KasaId;
                model.FisNo = item.FisNo;

                model.BankaAdi = harfIslem.GetValueStr(item.BankaAdi);
                model.BankaKodu = item.BankaKodu;
                model.CariUnvan = "";
                model.BankaId = item.BankaId;
                model.CariKodu = "";
                model.TabloAciklama = harfIslem.GetValueStr(item.IslemAciklama);
                model.KasaKodu = item.KasaKodu;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;

                model.Id = rapor_id;
                model.Islem = harfIslem.GetValueStr(item.Islem);
                model.IslemTuru = harfIslem.GetValueStr("Nakit Çekilen");
                model.TabloId = item.Id;
                model.Tarih = item.Tarih;
                model.Cekilen = item.Tutar;



                liste.Add(model);

                rapor_id++;
            }

            //Kasa Yatirilanlar

            foreach (var item in data.BankaKasaYatirilanlar_BankaId(bankaId))
            {
                var model = new BankaIslemListeModel();

                model.Aciklama = item.KasaAdi + " - " + item.IslemAciklama;
                model.KasaId = item.KasaId;
                model.FisNo = item.FisNo;

                model.BankaAdi = harfIslem.GetValueStr(item.BankaAdi);
                model.BankaKodu = item.BankaKodu;
                model.CariUnvan = "";
                model.BankaId = item.BankaId;
                model.CariKodu = "";
                model.TabloAciklama = harfIslem.GetValueStr(item.IslemAciklama);
                model.KasaKodu = item.KasaKodu;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;

                model.Id = rapor_id;
                model.Islem = harfIslem.GetValueStr(item.Islem);
                model.IslemTuru = harfIslem.GetValueStr("Nakit Yatan");
                model.TabloId = item.Id;
                model.Tarih = item.Tarih;
                model.Yatan = item.Tutar;

                liste.Add(model);

                rapor_id++;
            }


            liste = (from m in liste orderby m.Tarih ascending select m).ToList();

            decimal? bakiye = null;

            // Bakiyenin Ayarlanması

            foreach (var item in liste)
            {
                if (item.Yatan != null)
                {
                    if (bakiye == null)
                        bakiye = 0;

                    bakiye += item.Yatan;
                }

                if (item.Cekilen != null)
                {
                    if (bakiye == null)
                        bakiye = 0;

                    bakiye -= item.Cekilen;
                }

                item.Bakiye = bakiye;
            }

            return liste;

        }

        public List<BankaIslemListeModel> BankaIslemListesi_Tarih(int? bankaId, DateTime? ilkTarih, DateTime? sonTarih)
        {
            var liste = new List<BankaIslemListeModel>();

            var rapor_id = 1;

            //Cari Ödemeler

            foreach (var item in data.BankaCariOdemeler_BankaId_Tarih(bankaId, ilkTarih, sonTarih))
            {
                var model = new BankaIslemListeModel();

                model.Aciklama = item.UNVAN + " - " + item.IslemAciklama;
                model.CariId = item.CariId;
                model.FisNo = item.FisNo;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);

                model.BankaAdi = harfIslem.GetValueStr(item.BankaAdi);
                model.BankaKodu = item.BankaKodu;
                model.CariUnvan = harfIslem.GetValueStr(item.UNVAN);
                model.BankaId = item.BankaId;
                model.CariKodu = item.CariKodu;
                model.TabloAciklama = harfIslem.GetValueStr(item.IslemAciklama);
                model.KasaKodu = "";
                model.KasaAdi = "";
                model.CariId = item.CariId;


                model.Id = rapor_id;
                model.Islem = harfIslem.GetValueStr(item.Islem);
                model.IslemTuru = harfIslem.GetValueStr("Giden Havale");
                model.TabloId = item.Id;
                model.Tarih = item.Tarih;
                model.Cekilen = item.Tutar;



                liste.Add(model);

                rapor_id++;
            }

            //Cari Tahsilatlar

            foreach (var item in data.BankaCariTahsilatlar_BankaId_Tarih(bankaId, ilkTarih, sonTarih))
            {
                var model = new BankaIslemListeModel();

                model.Aciklama = item.UNVAN + " - " + item.IslemAciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.CariId = item.CariId;
                model.FisNo = item.FisNo;

                model.BankaAdi = harfIslem.GetValueStr(item.BankaAdi);
                model.BankaKodu = item.BankaKodu;
                model.CariUnvan = harfIslem.GetValueStr(item.UNVAN);
                model.BankaId = item.BankaId;
                model.CariKodu = item.CariKodu;
                model.TabloAciklama = harfIslem.GetValueStr(item.IslemAciklama);
                model.KasaKodu = "";
                model.KasaAdi = "";
                model.CariId = item.CariId;


                model.Id = rapor_id;
                model.Islem = harfIslem.GetValueStr(item.Islem);
                model.IslemTuru = harfIslem.GetValueStr("Gelen Havale");
                model.TabloId = item.Id;
                model.Tarih = item.Tarih;
                model.Yatan = item.Tutar;



                liste.Add(model);

                rapor_id++;
            }

            //Kasa Çekilenler

            foreach (var item in data.BankaKasaCekilenler_BankaId_Tarih(bankaId, ilkTarih, sonTarih))
            {
                var model = new BankaIslemListeModel();

                model.Aciklama = item.KasaAdi + " - " + item.IslemAciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.KasaId = item.KasaId;
                model.FisNo = item.FisNo;

                model.BankaAdi = harfIslem.GetValueStr(item.BankaAdi);
                model.BankaKodu = item.BankaKodu;
                model.CariUnvan = "";
                model.BankaId = item.BankaId;
                model.CariKodu = "";
                model.TabloAciklama = harfIslem.GetValueStr(item.IslemAciklama);
                model.KasaKodu = item.KasaKodu;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;

                model.Id = rapor_id;
                model.Islem = harfIslem.GetValueStr(item.Islem);
                model.IslemTuru = harfIslem.GetValueStr("Nakit Çekilen");
                model.TabloId = item.Id;
                model.Tarih = item.Tarih;
                model.Cekilen = item.Tutar;



                liste.Add(model);

                rapor_id++;
            }

            //Kasa Yatirilanlar

            foreach (var item in data.BankaKasaYatirilanlar_BankaId_Tarih(bankaId, ilkTarih, sonTarih))
            {
                var model = new BankaIslemListeModel();

                model.Aciklama = item.KasaAdi + " - " + item.IslemAciklama;
                model.KasaId = item.KasaId;
                model.FisNo = item.FisNo;

                model.BankaAdi = harfIslem.GetValueStr(item.BankaAdi);
                model.BankaKodu = item.BankaKodu;
                model.CariUnvan = "";
                model.BankaId = item.BankaId;
                model.CariKodu = "";
                model.TabloAciklama = harfIslem.GetValueStr(item.IslemAciklama);
                model.KasaKodu = item.KasaKodu;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;

                model.Id = rapor_id;
                model.Islem = harfIslem.GetValueStr(item.Islem);
                model.IslemTuru = harfIslem.GetValueStr("Nakit Yatan");
                model.TabloId = item.Id;
                model.Tarih = item.Tarih;
                model.Yatan = item.Tutar;

                liste.Add(model);

                rapor_id++;
            }



            liste = (from m in liste orderby m.Tarih ascending select m).ToList();

            decimal? bakiye = null;

            // Bakiyenin Ayarlanması

            foreach (var item in liste)
            {
                if (item.Yatan != null)
                {
                    if (bakiye == null)
                        bakiye = 0;

                    bakiye += item.Yatan;
                }

                if (item.Cekilen != null)
                {
                    if (bakiye == null)
                        bakiye = 0;

                    bakiye -= item.Cekilen;
                }

                item.Bakiye = bakiye;
            }

            return liste;

        }


        public List<BankaBakiyeRaporModel> BankaBakiyeRaporu(Tacmin.Model.RaporRequestModel args)
        {
            var liste = new List<BankaBakiyeRaporModel>();

            var data_list = data.Banka_Kartlaris.ToList();

            foreach (var item in data_list)
            {
                var model = new BankaBakiyeRaporModel();

                model.Alacak = GetAlacakToplam(item.Id, args);
                model.BankaAdi = item.BankaAdi;
                model.BankaKodu = item.BankaKodu;
                model.Borc = GetBorcToplam(item.Id, args);
                model.IbanNo = item.IbanNo;
                model.Id = item.Id;
                model.SubeAdi = item.SubeAdi;

                if (model.Alacak == null)
                    model.Alacak = 0;

                if (model.Borc == null)
                    model.Borc = 0;

                model.Bakiye = model.Borc - model.Alacak;

                liste.Add(model);
            }

            return liste;
        }

        private decimal? GetBorcToplam(int bankaId, Tacmin.Model.RaporRequestModel args)
        {
            decimal? toplam = 0;

            if (args.IlkTarih != null)
            {
                var sonTarih = args.SonTarih;
                if (sonTarih == null)
                    sonTarih = DateTime.Now;

                foreach (var item in data.BankaCariTahsilatlar_BankaId_Tarih(bankaId, args.IlkTarih, sonTarih))
                {
                    if (item.Tutar != null)
                    {
                        toplam += item.Tutar;
                    }
                }
                foreach (var item in data.BankaKasaYatirilanlar_BankaId_Tarih(bankaId, args.IlkTarih, sonTarih))
                {
                    if (item.Tutar != null)
                    {
                        toplam += item.Tutar;
                    }
                }
            }
            if (args.IlkTarih == null)
            {
                foreach (var item in data.BankaCariTahsilatlar_BankaId(bankaId))
                {
                    if (item.Tutar != null)
                    {
                        toplam += item.Tutar;
                    }
                }
                foreach (var item in data.BankaKasaYatirilanlar_BankaId(bankaId))
                {
                    if (item.Tutar != null)
                    {
                        toplam += item.Tutar;
                    }
                }
            }

            return toplam;
        }

        private decimal? GetAlacakToplam(int bankaId, Tacmin.Model.RaporRequestModel args)
        {
            decimal? toplam = 0;

            if (args.IlkTarih != null)
            {
                var sonTarih = args.SonTarih;
                if (sonTarih == null)
                    sonTarih = DateTime.Now;

                foreach (var item in data.BankaCariOdemeler_BankaId_Tarih(bankaId, args.IlkTarih, sonTarih))
                {
                    if (item.Tutar != null)
                    {
                        toplam += item.Tutar;
                    }
                }

                foreach (var item in data.BankaKasaCekilenler_BankaId_Tarih(bankaId, args.IlkTarih, sonTarih))
                {
                    if (item.Tutar != null)
                    {
                        toplam += item.Tutar;
                    }
                }
            }
            if (args.IlkTarih == null)
            {
                foreach (var item in data.BankaCariOdemeler_BankaId(bankaId))
                {
                    if (item.Tutar != null)
                    {
                        toplam += item.Tutar;
                    }
                }
                foreach (var item in data.BankaKasaCekilenler_BankaId(bankaId))
                {
                    if (item.Tutar != null)
                    {
                        toplam += item.Tutar;
                    }
                }
            }

            return toplam;
        }
    }
}
