using DataIslemler.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataIslemler.Listeler
{
    public class Genel
    {
        public List<YilModel> YilListesi()
        {

            var liste = new List<YilModel>();

            liste.Add(new YilModel { Id = 1, Yil = 2023 });

            return liste;
        }

        public List<AyModel> AyListesi()
        {

            var liste = new List<AyModel>();
            liste.Add(GetAyModel(1, "Ocak", 1, new DateTime(2023, 1, 1), new DateTime(2023, 1, 31), "OCAK"));
            liste.Add(GetAyModel(2, "Şubat", 2, new DateTime(2023, 2, 1), new DateTime(2023, 2, 28), "ŞUBAT"));
            liste.Add(GetAyModel(3, "Mart", 3, new DateTime(2023, 3, 1), new DateTime(2023, 3, 31), "MART"));
            liste.Add(GetAyModel(4, "Nisan", 4, new DateTime(2023, 4, 1), new DateTime(2023, 4, 30), "NİSAN"));
            liste.Add(GetAyModel(5, "Mayıs", 5, new DateTime(2023, 5, 1), new DateTime(2023, 5, 31), "MAYIS"));
            liste.Add(GetAyModel(6, "Haziran", 6, new DateTime(2023, 6, 1), new DateTime(2023, 6, 30), "HAZİRAN"));
            liste.Add(GetAyModel(7, "Temmuz", 7, new DateTime(2023, 7, 1), new DateTime(2023, 7, 31), "TEMMUZ"));
            liste.Add(GetAyModel(8, "Ağustos", 8, new DateTime(2023, 8, 1), new DateTime(2023, 8, 31), "AĞUSTOS"));
            liste.Add(GetAyModel(9, "Eylül", 9, new DateTime(2023, 9, 1), new DateTime(2023, 9, 30), "EYLÜL"));
            liste.Add(GetAyModel(10, "Ekim", 10, new DateTime(2023, 10, 1), new DateTime(2023, 10, 31), "EKİM"));
            liste.Add(GetAyModel(11, "Kasım", 11, new DateTime(2023, 11, 1), new DateTime(2023, 11, 30), "KASIM"));
            liste.Add(GetAyModel(12, "Aralık", 12, new DateTime(2023, 12, 1), new DateTime(2023, 12, 31), "ARALIK"));


            return liste;
        }

        public AyModel MukellefAyModel(int ay)
        {
            var model = AyListesi().Where(x => x.Ay == ay).SingleOrDefault();

            return model;
        }

        private AyModel GetAyModel(int id, string aystr, int ay, DateTime ilktarih, DateTime sontarih, string title)
        {
            return new AyModel { Id = id, AyStr = aystr, Ay = ay, IlkTarih = ilktarih, SonTarih = sontarih, Title = title };
        }
    }
}
