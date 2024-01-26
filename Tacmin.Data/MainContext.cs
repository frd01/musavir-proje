using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Tacmin.Data.DbModel;

namespace Tacmin.Data
{
    public class MainContext : DbContext
    {
        public MainContext() : base(ConnectionStringBuilder.MainDbConStr())
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        

        public virtual DbSet<BEYANNAME_ICERIKLERI> BEYANNAME_ICERIKLERI { get; set; }
        public virtual DbSet<DOSYA> DOSYA { get; set; }
        public virtual DbSet<BILDIRGE_ICERIKLERI> BILDIRGE_ICERIKLERI { get; set; }
        public virtual DbSet<KULLANICI_TANIMLARI> KULLANICI_TANIMLARI { get; set; }
        public virtual DbSet<FIRMA_TANIMLARI> FIRMA_TANIMLARI { get; set; }
        public virtual DbSet<FIRMA_BAGLANTILARI> FIRMA_BAGLANTILARI { get; set; }
        public virtual DbSet<FIRMA_FAALIYET_KODLARI> FIRMA_FAALIYET_KODLARI { get; set; }
        public virtual DbSet<FIRMA_SUBE_TANIMLARI> FIRMA_SUBE_TANIMLARI { get; set; }
        public virtual DbSet<GELEN_FATURALAR> GELEN_FATURALAR { get; set; }
        public virtual DbSet<Giden_Faturalar> Giden_Faturalar { get; set; }
        public virtual DbSet<Vergi_Levhalari> Vergi_Levhalari { get; set; }
        public virtual DbSet<Tebligat_Dosyalar> Tebligat_Dosyalar { get; set; }
        public virtual DbSet<Vergi_Daireleri> Vergi_Daireleri { get; set; }
        public virtual DbSet<Pos_Bilgileri> Pos_Bilgileri { get; set; }
        public virtual DbSet<Firma_Iletisim> Firma_Iletisim { get; set; }

    }
}
