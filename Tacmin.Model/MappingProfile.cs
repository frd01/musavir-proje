using AutoMapper;
using Tacmin.Core.Extensions;
using Tacmin.Data;
using Tacmin.Data.DbModel;
using Tacmin.Model.MaliyeIslemleri.Faturalar;

namespace Tacmin.Model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BEYANNAME_ICERIKLERI, BeyannameAraModel>();

            CreateMap<FIRMA_TANIMLARI, MukellefAraModel>();

            CreateMap<FIRMA_TANIMLARI, MukellefKayitModel>();

            CreateMap<MukellefKayitModel, FIRMA_TANIMLARI>()
                .Ignore(x => x.SUBE_TANIMLARI)
                .Ignore(x => x.BEYANNAMELER)
                .Ignore(x => x.BILDIRGELER)
                ;

            CreateMap<FIRMA_FAALIYET_KODLARI, FaaliyetKoduAraModel>();

            CreateMap<FIRMA_SUBE_TANIMLARI, FirmaSubeKayitModel>();

            CreateMap<FirmaSubeKayitModel, FIRMA_SUBE_TANIMLARI>()
                .Ignore(x => x.FIRMA)
                ;

            CreateMap<KULLANICI_TANIMLARI, KullaniciKayitModel>();

            CreateMap<KullaniciKayitModel, KULLANICI_TANIMLARI>()
                .Ignore(x => x.FIRMA_BAGLANTILARI)
                .Ignore(x => x.KULLANICI_KODU)
                .Ignore(x => x.SIFRE)
                .Ignore(x => x.YETKILER)
                .Ignore(x => x.YETKILI)
                .Ignore(x => x.AKTIF)
                ;

            CreateMap<BILDIRGE_ICERIKLERI, BildirgeAraModel>();

            CreateMap<GELEN_FATURALAR, GelenFaturaModel>();
        }
    }
}