using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Tebligat;

namespace DataIslemler.Tebligat
{
    public class DosyaIslem
    {
        private Musavir_DbDataContext data;

        public DosyaIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public TebligatDosyaModel GetDosya(int? tebligatId,string tur, TebligatModulEnums modulId) 
        {
            var liste = data.TebligatDosyalars.Where(x => x.TebligatId == tebligatId && x.Tur == tur && x.ModulId == (int)modulId).ToList();

            

            if (liste.Count()>0)
            {
                var data_model = liste[0];
                var model = new TebligatDosyaModel();

                model.Id = data_model.Id;
                model.DosyaAdi = data_model.DosyaAdi;
                model.Icerik = data_model.Icerik.ToArray();
                model.MimeType = data_model.MimeType;
                model.TebligatId = data_model.TebligatId;
                model.Tur = data_model.Tur;

                return model;
            }

            return null;
        }

        public List<TebligatDosyaModel> GetDosyalar(int? tebligatId,string tur, TebligatModulEnums modulId)
        {
            

            var liste = new List<TebligatDosyaModel>();

            var data_list = data.TebligatDosyalars.Where(x => x.TebligatId == tebligatId && x.Tur == tur && x.ModulId==(int)modulId).ToList();

            if (tur == "hepsi")
            {
                 data_list = data.TebligatDosyalars.Where(x => x.TebligatId == tebligatId && x.ModulId == (int)modulId).ToList();
            }

            foreach (var item in data_list)
            {
                var model = new TebligatDosyaModel();

                model.DosyaAdi = item.DosyaAdi;
                model.Icerik = item.Icerik.ToArray();
                model.Id = item.Id;
                model.MimeType = item.MimeType;
                model.TebligatId = item.TebligatId;
                model.Tur = item.Tur;

                liste.Add(model);
            }

            return liste;
        }
    }
}
