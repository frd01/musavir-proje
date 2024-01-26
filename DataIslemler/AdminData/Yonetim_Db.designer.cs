﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataIslemler.AdminData
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Yonetim_Musavir_Db")]
	public partial class Yonetim_DbDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSmsFirmalar(SmsFirmalar instance);
    partial void UpdateSmsFirmalar(SmsFirmalar instance);
    partial void DeleteSmsFirmalar(SmsFirmalar instance);
    partial void InsertKULLANICI_TANIMLARI(KULLANICI_TANIMLARI instance);
    partial void UpdateKULLANICI_TANIMLARI(KULLANICI_TANIMLARI instance);
    partial void DeleteKULLANICI_TANIMLARI(KULLANICI_TANIMLARI instance);
    partial void InsertKullaniciGirisleri(KullaniciGirisleri instance);
    partial void UpdateKullaniciGirisleri(KullaniciGirisleri instance);
    partial void DeleteKullaniciGirisleri(KullaniciGirisleri instance);
    #endregion
		
		public Yonetim_DbDataContext() : 
				base(global::DataIslemler.Properties.Settings.Default.Yonetim_Musavir_DbConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public Yonetim_DbDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Yonetim_DbDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Yonetim_DbDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Yonetim_DbDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SmsFirmalar> SmsFirmalars
		{
			get
			{
				return this.GetTable<SmsFirmalar>();
			}
		}
		
		public System.Data.Linq.Table<KULLANICI_TANIMLARI> KULLANICI_TANIMLARIs
		{
			get
			{
				return this.GetTable<KULLANICI_TANIMLARI>();
			}
		}
		
		public System.Data.Linq.Table<KullaniciGirisleri> KullaniciGirisleris
		{
			get
			{
				return this.GetTable<KullaniciGirisleri>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetKullaniciListesi")]
		public ISingleResult<GetKullaniciListesiResult> GetKullaniciListesi()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<GetKullaniciListesiResult>)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SmsFirmalar")]
	public partial class SmsFirmalar : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _FirmaAdi;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnFirmaAdiChanging(string value);
    partial void OnFirmaAdiChanged();
    #endregion
		
		public SmsFirmalar()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirmaAdi", DbType="NVarChar(250)")]
		public string FirmaAdi
		{
			get
			{
				return this._FirmaAdi;
			}
			set
			{
				if ((this._FirmaAdi != value))
				{
					this.OnFirmaAdiChanging(value);
					this.SendPropertyChanging();
					this._FirmaAdi = value;
					this.SendPropertyChanged("FirmaAdi");
					this.OnFirmaAdiChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.KULLANICI_TANIMLARI")]
	public partial class KULLANICI_TANIMLARI : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _KULLANICI_KODU;
		
		private string _SIFRE;
		
		private System.Nullable<bool> _AKTIF;
		
		private string _YETKILER;
		
		private System.Nullable<bool> _YETKILI;
		
		private string _ADI;
		
		private string _SOYADI;
		
		private string _GIB_KODU;
		
		private string _GIB_SIFRE;
		
		private string _NTB_KODU;
		
		private string _NTB_SIFRE;
		
		private string _USERNAME;
		
		private string _PASSWORD;
		
		private string _AD;
		
		private string _SOYAD;
		
		private string _DATA_ADI;
		
		private string _Unvan;
		
		private string _Telefon;
		
		private string _Mail;
		
		private System.Nullable<int> _SehirId;
		
		private System.Nullable<int> _IlceId;
		
		private string _Text_Sifre;
		
		private string _Adres;
		
		private System.Nullable<System.DateTime> _LisansBaslangicTarihi;
		
		private System.Nullable<System.DateTime> _LisansBitisTarihi;
		
		private System.Nullable<int> _VergiDaireId;
		
		private string _VergiNo;
		
		private string _TcNo;
		
		private string _MysoftKullaniciAdi;
		
		private string _MysoftSifre;
		
		private string _MailKullaniciAdi;
		
		private string _MailSifre;
		
		private string _SmsKullaniciAdi;
		
		private string _SmsSifre;
		
		private string _GibParola;
		
		private System.Nullable<int> _SmsFirmaId;
		
		private string _SmsBaslik;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnKULLANICI_KODUChanging(string value);
    partial void OnKULLANICI_KODUChanged();
    partial void OnSIFREChanging(string value);
    partial void OnSIFREChanged();
    partial void OnAKTIFChanging(System.Nullable<bool> value);
    partial void OnAKTIFChanged();
    partial void OnYETKILERChanging(string value);
    partial void OnYETKILERChanged();
    partial void OnYETKILIChanging(System.Nullable<bool> value);
    partial void OnYETKILIChanged();
    partial void OnADIChanging(string value);
    partial void OnADIChanged();
    partial void OnSOYADIChanging(string value);
    partial void OnSOYADIChanged();
    partial void OnGIB_KODUChanging(string value);
    partial void OnGIB_KODUChanged();
    partial void OnGIB_SIFREChanging(string value);
    partial void OnGIB_SIFREChanged();
    partial void OnNTB_KODUChanging(string value);
    partial void OnNTB_KODUChanged();
    partial void OnNTB_SIFREChanging(string value);
    partial void OnNTB_SIFREChanged();
    partial void OnUSERNAMEChanging(string value);
    partial void OnUSERNAMEChanged();
    partial void OnPASSWORDChanging(string value);
    partial void OnPASSWORDChanged();
    partial void OnADChanging(string value);
    partial void OnADChanged();
    partial void OnSOYADChanging(string value);
    partial void OnSOYADChanged();
    partial void OnDATA_ADIChanging(string value);
    partial void OnDATA_ADIChanged();
    partial void OnUnvanChanging(string value);
    partial void OnUnvanChanged();
    partial void OnTelefonChanging(string value);
    partial void OnTelefonChanged();
    partial void OnMailChanging(string value);
    partial void OnMailChanged();
    partial void OnSehirIdChanging(System.Nullable<int> value);
    partial void OnSehirIdChanged();
    partial void OnIlceIdChanging(System.Nullable<int> value);
    partial void OnIlceIdChanged();
    partial void OnText_SifreChanging(string value);
    partial void OnText_SifreChanged();
    partial void OnAdresChanging(string value);
    partial void OnAdresChanged();
    partial void OnLisansBaslangicTarihiChanging(System.Nullable<System.DateTime> value);
    partial void OnLisansBaslangicTarihiChanged();
    partial void OnLisansBitisTarihiChanging(System.Nullable<System.DateTime> value);
    partial void OnLisansBitisTarihiChanged();
    partial void OnVergiDaireIdChanging(System.Nullable<int> value);
    partial void OnVergiDaireIdChanged();
    partial void OnVergiNoChanging(string value);
    partial void OnVergiNoChanged();
    partial void OnTcNoChanging(string value);
    partial void OnTcNoChanged();
    partial void OnMysoftKullaniciAdiChanging(string value);
    partial void OnMysoftKullaniciAdiChanged();
    partial void OnMysoftSifreChanging(string value);
    partial void OnMysoftSifreChanged();
    partial void OnMailKullaniciAdiChanging(string value);
    partial void OnMailKullaniciAdiChanged();
    partial void OnMailSifreChanging(string value);
    partial void OnMailSifreChanged();
    partial void OnSmsKullaniciAdiChanging(string value);
    partial void OnSmsKullaniciAdiChanged();
    partial void OnSmsSifreChanging(string value);
    partial void OnSmsSifreChanged();
    partial void OnGibParolaChanging(string value);
    partial void OnGibParolaChanged();
    partial void OnSmsFirmaIdChanging(System.Nullable<int> value);
    partial void OnSmsFirmaIdChanged();
    partial void OnSmsBaslikChanging(string value);
    partial void OnSmsBaslikChanged();
    #endregion
		
		public KULLANICI_TANIMLARI()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_KULLANICI_KODU", DbType="NVarChar(150)")]
		public string KULLANICI_KODU
		{
			get
			{
				return this._KULLANICI_KODU;
			}
			set
			{
				if ((this._KULLANICI_KODU != value))
				{
					this.OnKULLANICI_KODUChanging(value);
					this.SendPropertyChanging();
					this._KULLANICI_KODU = value;
					this.SendPropertyChanged("KULLANICI_KODU");
					this.OnKULLANICI_KODUChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SIFRE", DbType="NVarChar(32)")]
		public string SIFRE
		{
			get
			{
				return this._SIFRE;
			}
			set
			{
				if ((this._SIFRE != value))
				{
					this.OnSIFREChanging(value);
					this.SendPropertyChanging();
					this._SIFRE = value;
					this.SendPropertyChanged("SIFRE");
					this.OnSIFREChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AKTIF", DbType="Bit")]
		public System.Nullable<bool> AKTIF
		{
			get
			{
				return this._AKTIF;
			}
			set
			{
				if ((this._AKTIF != value))
				{
					this.OnAKTIFChanging(value);
					this.SendPropertyChanging();
					this._AKTIF = value;
					this.SendPropertyChanged("AKTIF");
					this.OnAKTIFChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_YETKILER", DbType="NVarChar(MAX)")]
		public string YETKILER
		{
			get
			{
				return this._YETKILER;
			}
			set
			{
				if ((this._YETKILER != value))
				{
					this.OnYETKILERChanging(value);
					this.SendPropertyChanging();
					this._YETKILER = value;
					this.SendPropertyChanged("YETKILER");
					this.OnYETKILERChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_YETKILI", DbType="Bit")]
		public System.Nullable<bool> YETKILI
		{
			get
			{
				return this._YETKILI;
			}
			set
			{
				if ((this._YETKILI != value))
				{
					this.OnYETKILIChanging(value);
					this.SendPropertyChanging();
					this._YETKILI = value;
					this.SendPropertyChanged("YETKILI");
					this.OnYETKILIChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ADI", DbType="NVarChar(70)")]
		public string ADI
		{
			get
			{
				return this._ADI;
			}
			set
			{
				if ((this._ADI != value))
				{
					this.OnADIChanging(value);
					this.SendPropertyChanging();
					this._ADI = value;
					this.SendPropertyChanged("ADI");
					this.OnADIChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SOYADI", DbType="NVarChar(70)")]
		public string SOYADI
		{
			get
			{
				return this._SOYADI;
			}
			set
			{
				if ((this._SOYADI != value))
				{
					this.OnSOYADIChanging(value);
					this.SendPropertyChanging();
					this._SOYADI = value;
					this.SendPropertyChanged("SOYADI");
					this.OnSOYADIChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GIB_KODU", DbType="NVarChar(10)")]
		public string GIB_KODU
		{
			get
			{
				return this._GIB_KODU;
			}
			set
			{
				if ((this._GIB_KODU != value))
				{
					this.OnGIB_KODUChanging(value);
					this.SendPropertyChanging();
					this._GIB_KODU = value;
					this.SendPropertyChanged("GIB_KODU");
					this.OnGIB_KODUChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GIB_SIFRE", DbType="NVarChar(10)")]
		public string GIB_SIFRE
		{
			get
			{
				return this._GIB_SIFRE;
			}
			set
			{
				if ((this._GIB_SIFRE != value))
				{
					this.OnGIB_SIFREChanging(value);
					this.SendPropertyChanging();
					this._GIB_SIFRE = value;
					this.SendPropertyChanged("GIB_SIFRE");
					this.OnGIB_SIFREChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NTB_KODU", DbType="NVarChar(11)")]
		public string NTB_KODU
		{
			get
			{
				return this._NTB_KODU;
			}
			set
			{
				if ((this._NTB_KODU != value))
				{
					this.OnNTB_KODUChanging(value);
					this.SendPropertyChanging();
					this._NTB_KODU = value;
					this.SendPropertyChanged("NTB_KODU");
					this.OnNTB_KODUChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NTB_SIFRE", DbType="NVarChar(10)")]
		public string NTB_SIFRE
		{
			get
			{
				return this._NTB_SIFRE;
			}
			set
			{
				if ((this._NTB_SIFRE != value))
				{
					this.OnNTB_SIFREChanging(value);
					this.SendPropertyChanging();
					this._NTB_SIFRE = value;
					this.SendPropertyChanged("NTB_SIFRE");
					this.OnNTB_SIFREChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_USERNAME", DbType="NVarChar(150)")]
		public string USERNAME
		{
			get
			{
				return this._USERNAME;
			}
			set
			{
				if ((this._USERNAME != value))
				{
					this.OnUSERNAMEChanging(value);
					this.SendPropertyChanging();
					this._USERNAME = value;
					this.SendPropertyChanged("USERNAME");
					this.OnUSERNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PASSWORD", DbType="NVarChar(32)")]
		public string PASSWORD
		{
			get
			{
				return this._PASSWORD;
			}
			set
			{
				if ((this._PASSWORD != value))
				{
					this.OnPASSWORDChanging(value);
					this.SendPropertyChanging();
					this._PASSWORD = value;
					this.SendPropertyChanged("PASSWORD");
					this.OnPASSWORDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AD", DbType="NVarChar(75)")]
		public string AD
		{
			get
			{
				return this._AD;
			}
			set
			{
				if ((this._AD != value))
				{
					this.OnADChanging(value);
					this.SendPropertyChanging();
					this._AD = value;
					this.SendPropertyChanged("AD");
					this.OnADChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SOYAD", DbType="NVarChar(75)")]
		public string SOYAD
		{
			get
			{
				return this._SOYAD;
			}
			set
			{
				if ((this._SOYAD != value))
				{
					this.OnSOYADChanging(value);
					this.SendPropertyChanging();
					this._SOYAD = value;
					this.SendPropertyChanged("SOYAD");
					this.OnSOYADChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DATA_ADI", DbType="VarChar(100)")]
		public string DATA_ADI
		{
			get
			{
				return this._DATA_ADI;
			}
			set
			{
				if ((this._DATA_ADI != value))
				{
					this.OnDATA_ADIChanging(value);
					this.SendPropertyChanging();
					this._DATA_ADI = value;
					this.SendPropertyChanged("DATA_ADI");
					this.OnDATA_ADIChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Unvan", DbType="NVarChar(200)")]
		public string Unvan
		{
			get
			{
				return this._Unvan;
			}
			set
			{
				if ((this._Unvan != value))
				{
					this.OnUnvanChanging(value);
					this.SendPropertyChanging();
					this._Unvan = value;
					this.SendPropertyChanged("Unvan");
					this.OnUnvanChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Telefon", DbType="VarChar(20)")]
		public string Telefon
		{
			get
			{
				return this._Telefon;
			}
			set
			{
				if ((this._Telefon != value))
				{
					this.OnTelefonChanging(value);
					this.SendPropertyChanging();
					this._Telefon = value;
					this.SendPropertyChanged("Telefon");
					this.OnTelefonChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Mail", DbType="NVarChar(150)")]
		public string Mail
		{
			get
			{
				return this._Mail;
			}
			set
			{
				if ((this._Mail != value))
				{
					this.OnMailChanging(value);
					this.SendPropertyChanging();
					this._Mail = value;
					this.SendPropertyChanged("Mail");
					this.OnMailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SehirId", DbType="Int")]
		public System.Nullable<int> SehirId
		{
			get
			{
				return this._SehirId;
			}
			set
			{
				if ((this._SehirId != value))
				{
					this.OnSehirIdChanging(value);
					this.SendPropertyChanging();
					this._SehirId = value;
					this.SendPropertyChanged("SehirId");
					this.OnSehirIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IlceId", DbType="Int")]
		public System.Nullable<int> IlceId
		{
			get
			{
				return this._IlceId;
			}
			set
			{
				if ((this._IlceId != value))
				{
					this.OnIlceIdChanging(value);
					this.SendPropertyChanging();
					this._IlceId = value;
					this.SendPropertyChanged("IlceId");
					this.OnIlceIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text_Sifre", DbType="VarChar(50)")]
		public string Text_Sifre
		{
			get
			{
				return this._Text_Sifre;
			}
			set
			{
				if ((this._Text_Sifre != value))
				{
					this.OnText_SifreChanging(value);
					this.SendPropertyChanging();
					this._Text_Sifre = value;
					this.SendPropertyChanged("Text_Sifre");
					this.OnText_SifreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Adres", DbType="NVarChar(MAX)")]
		public string Adres
		{
			get
			{
				return this._Adres;
			}
			set
			{
				if ((this._Adres != value))
				{
					this.OnAdresChanging(value);
					this.SendPropertyChanging();
					this._Adres = value;
					this.SendPropertyChanged("Adres");
					this.OnAdresChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LisansBaslangicTarihi", DbType="Date")]
		public System.Nullable<System.DateTime> LisansBaslangicTarihi
		{
			get
			{
				return this._LisansBaslangicTarihi;
			}
			set
			{
				if ((this._LisansBaslangicTarihi != value))
				{
					this.OnLisansBaslangicTarihiChanging(value);
					this.SendPropertyChanging();
					this._LisansBaslangicTarihi = value;
					this.SendPropertyChanged("LisansBaslangicTarihi");
					this.OnLisansBaslangicTarihiChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LisansBitisTarihi", DbType="Date")]
		public System.Nullable<System.DateTime> LisansBitisTarihi
		{
			get
			{
				return this._LisansBitisTarihi;
			}
			set
			{
				if ((this._LisansBitisTarihi != value))
				{
					this.OnLisansBitisTarihiChanging(value);
					this.SendPropertyChanging();
					this._LisansBitisTarihi = value;
					this.SendPropertyChanged("LisansBitisTarihi");
					this.OnLisansBitisTarihiChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VergiDaireId", DbType="Int")]
		public System.Nullable<int> VergiDaireId
		{
			get
			{
				return this._VergiDaireId;
			}
			set
			{
				if ((this._VergiDaireId != value))
				{
					this.OnVergiDaireIdChanging(value);
					this.SendPropertyChanging();
					this._VergiDaireId = value;
					this.SendPropertyChanged("VergiDaireId");
					this.OnVergiDaireIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VergiNo", DbType="VarChar(15)")]
		public string VergiNo
		{
			get
			{
				return this._VergiNo;
			}
			set
			{
				if ((this._VergiNo != value))
				{
					this.OnVergiNoChanging(value);
					this.SendPropertyChanging();
					this._VergiNo = value;
					this.SendPropertyChanged("VergiNo");
					this.OnVergiNoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TcNo", DbType="VarChar(15)")]
		public string TcNo
		{
			get
			{
				return this._TcNo;
			}
			set
			{
				if ((this._TcNo != value))
				{
					this.OnTcNoChanging(value);
					this.SendPropertyChanging();
					this._TcNo = value;
					this.SendPropertyChanged("TcNo");
					this.OnTcNoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MysoftKullaniciAdi", DbType="VarChar(150)")]
		public string MysoftKullaniciAdi
		{
			get
			{
				return this._MysoftKullaniciAdi;
			}
			set
			{
				if ((this._MysoftKullaniciAdi != value))
				{
					this.OnMysoftKullaniciAdiChanging(value);
					this.SendPropertyChanging();
					this._MysoftKullaniciAdi = value;
					this.SendPropertyChanged("MysoftKullaniciAdi");
					this.OnMysoftKullaniciAdiChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MysoftSifre", DbType="VarChar(150)")]
		public string MysoftSifre
		{
			get
			{
				return this._MysoftSifre;
			}
			set
			{
				if ((this._MysoftSifre != value))
				{
					this.OnMysoftSifreChanging(value);
					this.SendPropertyChanging();
					this._MysoftSifre = value;
					this.SendPropertyChanged("MysoftSifre");
					this.OnMysoftSifreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MailKullaniciAdi", DbType="VarChar(150)")]
		public string MailKullaniciAdi
		{
			get
			{
				return this._MailKullaniciAdi;
			}
			set
			{
				if ((this._MailKullaniciAdi != value))
				{
					this.OnMailKullaniciAdiChanging(value);
					this.SendPropertyChanging();
					this._MailKullaniciAdi = value;
					this.SendPropertyChanged("MailKullaniciAdi");
					this.OnMailKullaniciAdiChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MailSifre", DbType="VarChar(50)")]
		public string MailSifre
		{
			get
			{
				return this._MailSifre;
			}
			set
			{
				if ((this._MailSifre != value))
				{
					this.OnMailSifreChanging(value);
					this.SendPropertyChanging();
					this._MailSifre = value;
					this.SendPropertyChanged("MailSifre");
					this.OnMailSifreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SmsKullaniciAdi", DbType="VarChar(150)")]
		public string SmsKullaniciAdi
		{
			get
			{
				return this._SmsKullaniciAdi;
			}
			set
			{
				if ((this._SmsKullaniciAdi != value))
				{
					this.OnSmsKullaniciAdiChanging(value);
					this.SendPropertyChanging();
					this._SmsKullaniciAdi = value;
					this.SendPropertyChanged("SmsKullaniciAdi");
					this.OnSmsKullaniciAdiChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SmsSifre", DbType="VarChar(50)")]
		public string SmsSifre
		{
			get
			{
				return this._SmsSifre;
			}
			set
			{
				if ((this._SmsSifre != value))
				{
					this.OnSmsSifreChanging(value);
					this.SendPropertyChanging();
					this._SmsSifre = value;
					this.SendPropertyChanged("SmsSifre");
					this.OnSmsSifreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GibParola", DbType="NVarChar(50)")]
		public string GibParola
		{
			get
			{
				return this._GibParola;
			}
			set
			{
				if ((this._GibParola != value))
				{
					this.OnGibParolaChanging(value);
					this.SendPropertyChanging();
					this._GibParola = value;
					this.SendPropertyChanged("GibParola");
					this.OnGibParolaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SmsFirmaId", DbType="Int")]
		public System.Nullable<int> SmsFirmaId
		{
			get
			{
				return this._SmsFirmaId;
			}
			set
			{
				if ((this._SmsFirmaId != value))
				{
					this.OnSmsFirmaIdChanging(value);
					this.SendPropertyChanging();
					this._SmsFirmaId = value;
					this.SendPropertyChanged("SmsFirmaId");
					this.OnSmsFirmaIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SmsBaslik", DbType="NVarChar(150)")]
		public string SmsBaslik
		{
			get
			{
				return this._SmsBaslik;
			}
			set
			{
				if ((this._SmsBaslik != value))
				{
					this.OnSmsBaslikChanging(value);
					this.SendPropertyChanging();
					this._SmsBaslik = value;
					this.SendPropertyChanged("SmsBaslik");
					this.OnSmsBaslikChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.KullaniciGirisleri")]
	public partial class KullaniciGirisleri : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private System.Nullable<int> _UserId;
		
		private System.Nullable<System.DateTime> _Tarih;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnUserIdChanging(System.Nullable<int> value);
    partial void OnUserIdChanged();
    partial void OnTarihChanging(System.Nullable<System.DateTime> value);
    partial void OnTarihChanged();
    #endregion
		
		public KullaniciGirisleri()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int")]
		public System.Nullable<int> UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Tarih", DbType="Date")]
		public System.Nullable<System.DateTime> Tarih
		{
			get
			{
				return this._Tarih;
			}
			set
			{
				if ((this._Tarih != value))
				{
					this.OnTarihChanging(value);
					this.SendPropertyChanging();
					this._Tarih = value;
					this.SendPropertyChanged("Tarih");
					this.OnTarihChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	public partial class GetKullaniciListesiResult
	{
		
		private int _ID;
		
		private string _KULLANICI_KODU;
		
		private string _SIFRE;
		
		private System.Nullable<bool> _AKTIF;
		
		private string _YETKILER;
		
		private System.Nullable<bool> _YETKILI;
		
		private string _ADI;
		
		private string _SOYADI;
		
		private string _GIB_KODU;
		
		private string _GIB_SIFRE;
		
		private string _NTB_KODU;
		
		private string _NTB_SIFRE;
		
		private string _USERNAME;
		
		private string _PASSWORD;
		
		private string _AD;
		
		private string _SOYAD;
		
		private string _DATA_ADI;
		
		private string _Unvan;
		
		private string _Telefon;
		
		private string _Mail;
		
		private System.Nullable<int> _SehirId;
		
		private System.Nullable<int> _IlceId;
		
		private string _Text_Sifre;
		
		private string _Adres;
		
		private System.Nullable<System.DateTime> _LisansBaslangicTarihi;
		
		private System.Nullable<System.DateTime> _LisansBitisTarihi;
		
		private System.Nullable<int> _VergiDaireId;
		
		private string _VergiNo;
		
		private string _TcNo;
		
		public GetKullaniciListesiResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="Int NOT NULL")]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_KULLANICI_KODU", DbType="NVarChar(50)")]
		public string KULLANICI_KODU
		{
			get
			{
				return this._KULLANICI_KODU;
			}
			set
			{
				if ((this._KULLANICI_KODU != value))
				{
					this._KULLANICI_KODU = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SIFRE", DbType="NVarChar(32)")]
		public string SIFRE
		{
			get
			{
				return this._SIFRE;
			}
			set
			{
				if ((this._SIFRE != value))
				{
					this._SIFRE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AKTIF", DbType="Bit")]
		public System.Nullable<bool> AKTIF
		{
			get
			{
				return this._AKTIF;
			}
			set
			{
				if ((this._AKTIF != value))
				{
					this._AKTIF = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_YETKILER", DbType="NVarChar(MAX)")]
		public string YETKILER
		{
			get
			{
				return this._YETKILER;
			}
			set
			{
				if ((this._YETKILER != value))
				{
					this._YETKILER = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_YETKILI", DbType="Bit")]
		public System.Nullable<bool> YETKILI
		{
			get
			{
				return this._YETKILI;
			}
			set
			{
				if ((this._YETKILI != value))
				{
					this._YETKILI = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ADI", DbType="NVarChar(70)")]
		public string ADI
		{
			get
			{
				return this._ADI;
			}
			set
			{
				if ((this._ADI != value))
				{
					this._ADI = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SOYADI", DbType="NVarChar(70)")]
		public string SOYADI
		{
			get
			{
				return this._SOYADI;
			}
			set
			{
				if ((this._SOYADI != value))
				{
					this._SOYADI = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GIB_KODU", DbType="NVarChar(10)")]
		public string GIB_KODU
		{
			get
			{
				return this._GIB_KODU;
			}
			set
			{
				if ((this._GIB_KODU != value))
				{
					this._GIB_KODU = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GIB_SIFRE", DbType="NVarChar(10)")]
		public string GIB_SIFRE
		{
			get
			{
				return this._GIB_SIFRE;
			}
			set
			{
				if ((this._GIB_SIFRE != value))
				{
					this._GIB_SIFRE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NTB_KODU", DbType="NVarChar(11)")]
		public string NTB_KODU
		{
			get
			{
				return this._NTB_KODU;
			}
			set
			{
				if ((this._NTB_KODU != value))
				{
					this._NTB_KODU = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NTB_SIFRE", DbType="NVarChar(10)")]
		public string NTB_SIFRE
		{
			get
			{
				return this._NTB_SIFRE;
			}
			set
			{
				if ((this._NTB_SIFRE != value))
				{
					this._NTB_SIFRE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_USERNAME", DbType="NVarChar(50)")]
		public string USERNAME
		{
			get
			{
				return this._USERNAME;
			}
			set
			{
				if ((this._USERNAME != value))
				{
					this._USERNAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PASSWORD", DbType="NVarChar(32)")]
		public string PASSWORD
		{
			get
			{
				return this._PASSWORD;
			}
			set
			{
				if ((this._PASSWORD != value))
				{
					this._PASSWORD = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AD", DbType="NVarChar(75)")]
		public string AD
		{
			get
			{
				return this._AD;
			}
			set
			{
				if ((this._AD != value))
				{
					this._AD = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SOYAD", DbType="NVarChar(75)")]
		public string SOYAD
		{
			get
			{
				return this._SOYAD;
			}
			set
			{
				if ((this._SOYAD != value))
				{
					this._SOYAD = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DATA_ADI", DbType="VarChar(100)")]
		public string DATA_ADI
		{
			get
			{
				return this._DATA_ADI;
			}
			set
			{
				if ((this._DATA_ADI != value))
				{
					this._DATA_ADI = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Unvan", DbType="NVarChar(200)")]
		public string Unvan
		{
			get
			{
				return this._Unvan;
			}
			set
			{
				if ((this._Unvan != value))
				{
					this._Unvan = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Telefon", DbType="VarChar(20)")]
		public string Telefon
		{
			get
			{
				return this._Telefon;
			}
			set
			{
				if ((this._Telefon != value))
				{
					this._Telefon = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Mail", DbType="VarChar(20)")]
		public string Mail
		{
			get
			{
				return this._Mail;
			}
			set
			{
				if ((this._Mail != value))
				{
					this._Mail = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SehirId", DbType="Int")]
		public System.Nullable<int> SehirId
		{
			get
			{
				return this._SehirId;
			}
			set
			{
				if ((this._SehirId != value))
				{
					this._SehirId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IlceId", DbType="Int")]
		public System.Nullable<int> IlceId
		{
			get
			{
				return this._IlceId;
			}
			set
			{
				if ((this._IlceId != value))
				{
					this._IlceId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text_Sifre", DbType="VarChar(50)")]
		public string Text_Sifre
		{
			get
			{
				return this._Text_Sifre;
			}
			set
			{
				if ((this._Text_Sifre != value))
				{
					this._Text_Sifre = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Adres", DbType="NVarChar(MAX)")]
		public string Adres
		{
			get
			{
				return this._Adres;
			}
			set
			{
				if ((this._Adres != value))
				{
					this._Adres = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LisansBaslangicTarihi", DbType="Date")]
		public System.Nullable<System.DateTime> LisansBaslangicTarihi
		{
			get
			{
				return this._LisansBaslangicTarihi;
			}
			set
			{
				if ((this._LisansBaslangicTarihi != value))
				{
					this._LisansBaslangicTarihi = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LisansBitisTarihi", DbType="Date")]
		public System.Nullable<System.DateTime> LisansBitisTarihi
		{
			get
			{
				return this._LisansBitisTarihi;
			}
			set
			{
				if ((this._LisansBitisTarihi != value))
				{
					this._LisansBitisTarihi = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VergiDaireId", DbType="Int")]
		public System.Nullable<int> VergiDaireId
		{
			get
			{
				return this._VergiDaireId;
			}
			set
			{
				if ((this._VergiDaireId != value))
				{
					this._VergiDaireId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VergiNo", DbType="VarChar(15)")]
		public string VergiNo
		{
			get
			{
				return this._VergiNo;
			}
			set
			{
				if ((this._VergiNo != value))
				{
					this._VergiNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TcNo", DbType="VarChar(15)")]
		public string TcNo
		{
			get
			{
				return this._TcNo;
			}
			set
			{
				if ((this._TcNo != value))
				{
					this._TcNo = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
