using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;

namespace DataIslemler.Mukellef
{
    public class ExcelIslem
    {
        private Musavir_DbDataContext data;

        private List<FIRMA_TANIMLARI> data_firma_listesi;

        public ExcelIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_firma_listesi = data.FIRMA_TANIMLARIs.ToList();
        }

        public KayitResModel MukellefGibBilgileriGuncelle(List<Tacmin.Model.LocalData.FirmaModel> clientList)
        {
            var res_model = new KayitResModel();

            res_model.IslemDurum = true;

            try
            {
                foreach (var item in clientList)
                {
                    var model = data_firma_listesi.Where(x => x.ID == item.Id).SingleOrDefault();

                    if (model != null)
                    {
                        model.GIB_KODU = item.GibKodu;
                        model.GIB_PAROLA = item.GibParola;
                        model.GIB_SIFRE = item.GibSifre;

                        data.SubmitChanges();
                    }
                }

                res_model.IslemDurum = true;
                res_model.Mesaj = "Mükellef Gib Bilgileri Güncellendi";
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }
    }
}
