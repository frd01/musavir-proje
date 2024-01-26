using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Model.KontrolIslem;

namespace Tacmin.Service
{
    public class KontrolIslemService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.KontrolIslem.ListeIslem listeIslem;

        public KontrolIslemService(IVirtualContext _vctx)
        {
            vctx = _vctx;

            listeIslem = new DataIslemler.KontrolIslem.ListeIslem(vctx.DataAdi);
        }

        public List<FirmaKontrolModel> FirmaKontrolListesi()
        {
            var result = listeIslem.FirmaKontrolListesi();

            return result;
        }
    }
}
