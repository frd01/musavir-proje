using DataIslemler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tacmin.Core.Extensions;
using Tacmin.Core.Interface;
using Tacmin.Model;
using Tacmin.Model.LocalData;
using Tacmin.Model.Login;
using Tacmin.Model.Main;
using Tacmin.Model.Mukellef;

namespace Tacmin.Service
{
    public class MainService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.LocalData.ListeIslem localIslem;
        private DataIslemler.Main.ListeIslem listeIslem;
        private DataIslemler.Main.DataIslem dataIslem;

        public MainService(IVirtualContext _vctx)
        {
            vctx = _vctx;

            localIslem = new DataIslemler.LocalData.ListeIslem(vctx.DataAdi);

            listeIslem = new DataIslemler.Main.ListeIslem(vctx.DataAdi);

            dataIslem = new DataIslemler.Main.DataIslem(vctx.DataAdi);
        }

        public LocalDataModel LocalListeler()
        {

            var result = localIslem.LocalListeler();

            return result;

        }

        public List<FirmaGibModel> GetGibFirmaListesi()
        {
            var result = listeIslem.GetGibFirmaListesi();

            return result;
        }
    }
}
