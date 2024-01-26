using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;

namespace Tacmin.Service
{
    public class IslemService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.Ozel.DataIslem dataIslem;

        public IslemService(IVirtualContext _vctx)
        {
            vctx = _vctx;

            dataIslem = new DataIslemler.Ozel.DataIslem(vctx.DataAdi);
        }

        public void KullaniciGirisKaydi()
        {
            dataIslem.KullaniciGirisKaydi(vctx.UserId);
        }
    }
}
