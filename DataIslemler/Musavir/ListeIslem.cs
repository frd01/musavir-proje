using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Helpers;
using Tacmin.Model.Musavir;

namespace DataIslemler.Musavir
{
    public class ListeIslem
    {
        DataIslemler.AdminData.Yonetim_DbDataContext adminData;
        
        DataIslemler.Data.Musavir_DbDataContext userData;

        public ListeIslem(string dataAdi)
        {
            var adm_con_str = new Baglanti(dataAdi).AdmConStr;

            adminData = new AdminData.Yonetim_DbDataContext(adm_con_str);
            var con_str = new Baglanti(dataAdi).ConStr;

            userData = new Data.Musavir_DbDataContext(con_str);
        }

        public FirmaIntModel GetMusavirBilgi(int userId)
        {
            var model = new FirmaIntModel();
            model.gib_kod = "";
            model.gib_parola = "";
            model.gib_sifre = "";
            model.vergi_no = "";
            model.tcNo = "";
            

            var item = adminData.KULLANICI_TANIMLARIs.Where(x => x.ID == userId).SingleOrDefault();

            if (item!=null)
            {
                model.id = item.ID;

                if (!string.IsNullOrEmpty(item.GIB_KODU))
                {
                    model.gib_kod = item.GIB_KODU;
                }
                if (!string.IsNullOrEmpty(item.GibParola))
                {
                    model.gib_parola = item.GibParola;
                }
                if (!string.IsNullOrEmpty(item.GIB_SIFRE))
                {
                    model.gib_sifre = item.GIB_SIFRE;
                }
                if (!string.IsNullOrEmpty(item.VergiNo))
                {
                    model.vergi_no = item.VergiNo;
                }
                if (!string.IsNullOrEmpty(item.TcNo))
                {
                    model.tcNo = item.TcNo;
                }

                model.is_kullanici = true;
                model.token = "";
                
            }

            return model;
        }

        public List<VergiTurModel> VergiTurListesi()
        {
            var liste = new List<VergiTurModel>();

            foreach (var item in userData.Vergi_Turleris)
            {
                var model = new VergiTurModel();

                model.Id = item.Id;
                model.VergiAdi = item.Vergi_Adi;
                model.VergiKodu = item.Vergi_Kodu;

                liste.Add(model);
            }

            return liste;
        }

        

        
    }
}
