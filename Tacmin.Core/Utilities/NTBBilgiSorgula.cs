using System;
using System.Collections.Generic;
using Tacmin.Core.GIBBilgiServisi;
using Tacmin.Core.Model;
using Tacmin.Core.NPSKimlikDogrulamaServisi;

namespace Tacmin.Core.Utilities
{
    public class NTBBilgiSorgula
    {
        //private readonly ServiceSoapClient ClNPSOlustur = new ServiceSoapClient();
        //private readonly GIBBilgiServisiSoapClient ClGibBilgiGetir = new GIBBilgiServisiSoapClient();
        //private readonly IVirtualContext _vctx;

        public NTBBilgiSorgula(
            )
        {
        }



        public bool BaglantiTesti()
        {
            try
            {
                var ClNPSOlustur = new ServiceSoapClient();
                NPSKimlikDogrulamaServisi.ServisHataHeader NPSHata = null;
                NPSHata = ClNPSOlustur.BaglantiTesti(out var dt);
                if (NPSHata == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public long NPSBelgeNoOlustur(string SMMMVergiNo, string SMMMSifre)
        {
            try
            {
                var disKullanici = new DisKullaniciKimlik
                {
                    ProgramAdi = NPSKimlikDogrulamaServisi.ProgramAdi.Belirtilmemis,
                    DisKullaniciTipi = DisKullaniciTipi.Belirtilmemis,
                    KimlikNO = SMMMVergiNo,
                    KimlikNOTipi = NPSKimlikDogrulamaServisi.KimlikNOTipi.TCKN
                };
                disKullanici.DisKullaniciTipi = DisKullaniciTipi.Belirtilmemis;
                disKullanici.Sifre = SMMMSifre;
                var ClNPSOlustur = new ServiceSoapClient();
                ClNPSOlustur.DisKullaniciKimlikDogrula(disKullanici, NPSKimlikDogrulamaServisi.NPSIslemTipi.BelgeOnayi, DateTime.Now, out var npsBelgeNO);
                return npsBelgeNO;
            }
            catch
            {
                throw;
            }
        }

        public NTBBilgiModel GercekKisiBilgiGetir(long TCKNo, string SMMMSifre, long npsBelgeNO)
        {
            var cariBilgileri = new NTBBilgiModel();
            var bilgiServisHeader = new BilgiServisHeader
            {
                IslemTipi = GIBBilgiServisi.NPSIslemTipi.BelgeOnayi,
                NPSBelgeNO = npsBelgeNO,
                Program = GIBBilgiServisi.ProgramAdi.Belirtilmemis,
                Sifre = SMMMSifre
            };

            var noterlikKimlikHeader = new NoterlikKimlikHeader();
            var ClGibBilgiGetir = new GIBBilgiServisiSoapClient();
            ClGibBilgiGetir.GercekSahisMukellefMerkezBilgiSorgu(bilgiServisHeader, noterlikKimlikHeader, TCKNo, out var merkezBilgiSorguSonuc);
            cariBilgileri.Adi = merkezBilgiSorguSonuc.Adi;
            cariBilgileri.Soyadi = merkezBilgiSorguSonuc.Soyadi;
            cariBilgileri.VergiDairesiKodu = merkezBilgiSorguSonuc.VergiDairesiKodu;
            cariBilgileri.VergiDairesiAdi = merkezBilgiSorguSonuc.VergiDairesiAdi;
            cariBilgileri.IsMahalleSemt = merkezBilgiSorguSonuc.IsAdresi.MahalleSemt;
            cariBilgileri.IsCaddeSokak = merkezBilgiSorguSonuc.IsAdresi.CaddeSokak;
            cariBilgileri.IsKapiNo = merkezBilgiSorguSonuc.IsAdresi.KapiNO;
            cariBilgileri.IsDaireNo = merkezBilgiSorguSonuc.IsAdresi.DaireNO;
            cariBilgileri.IsIlKodu = merkezBilgiSorguSonuc.IsAdresi.IlKodu;
            cariBilgileri.IsIlAdi = merkezBilgiSorguSonuc.IsAdresi.IlAdi;
            cariBilgileri.IsIlceAdi = merkezBilgiSorguSonuc.IsAdresi.IlceAdi;
            cariBilgileri.IkametMahalleSemt = merkezBilgiSorguSonuc.IkametgahAdresi.MahalleSemt;
            cariBilgileri.IkametCaddeSokak = merkezBilgiSorguSonuc.IkametgahAdresi.CaddeSokak;
            cariBilgileri.IkametKapiNo = merkezBilgiSorguSonuc.IkametgahAdresi.KapiNO;
            cariBilgileri.IkametDaireNo = merkezBilgiSorguSonuc.IkametgahAdresi.DaireNO;
            cariBilgileri.IkametIlKodu = merkezBilgiSorguSonuc.IkametgahAdresi.IlKodu;
            cariBilgileri.IkametIlAdi = merkezBilgiSorguSonuc.IkametgahAdresi.IlAdi;
            cariBilgileri.IkametIlceAdi = merkezBilgiSorguSonuc.IkametgahAdresi.IlceAdi;
            cariBilgileri.Faal = merkezBilgiSorguSonuc.FaalTerkBilgisi.ToString();
            cariBilgileri.VergiKimlikNo = merkezBilgiSorguSonuc.VKN;
            cariBilgileri.MeslekBilgileri = new List<NtbMeslekBilgisi>();
            foreach (var item in merkezBilgiSorguSonuc.MeslekListesi)
            {
                cariBilgileri.MeslekBilgileri.Add(new NtbMeslekBilgisi { MeslekKodu = item.MeslekKodu, MeslekAdi = item.MeslekAdi });
            }

            return cariBilgileri;
        }

        public NTBBilgiModel TuzelKisiBilgiGetir(string VergiNo, string SMMMSifre, long npsBelgeNO)
        {
            var cariBilgileri = new NTBBilgiModel();
            var bilgiServisHeader = new BilgiServisHeader
            {
                IslemTipi = GIBBilgiServisi.NPSIslemTipi.BelgeOnayi,
                NPSBelgeNO = npsBelgeNO,
                Program = GIBBilgiServisi.ProgramAdi.Belirtilmemis,
                Sifre = SMMMSifre
            };

            var noterlikKimlikHeader = new NoterlikKimlikHeader();
            var ClGibBilgiGetir = new GIBBilgiServisiSoapClient();
            ClGibBilgiGetir.TuzelSahisMukellefMerkezBilgiSorgu(bilgiServisHeader, noterlikKimlikHeader, VergiNo, out var merkezBilgiSorguSonuc);
            cariBilgileri.Unvan = merkezBilgiSorguSonuc.Unvan;
            cariBilgileri.VergiDairesiKodu = merkezBilgiSorguSonuc.VergiDairesiKodu;
            cariBilgileri.VergiDairesiAdi = merkezBilgiSorguSonuc.VergiDairesiAdi;

            cariBilgileri.IsMahalleSemt = merkezBilgiSorguSonuc.IsAdresi.MahalleSemt;
            cariBilgileri.IsCaddeSokak = merkezBilgiSorguSonuc.IsAdresi.CaddeSokak;
            cariBilgileri.IsKapiNo = merkezBilgiSorguSonuc.IsAdresi.KapiNO;
            cariBilgileri.IsDaireNo = merkezBilgiSorguSonuc.IsAdresi.DaireNO;
            cariBilgileri.IsIlKodu = merkezBilgiSorguSonuc.IsAdresi.IlKodu;
            cariBilgileri.IsIlAdi = merkezBilgiSorguSonuc.IsAdresi.IlAdi;
            cariBilgileri.IsIlceAdi = merkezBilgiSorguSonuc.IsAdresi.IlceAdi;

            cariBilgileri.IkametMahalleSemt = merkezBilgiSorguSonuc.IkametgahAdresi.MahalleSemt;
            cariBilgileri.IkametCaddeSokak = merkezBilgiSorguSonuc.IkametgahAdresi.CaddeSokak;
            cariBilgileri.IkametKapiNo = merkezBilgiSorguSonuc.IkametgahAdresi.KapiNO;
            cariBilgileri.IkametDaireNo = merkezBilgiSorguSonuc.IkametgahAdresi.DaireNO;
            cariBilgileri.IkametIlKodu = merkezBilgiSorguSonuc.IkametgahAdresi.IlKodu;
            cariBilgileri.IkametIlAdi = merkezBilgiSorguSonuc.IkametgahAdresi.IlAdi;
            cariBilgileri.IkametIlceAdi = merkezBilgiSorguSonuc.IkametgahAdresi.IlceAdi;

            cariBilgileri.Faal = merkezBilgiSorguSonuc.FaalTerkBilgisi.ToString();

            cariBilgileri.MeslekBilgileri = new List<NtbMeslekBilgisi>();
            foreach (var item in merkezBilgiSorguSonuc.MeslekListesi)
            {
                cariBilgileri.MeslekBilgileri.Add(new NtbMeslekBilgisi { MeslekKodu = item.MeslekKodu, MeslekAdi = item.MeslekAdi });
            }
            return cariBilgileri;
        }
    }
}
