using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace DataIslemler.Models.Gib
{
    public class GelenFaturaRequest
    {

        private List<Firma> _firmaListesi;


        [DisplayName("Roles")]

        public List<Firma> FirmaListesi
        {

            get
            {
                if (this._firmaListesi == null)
                    this._firmaListesi = new List<Firma>();
                return this._firmaListesi;
            }
            set
            {
                this._firmaListesi = value;
            }
        }

        public int Yil { get; set; }
        public int Ay { get; set; }
        [DisplayName("İlk Tarih")]
        public DateTime IlkTarih { get; set; }
        [DisplayName("Son Tarih")]
        public DateTime SonTarih { get; set; }

        public Firma Firma { get; set; }
        public int FirmaId { get; set; }

    }
}
