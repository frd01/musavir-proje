using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Banka;

namespace DataIslemler.Banka
{
    public class BankaEsktraRapor
    {
        private Musavir_DbDataContext data;
        private BuyukHarfIslem harfIslem;
        private DataIslemler.Banka.RaporIslem raporIslem;

        public BankaEsktraRapor(string dataAdi, bool isBuyukHarf)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            harfIslem = new BuyukHarfIslem(isBuyukHarf);

            raporIslem = new RaporIslem(dataAdi, isBuyukHarf);
        }

        public BankaResponseModel<BankaEsktraModel, BankaEkstraParamsModel> GetBankaEkstraRapor(RaporRequestModel args)
        {

            var rapor_liste = new List<BankaEsktraModel>();

            var params_liste = new List<BankaEkstraParamsModel>();

            var banka_listesi = GetBankaListesi();

            foreach (var bankaId in banka_listesi)
            {
                foreach (var item in raporIslem.BankaIslemListesi_Tarih(bankaId, args.IlkTarih, args.SonTarih))
                {
                    var model = new BankaEsktraModel();

                    model.Aciklama = item.Aciklama;
                    model.Borc = item.Yatan;
                    model.Bakiye = item.Bakiye;
                    model.Alacak = item.Cekilen;
                    model.CariId = item.CariId;
                    model.Id = item.Id;
                    model.IslemTur = item.IslemTuru;
                    model.KasaId = item.KasaId;
                    if (item.CariId == null)
                        model.Kodu = item.KasaKodu;
                    if (item.CariId != null)
                        model.Kodu = item.CariKodu;
                    model.Tarih = item.Tarih.Value.ToString("dd/MM/yyyy");

                    rapor_liste.Add(model);

                    params_liste.Add(new BankaEkstraParamsModel { BankaAdi = item.BankaAdi, BankaKodu = item.BankaKodu });

                }
            }

            var result = new BankaResponseModel<BankaEsktraModel, BankaEkstraParamsModel>();

            result.RaporListe = rapor_liste;
            result.ParametreListesi = params_liste;

            return result;

        }

        private List<int> GetBankaListesi()
        {
            var liste = new List<int>();

            foreach (var item in data.Banka_Kartlaris)
            {
                liste.Add(item.Id);
            }

            return liste;
        }

        private List<int> GetKasaListesi()
        {
            var liste = new List<int>();

            foreach (var item in data.Kasa_Kartlaris)
            {
                liste.Add(item.Id);
            }

            return liste;
        }

        private List<int> GetCariListesi()
        {

            var liste = new List<int>();

            foreach (var item in data.FIRMA_TANIMLARIs)
            {
                liste.Add(item.ID);
            }

            return liste;
        }
    }
}
