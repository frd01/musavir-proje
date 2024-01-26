using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Mukellef;

namespace DataIslemler.Mukellef
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;
      

        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            
        }

        public List<SgkListeModel> MusteriSgkSubeListesi(int? firmaId)
        {
            var liste = new List<SgkListeModel>();

            var data_list = data.Firma_Sgk_Bilgileris.Where(x => x.FirmaId == firmaId).ToList();

            foreach (var item in data_list)
            {
                var model = new SgkListeModel();

                model.Id = item.Id;
                model.AcilisTarihi = item.AcilisTarihi;
                model.Adres = item.Adres;
                model.Aktif = item.Aktif;
                model.IsyeriSifresi = item.IsyeriSifresi;
                model.KapanisTarihi = item.KapanisTarihi;
                model.KullaniciAdi_1 = item.KullanıciAdi_1;
                model.KullaniciAdi_2 = item.KullaniciAdi_2;
                model.Notlar = item.Notlar;
                model.SistemSifresi = item.SistemSifresi;
                model.SubeAdi = item.SubeAdi;
                model.TakipListesi = BilgiTakipList(item.Id);
                model.M = item.M;
                model.IsKoluKodu = item.IsKoluKodu;
                model.Yeni = item.Yeni;
                model.Eski = item.Eski;
                model.IsYeriSiraNo = item.IsYeriSıraNo;
                model.IlKodu = item.IlKodu;
                model.IlceKodu = item.IlceKodu;
                model.KontrolNo = item.KontrolNo;
                model.AraciKodu = item.AraciKodu;

                liste.Add(model);
            }

            return liste;
        }

        public List<BildirgeTakipModel> BilgiTakipList(int? subeId)
        {
            var liste = new List<BildirgeTakipModel>();

            foreach (var item in data.Bildirge_Turleris)
            {
                var model = new BildirgeTakipModel();

                model.BtnId = item.BtnId;
                model.IconId = item.IconId;
                model.Id = item.Id;
                model.IsSecili = false;
                model.TurAciklama = item.Tur_Key;
                model.TurKod = item.Tur;

                liste.Add(model);
            }

            if (subeId !=null)
            {
                var data_takip_listesi = data.Firma_Bildirge_Takips.Where(x => x.FirmaSgkId == subeId).ToList();

                foreach (var item in liste)
                {
                    var takip = data_takip_listesi.Where(x => x.BildirgeTurId == item.Id).SingleOrDefault();

                    if (takip!=null)
                    {
                        item.IsSecili = true;
                    }
                }
            }

            return liste;
        }

       
    }
}
