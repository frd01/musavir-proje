using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Main;

namespace DataIslemler.Main
{
    public class DataIslem
    {
        private Musavir_DbDataContext data;

        public DataIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public KayitResModel GunlukOtoSorguOzetKayit(List<SorguOzetModel> clientList)
        {

            var res_model = new KayitResModel();

            res_model.IslemDurum = true;

            try
            {
                foreach (var item in clientList)
                {
                    if (item.EvrakSayisi>0)
                    {
                        var model = new SorguOzet();
                        model.Aciklama = item.Aciklama;
                        model.EvrakSayisi = item.EvrakSayisi;
                        model.ModulAdi = item.ModulAdi;
                        model.Tarih = DateTime.Now;
                        model.Tarih_Zaman = model.Tarih;

                        data.SorguOzets.InsertOnSubmit(model);
                        data.SubmitChanges();
                    }
                }

                var takip_model = new SorguTakip();

                takip_model.OtomatikIndirme = DateTime.Now;

                data.SorguTakips.InsertOnSubmit(takip_model);
                data.SubmitChanges();

                res_model.IslemDurum = true;
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
