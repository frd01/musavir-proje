using DataIslemler.Data;
using DataIslemler.Helpers;
using System;
using System.Linq;
using Tacmin.Model;


namespace DataIslemler.Islemler.Bildirge
{
    public class DataIslem
    {
        Musavir_DbDataContext data;

        private int inen_tahakkuh_sayisi = 0;
        private int inen_bildirge_sayisi = 0;

        public int BildirgeSayisi
        {
            get
            {
                return inen_bildirge_sayisi;
            }
        }

        public int TahakkuhSayisi
        {
            get
            {
                return inen_tahakkuh_sayisi;
            }
        }

        public DataIslem(string dataAdi)
        {

            data = new Musavir_DbDataContext(new Baglanti(dataAdi).ConStr);
        }

        public void dataKayit(EBildirgeIcerik bildirge, Tacmin.Data.DbModel.DOSYA tah_dosya, Tacmin.Data.DbModel.DOSYA bey_dosya)
        {

            var model = new BILDIRGE_ICERIKLERI();

            var kayit_durum = bildirgeKontrol(bildirge.MuhSgkTahOid);
            if (kayit_durum == false)
                model = data.BILDIRGE_ICERIKLERIs.Where(x => x.TAH_OID == bildirge.MuhSgkTahOid).SingleOrDefault();

            model.SICIL_NO = bildirge.SicilNo;
            model.TAH_OID = bildirge.MuhSgkTahOid;
            model.OID = bildirge.MuhSgkBeyOid;
            model.TCKN = bildirge.Tckn;
            model.TUTAR = bildirge.Tutar;
            model.UNVAN = bildirge.Unvan;
            model.VADE = bildirge.Vade;
            model.TAH_FILE_ID = tahakkuhKaydi(tah_dosya);
            model.FILE_ID = bildirgeKaydi(bey_dosya);
            model.DONEM = bildirge.Donem;
            model.KANUN_NO = bildirge.Kanun;
            model.BELGE_CESIDI = bildirge.BelgeCesidi;
            model.FIRMA_ID = getFirmaId(bildirge.MuhSgkBeyOid);
            //model.DonemStr = bildirge.DonemStr;



            if (kayit_durum == true)
            {
                data.BILDIRGE_ICERIKLERIs.InsertOnSubmit(model);
                data.SubmitChanges();
            }
            else
            {
                data.SubmitChanges();
            }
        }

        private int bildirgeKaydi(Tacmin.Data.DbModel.DOSYA dosya)
        {

            var model = new DataIslemler.Data.DOSYA();

            model.DOSYA_ADI = dosya.DOSYA_ADI;
            model.SIZE = dosya.SIZE;
            model.ICERIK = dosya.ICERIK;
            model.MIME_TYPE = dosya.MIME_TYPE;

            if (dosya.SIZE > 50)
                inen_bildirge_sayisi += 1;

            data.DOSYAs.InsertOnSubmit(model);
            data.SubmitChanges();

            return model.ID;
        }

        private int tahakkuhKaydi(Tacmin.Data.DbModel.DOSYA dosya)
        {

            var model = new DataIslemler.Data.DOSYA();

            model.DOSYA_ADI = dosya.DOSYA_ADI;
            model.SIZE = dosya.SIZE;
            model.ICERIK = dosya.ICERIK;
            model.MIME_TYPE = dosya.MIME_TYPE;

            if (dosya.SIZE > 50)
                inen_tahakkuh_sayisi += 1;

            data.DOSYAs.InsertOnSubmit(model);
            data.SubmitChanges();

            return model.ID;
        }

        private bool bildirgeKontrol(string tahakkuhId)
        {
            var kayitDurum = false;

            try
            {
                var bildirge = (from m in data.BILDIRGE_ICERIKLERIs where m.TAH_OID == tahakkuhId select m).SingleOrDefault();

                if (bildirge == null)
                    kayitDurum = true;
            }
            catch
            {

                kayitDurum = true;
            }

            return kayitDurum;

        }

        private int? getFirmaId(string beyannameId)
        {

            try
            {
                return data.BEYANNAME_ICERIKLERIs.Where(x => x.OID == beyannameId).SingleOrDefault().FIRMA_ID;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
