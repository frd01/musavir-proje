namespace Tacmin.Core.Model
{
    public class YetkilerModel
    {
        public static SistemYetkiModel SistemYetkiModel()
        {
            var model = new SistemYetkiModel();
            return model;
        }

        public SistemYetkiModel DASHBORD => SistemYetkiModel().Name(nameof(DASHBORD)).Desc("Anasayfa");

        #region Beyanname İşlemleri
        public SistemYetkiModel BEYANNAME_LIST_GORUNTULE => SistemYetkiModel().Name(nameof(BEYANNAME_LIST_GORUNTULE)).Desc("Beyanname Listesi Görüntüle");
        public SistemYetkiModel BEYANNAME_SORGULA => SistemYetkiModel().Name(nameof(BEYANNAME_SORGULA)).Desc("Beyanname Sorgula");
        public SistemYetkiModel BEYANNAME_TAHAKKUK_GORUNTULE => SistemYetkiModel().Name(nameof(BEYANNAME_TAHAKKUK_GORUNTULE)).Desc("Tahakkuk Görüntüle");
        public SistemYetkiModel BEYANNAME_BEYANNAME_GORUNTULE => SistemYetkiModel().Name(nameof(BEYANNAME_BEYANNAME_GORUNTULE)).Desc("Beyanname Görüntüle");
        #endregion

        #region Mükellef İşlemleri
        public SistemYetkiModel MUKELLEF_LIST_GORUNTULE => SistemYetkiModel().Name(nameof(MUKELLEF_LIST_GORUNTULE)).Desc("Mükellef Listesi Görüntüle");
        public SistemYetkiModel MUKELLEF_KAYIT_GOR => SistemYetkiModel().Name(nameof(MUKELLEF_KAYIT_GOR)).Desc("Mükellef Kart Gör");
        public SistemYetkiModel MUKELLEF_KAYIT_GUNCELLE => SistemYetkiModel().Name(nameof(MUKELLEF_KAYIT_GUNCELLE)).Desc("Mükellef Kart Güncelle");
        public SistemYetkiModel MUKELLEF_KAYIT_SIL => SistemYetkiModel().Name(nameof(MUKELLEF_KAYIT_SIL)).Desc("Mükellef Kart Sil");
        #endregion

        #region Beyanname İşlemleri
        public SistemYetkiModel BILDIRGE_LIST_GORUNTULE => SistemYetkiModel().Name(nameof(BILDIRGE_LIST_GORUNTULE)).Desc("Bildirge Listesi Görüntüle");
        public SistemYetkiModel BILDIRGE_SORGULA => SistemYetkiModel().Name(nameof(BILDIRGE_SORGULA)).Desc("Bildirge Sorgula");
        public SistemYetkiModel BILDIRGE_TAHAKKUK_GORUNTULE => SistemYetkiModel().Name(nameof(BILDIRGE_TAHAKKUK_GORUNTULE)).Desc("Tahakkuk Görüntüle");
        public SistemYetkiModel BILDIRGE_HIZLIST_GORUNTULE => SistemYetkiModel().Name(nameof(BILDIRGE_HIZLIST_GORUNTULE)).Desc("Hiz. List. Görüntüle");
        #endregion
    }
}
