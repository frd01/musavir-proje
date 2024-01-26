namespace DataIslemler.Helpers
{
    public class Baglanti
    {

        private string dataAdi = "";

        public Baglanti(string _dataAdi)
        {

            dataAdi = _dataAdi;
        }

        private string local_server = "DESKTOP-LDDEBNJ";
#pragma warning disable CS0414 // The field 'Baglanti.server' is assigned but its value is never used
        //private string server = "YAZILIMSRV";
        private string server = "192.168.1.165";
#pragma warning restore CS0414 // The field 'Baglanti.server' is assigned but its value is never used
        public string ConStr
        {
            get
            {
                return $"Data Source={local_server};Initial Catalog={dataAdi};User ID=sa;Password=1";
            }
        }
        public string AdmConStr
        {
            get
            {
                return $"Data Source={local_server};Initial Catalog=Yonetim_Musavir_Db;User ID=sa;Password=1";
            }
        }
    }
}
