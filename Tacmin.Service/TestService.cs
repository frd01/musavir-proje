using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Model;

namespace Tacmin.Service
{
    public class TestService
    {
        private readonly IVirtualContext vctx;

        public TestService(IVirtualContext _vctx)
        {
            vctx = _vctx;
        }

        public GibLoginResultModel GetToken(string gibKodu,string gibSifre)
        {

            var giris_service = new DigitalVdIslemler.GirisIslem();

            var result = giris_service.IvdLogin(gibKodu, gibSifre);

            return result;
        }
    }
}
