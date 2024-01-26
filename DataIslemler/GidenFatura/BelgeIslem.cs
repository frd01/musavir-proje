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
    public class BelgeIslem
    {
        private Musavir_DbDataContext data;

        public BelgeIslem(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public FaturaDosyaModel GetBelge(string tur,int? id)
        {

            var kontrol = data.ArsivFaturalars.Where(x => x.FaturaId == id && x.Tur == tur).ToList();

            if (kontrol.Count>0)
            {
                var data_model = kontrol[0];

                var model = new FaturaDosyaModel();

                model.DosyaAdi = data_model.DosyaAdi;
                model.FaturaId = data_model.FaturaId;
                model.Icerik = data_model.Icerik.ToArray();
                model.Id = data_model.Id;
                model.MimeType = data_model.MimeType;
                model.Tur = data_model.Tur;

                return model;
            }

            return null;
        }
    }
}
