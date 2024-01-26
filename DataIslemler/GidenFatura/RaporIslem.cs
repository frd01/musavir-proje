using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.EArsiv;

namespace DataIslemler.GidenFatura
{
    public class RaporIslem
    {
        private Musavir_DbDataContext data;

        public RaporIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<ArsivFaturaModel> SorguFaturaListesi(DateTime? tarih)
        {
            var liste = new List<ArsivFaturaModel>();

            foreach (var item in data.GetGidenFaturaListesi_SorguTarih(tarih))
            {
                var model = new ArsivFaturaModel();

                model.aliciUnvan = item.FaturaUnvan;
                model.aliciVergiNo = item.FirmaVergiNo;
                model.belgeNo = item.Fatura_No;
                model.belgeTuru = item.Belge_Turu;
                model.ettn = item.Ettn_No;
                model.firmaId = item.Firma_Id;
                model.hesaplananKdv_0 = item.KDV_0;
                model.hesaplananKdv_1 = item.Kdv_1;
                model.hesaplananKdv_10 = item.Kdv_10;
                model.hesaplananKdv_18 = item.Kdv_18;
                model.hesaplananKdv_20 = item.Kdv_20;
                model.hesaplananKdv_8 = item.Kdv_8;
                model.Id = item.Id;
                model.iskontoToplam = item.Iskonta_Toplam;
                model.kdvMatrah_0 = item.Matrah_0;
                model.kdvMatrah_1 = item.Matrah_1;
                model.kdvMatrah_10 = item.Matrah_10;
                model.kdvMatrah_18 = item.Matrah_18;
                model.kdvMatrah_20 = item.Matrah_20;
                model.kdvMatrah_8 = item.Matrah_8;
                model.malHizmetToplam = item.Mal_Hizmet_Toplami;
                model.odenecek = item.Odenecek_Toplam;
                model.onayDurumu = item.Onay_Durumu;
                model.paraBirimi = item.ParaBirimi;
                model.senaryo = item.Senorya;
                model.tarih = item.Tarih;
                model.tevfikatKdvTutar = item.TevfikatKdvToplam;
                model.tip = item.Fatura_Tipi;
                model.vergidahilToplam = item.Vergiler_Dahil_Toplam;
                model.Unvan = item.UNVAN;

                model.KdvToplam = item.Kdv_1 + item.Kdv_10 + item.Kdv_18 + item.Kdv_18 + item.Kdv_20 + item.Kdv_8;

                model.vergidahilToplam = model.KdvToplam - model.tevfikatKdvTutar;

                liste.Add(model);
            }

            return liste;
        }
    }
}
