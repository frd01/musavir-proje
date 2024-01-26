using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Rapor;

namespace DataIslemler.Rapor
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;
        private List<BEYANNAME_ICERIKLERI> beyanname_listesi;
        private List<BILDIRGE_ICERIKLERI> bildirge_listesi;
        private List<Giden_Faturalar> giden_fatura_listesi;
        private List<Maliye_Gelen_Faturalar> gelen_fatura_listesi;
        private List<Firma_IseGirisCikislar> ise_giris_listesi;
        private List<Firma_Viziteler> vizite_listesi;
        private List<Tebligatlar_Gelir_Idaresi> gelir_idaresi_listesi;
        private List<Tebligatlar_Ticaret_Bakanligi> ticaret_bakanligi_listesi;
        private List<Tebligat_Icisleri> icisleri_listesi;
        private List<Tebligat_Vergi_Denetim> vergi_denetim_listesi;
        private List<FIRMA_TANIMLARI> firma_listesi;

        private string baseUrl = "";

        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            beyanname_listesi = data.BEYANNAME_ICERIKLERIs.Where(x => x.SorguKayitTarihi != null).ToList();
            bildirge_listesi = data.BILDIRGE_ICERIKLERIs.Where(x => x.SorguKayitTarihi != null).ToList();
            giden_fatura_listesi = data.Giden_Faturalars.Where(x => x.SorguKayitTarihi != null).ToList();
            gelen_fatura_listesi = data.Maliye_Gelen_Faturalars.Where(x => x.SorguKayitTarihi != null).ToList();
            ise_giris_listesi = data.Firma_IseGirisCikislars.Where(x => x.SorguKayitTarihi != null).ToList();
            vizite_listesi = data.Firma_Vizitelers.Where(x => x.SorguKayitTarihi != null).ToList();
            gelir_idaresi_listesi = data.Tebligatlar_Gelir_Idaresis.Where(x => x.SorguKayitTarihi != null).ToList();
            ticaret_bakanligi_listesi = data.Tebligatlar_Ticaret_Bakanligis.Where(x => x.SorguKayitTarihi != null).ToList();
            icisleri_listesi = data.Tebligat_Icisleris.Where(x => x.SorguKayitTarihi != null).ToList();
            vergi_denetim_listesi = data.Tebligat_Vergi_Denetims.Where(x => x.SorguKayitTarihi != null).ToList();

            firma_listesi = data.FIRMA_TANIMLARIs.ToList();

            baseUrl = new UrlIslem().BaseUrl;

        }

        public List<GunlukIslemModel> GunlukIslemListesi()
        {

            var liste = new List<GunlukIslemModel>();

            var kontrol = data.SorguOzets.Count();

            if (kontrol > 0)
            {
                var id = 1;
                var tarih_list = (from m in data.SorguOzets group m by m.Tarih into gr orderby gr.Key descending select gr).ToList();

                foreach (var item in tarih_list)
                {
                    var model = new GunlukIslemModel();
                    model.Id = id;
                    model.Tarih = item.Key;
                    model.Beyaname = beyanname_listesi.Where(x => x.SorguKayitTarihi == item.Key).Count();
                    model.Bildirge = bildirge_listesi.Where(x => x.SorguKayitTarihi == item.Key).Count();
                    model.GidenFatura = giden_fatura_listesi.Where(x => x.SorguKayitTarihi == item.Key).Count();
                    model.GelenFatura = gelen_fatura_listesi.Where(x => x.SorguKayitTarihi == item.Key).Count();
                    model.GelirIdaresiTebligat= gelir_idaresi_listesi.Where(x => x.SorguKayitTarihi == item.Key).Count();
                    model.TicaretBakanligiTebligat = ticaret_bakanligi_listesi.Where(x => x.SorguKayitTarihi == item.Key).Count();
                    model.IcisleriTebligat = icisleri_listesi.Where(x => x.SorguKayitTarihi == item.Key).Count();
                    model.VergiDenetimTebligat = vergi_denetim_listesi.Where(x => x.SorguKayitTarihi == item.Key).Count();
                    model.IseCikis = Get_IsCikis(item.Key);
                    model.IseGiris = Get_IsGiris(item.Key);
                    model.IsKazasi = Get_IsKazasi(item.Key);
                    model.Analik = Get_Analik(item.Key);
                    model.Hastalik = Get_Hastalik(item.Key);

                    model.GelirIdaresiTebligatUrl = getLink("gelirtebligat/SorguTebligatListesi", item.Key);
                    model.BeyanameUrl = getLink("beyanname/SorguBeyannameListesi", item.Key);
                    model.BildirgeUrl = getLink("bildirge/SorguBildirgeListesi", item.Key);
                    model.VergiDenetimTebligatUrl = getLink("vergidenetim/SorguTebligatListesi",item.Key);
                    model.TicaretBakanligiTebligatUrl = getLink("ticarettebligat/SorguTebligatListesi", item.Key);
                    model.IcisleriTebligatUrl = getLink("icisleritebligat/SorguTebligatListesi", item.Key);
                    model.GelenFaturaUrl = getLink("gelenfatura/SorguGelenFaturaListesi", item.Key);
                    model.GidenFaturaUrl = getLink("gidenfatura/SorguFaturaListesi", item.Key);
                    model.IseGirisUrl = getLink("bildirge/SorguGirisler", item.Key);
                    model.IseCikisUrl = getLink("bildirge/SorguCikislar", item.Key);
                    model.IsKazasiUrl = getLink("bildirge/ViziteIsKazasi", item.Key);
                    model.AnalikUrl = getLink("bildirge/ViziteAnalik", item.Key);
                    model.HastalikUrl = getLink("bildirge/ViziteHastalik", item.Key);
                    model.MeslekHastalik = Get_MeslekHastalik(item.Key);
                    model.MeslekHastalikUrl = getLink("bildirge/ViziteMeslekHast", item.Key);


                    liste.Add(model);

                    id++;
                    
                }
            }

            return liste;

        }

        private string getLink(string servisAdres,DateTime? tarih)
        {

            return baseUrl + servisAdres + "?tarih=" + tarih.Value.ToString("dd.MM.yyyy");
        }

        public List<RaporFirmaModel> GibBilgisiEksikFirmalar()
        {
            var liste = new List<RaporFirmaModel>();

            foreach (var item in data.Rapor_GibBilgisi_Eksik_Firmalar())
            {
                var model = new RaporFirmaModel();
                model.Id = item.Id;
                model.Unvan = item.UNVAN;

                liste.Add(model);
            }

            return liste;
        }

        public List<RaporFirmaModel> SgkBilgisiEksikFirmalar()
        {
            var liste = new List<RaporFirmaModel>();

            foreach (var item in data.Rapor_SgkBilgisi_Eksik_Firmalar())
            {
                var model = new RaporFirmaModel();
                model.Id = item.Id;
                model.Unvan = item.UNVAN;

                liste.Add(model);
            }

            return liste;
        }

        public List<RaporFirmaModel> IslemHataListesi()
        {
            var liste = new List<RaporFirmaModel>();

            foreach (var item in data.SorguOzets)
            {
                var kontrol = liste.Where(x => x.FirmaId == item.FirmaId).Count();

                if (kontrol<=0)
                {
                    var model = new RaporFirmaModel();
                    model.FirmaId = item.FirmaId;
                    model.Id = item.Id;
                    model.Unvan = GetUnvan(item.FirmaId);
                    model.Mesaj = item.Aciklama;

                    liste.Add(model);
                }
            }

            return liste;
        }

        private string GetUnvan(int? firmaId)
        {
            var unvan = "";

            var model = firma_listesi.Where(x => x.ID == firmaId).SingleOrDefault();

            if (model!=null)
            {
                unvan = model.UNVAN;
            }

            return unvan;
        }

        private int Get_IsCikis(DateTime? sorguTarihi)
        {

            return ise_giris_listesi.Where(x => x.SorguKayitTarihi == sorguTarihi
            && x.Durum == "çıkış").Count();
        }

        private int Get_IsGiris(DateTime? sorguTarihi)
        {

            return ise_giris_listesi.Where(x => x.SorguKayitTarihi == sorguTarihi
            && x.Durum == "giriş").Count();
        }

        private int Get_Hastalik(DateTime? sorguTarihi)
        {
            return vizite_listesi.Where(x => x.SorguKayitTarihi == sorguTarihi
            && x.Vaka == "3").Count();

        }

        private int Get_IsKazasi(DateTime? sorguTarihi)
        {
            return vizite_listesi.Where(x => x.SorguKayitTarihi == sorguTarihi
            && x.Vaka == "1").Count();

        }

        private int Get_Analik(DateTime? sorguTarihi)
        {
            return vizite_listesi.Where(x => x.SorguKayitTarihi == sorguTarihi
            && x.Vaka == "4").Count();

        }

        private int Get_MeslekHastalik(DateTime? sorguTarihi)
        {
            return vizite_listesi.Where(x => x.SorguKayitTarihi == sorguTarihi
            && x.Vaka == "2").Count();

        }


    }
}
