using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Core.Extensions;
using Tacmin.Model;


namespace DataIslemler.Islemler.Beyanname
{
    public class DataIslem
    {
        Musavir_DbDataContext data;

        private int inen_beyanname_sayisi = 0;
        private int inen_tahakkuk_sayisi = 0;

        private List<BEYANNAME_ICERIKLERI> data_beyanname_list;
        private List<FIRMA_TANIMLARI> data_firma_listesi;

        public int BeyannameSayisi
        {
            get
            {
                return inen_beyanname_sayisi;
            }
        }

        public int TahakkuhSayisi
        {
            get
            {
                return inen_tahakkuk_sayisi;
            }
        }

        public DataIslem(string dataAdi) 
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
            data_beyanname_list = data.BEYANNAME_ICERIKLERIs.ToList();
        }

        public void BeyannameKayit(BeyannameIcerik model,Tacmin.Data.DbModel.DOSYA beyanname, Tacmin.Data.DbModel.DOSYA tahakkuh,int? firmaId)
        {

            if(KayitDurum(model.BeyannameOid) == true)
            {

                var data_model = new BEYANNAME_ICERIKLERI();
                data_model.OID = model.BeyannameOid;
                data_model.SUBE_NO = model.Subeno;
                data_model.TAH_OID = model.TahakkukOid;
                data_model.KODU = model.BeyannameKodu;
                data_model.VERGI_DAIRESI = model.VergiDairesi;
                data_model.UNVAN = model.Unvan;
                data_model.TCKN = model.Tckn;
                data_model.DURUM = model.Durum;
                data_model.TUR = model.BeyannameTuru;
                data_model.GONDERIM_TARIHI = model.YuklenmeTarihi;
                data_model.DONEM_BAS = Convert.ToDateTime("01/" + model.Donem.SafeSubstring(0, 7));
                data_model.DONEM_SON = Convert.ToDateTime("15/" + model.Donem.SafeSubstring(8, 7));
                data_model.TAH_FIS_NO = model.fisNo;
                data_model.TAH_VADE = model.Vade;
                data_model.TAH_TUTAR = model.Tutar;
                data_model.FILE_ID = beyannameKaydi(beyanname);
                data_model.TAH_FILE_ID = tahakkuhKaydi(tahakkuh);
                data_model.FIRMA_ID = firmaId;

                data.BEYANNAME_ICERIKLERIs.InsertOnSubmit(data_model);

                data.SubmitChanges();

            }
        }


        private bool KayitDurum(string beyannameId)
        {

            var kontrol = data.BEYANNAME_ICERIKLERIs.Where(x => x.OID == beyannameId).Count();

            if (kontrol > 0)
                return false;

            return true;
        }

        private int beyannameKaydi(Tacmin.Data.DbModel.DOSYA dosya)
        {

            var model = new DataIslemler.Data.DOSYA();

            model.DOSYA_ADI = dosya.DOSYA_ADI;
            model.SIZE = dosya.SIZE;
            model.ICERIK = dosya.ICERIK;
            model.MIME_TYPE = dosya.MIME_TYPE;

            if (dosya.SIZE > 50)
                inen_beyanname_sayisi += 1; 

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
                inen_tahakkuk_sayisi += 1;

            data.DOSYAs.InsertOnSubmit(model);
            data.SubmitChanges();

            return model.ID;
        }

        public Tacmin.Data.DbModel.DOSYA GetBeyanname(int? id)
        {
            Tacmin.Data.DbModel.DOSYA model = null;
            try
            {
                var kontrol = data.BEYANNAME_ICERIKLERIs.Where(x => x.ID == id).Count();

                if(kontrol == 1)
                {
                    var dosya_id = data.BEYANNAME_ICERIKLERIs.Where(x => x.ID == id).SingleOrDefault().FILE_ID;

                    model = (from m in data.DOSYAs
                             where m.ID == dosya_id
                             select new Tacmin.Data.DbModel.DOSYA
                             {
                                 ID = m.ID,
                                 DOSYA_ADI = m.DOSYA_ADI,
                                 ICERIK = m.ICERIK.ToArray(),
                                 MIME_TYPE = m.MIME_TYPE,
                                 SIZE = m.SIZE

                             }).SingleOrDefault();
                }
            }
            catch 
            {

                model = null;
            }

            return model;
        }

        public Tacmin.Data.DbModel.DOSYA GetTahakkuh(int? id)
        {
            Tacmin.Data.DbModel.DOSYA model = null;
            try
            {
                var kontrol = data.BEYANNAME_ICERIKLERIs.Where(x => x.ID == id).Count();

                if (kontrol == 1)
                {
                    var dosya_id = data.BEYANNAME_ICERIKLERIs.Where(x => x.ID == id).SingleOrDefault().TAH_FILE_ID;

                    model = (from m in data.DOSYAs
                             where m.ID == dosya_id
                             select new Tacmin.Data.DbModel.DOSYA
                             {
                                 ID = m.ID,
                                 DOSYA_ADI = m.DOSYA_ADI,
                                 ICERIK = m.ICERIK.ToArray(),
                                 MIME_TYPE = m.MIME_TYPE,
                                 SIZE = m.SIZE

                             }).SingleOrDefault();
                }
            }
            catch
            {

                model = null;
            }

            return model;
        }

        public int BeyannameKontrol(string beyannameId,bool is_aktif_cari,string unvan)
        {
            var kontrol = data_beyanname_list.Where(x => x.OID == beyannameId).Count();
            try
            {
                
                if (is_aktif_cari == true)
                {
                    if (data_firma_listesi == null)
                    {
                        data_firma_listesi = data.FIRMA_TANIMLARIs.Where(x => x.AKTIF == true).ToList();
                    }

                    var cari_kontrol = data_firma_listesi.Where(x => x.UNVAN == unvan).Count();

                    if (cari_kontrol <= 0)
                        kontrol = 1;
                }


                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return kontrol;
        }
    }
}
