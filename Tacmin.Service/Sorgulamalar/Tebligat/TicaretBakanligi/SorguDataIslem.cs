using DataIslemler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Model.Gib;
using Tacmin.Model.Tebligat;

namespace Tacmin.Service.Sorgulamalar.Tebligat.TicaretBakanligi
{
    public class SorguDataIslem
    {
        private string dataAdi;
        private List<Firma> firmaListesi;
        private DataIslemler.Tebligat.TicaretBakanligi.DataIslem dataIslem;

        public SorguDataIslem(string _dataAdi,List<Firma> _firmaListesi)
        {
            dataAdi = _dataAdi;
            firmaListesi = _firmaListesi;

            dataIslem = new DataIslemler.Tebligat.TicaretBakanligi.DataIslem(dataAdi);
        }

        public TicaretResModel TebligatSorguIslem()
        {
            var tebligat_listesi = new List<TebligatDataListModel>();
            var hata_listesi = new List<GibTabloSonuc>();

            foreach (var item in firmaListesi)
            {
                var islem = new TebligatIslem(item, dataAdi);

                var result = islem.TebligatSorgula();

                foreach (var hata_item in result.Item2)
                {
                    hata_listesi.Add(hata_item);
                }

                if (result.Item1.Count >0)
                {
                    var liste = dataIslem.TebligatKayitIslem(result.Item1,item.Id);

                    foreach (var teb_item in liste)
                    {
                        tebligat_listesi.Add(teb_item);
                    }
                }

            }

            var model = new TicaretResModel();

            model.HataListesi = hata_listesi;
            model.TebligatListesi = tebligat_listesi;

            return model;
        }
    }
}
