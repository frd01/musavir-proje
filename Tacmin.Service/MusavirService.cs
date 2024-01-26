using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Model.Musavir;

namespace Tacmin.Service
{
    public class MusavirService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.Musavir.ListeIslem listeIslem;

        public MusavirService(IVirtualContext _vctx)
        {
            vctx = _vctx;
            listeIslem = new DataIslemler.Musavir.ListeIslem(vctx.DataAdi);
        }

        public FirmaIntModel GetMusavirBilgi()
        {
            var result = listeIslem.GetMusavirBilgi(vctx.UserId);

            return result;
        }

        public List<VergiTurModel> VergiTurListesi()
        {

            var result = listeIslem.VergiTurListesi();

            return result;
        }
    }
}
