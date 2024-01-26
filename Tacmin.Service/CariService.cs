using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Model.Cari;

namespace Tacmin.Service
{
    public class CariService
    {
        private readonly IVirtualContext vctx;

        private DataIslemler.Cari.ListeIslem listeIslem;

        public CariService(IVirtualContext _vctx)
        {
            vctx = _vctx;

            listeIslem = new DataIslemler.Cari.ListeIslem(vctx.DataAdi);
        }

        public List<CariListeModel> CariListesi()
        {
            var result = listeIslem.CariListesi();

            return result;
        }

        public List<CariHareketDetay> CariHareketListesi(int cariId, DateTime? ilkTarih, DateTime? sonTarih)
        {
            var result = listeIslem.CariHareketListesi(cariId, ilkTarih, sonTarih);

            return result;
        }
    }
}
