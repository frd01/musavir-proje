﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tacmin.Core.NPSKimlikDogrulamaServisi {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="NPSKimlikDogrulamaServisi.ServiceSoap")]
    public interface ServiceSoap {
        
        // CODEGEN: Generating message contract since message DisKullaniciKimlikDogrulaResponse has headers
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DisKullaniciKimlikDogrula", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Kayit))]
        Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaResponse DisKullaniciKimlikDogrula(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DisKullaniciKimlikDogrula", ReplyAction="*")]
        System.Threading.Tasks.Task<Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaResponse> DisKullaniciKimlikDogrulaAsync(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaRequest request);
        
        // CODEGEN: Generating message contract since message DisKullaniciKimlikDogrula2Response has headers
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DisKullaniciKimlikDogrula2", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Kayit))]
        Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Response DisKullaniciKimlikDogrula2(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DisKullaniciKimlikDogrula2", ReplyAction="*")]
        System.Threading.Tasks.Task<Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Response> DisKullaniciKimlikDogrula2Async(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Request request);
        
        // CODEGEN: Generating message contract since message BaglantiTestiResponse has headers
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BaglantiTesti", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Kayit))]
        Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiResponse BaglantiTesti(Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BaglantiTesti", ReplyAction="*")]
        System.Threading.Tasks.Task<Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiResponse> BaglantiTestiAsync(Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class DisKullaniciKimlik : Kayit {
        
        private ProgramAdi programAdiField;
        
        private string kimlikNOField;
        
        private KimlikNOTipi kimlikNOTipiField;
        
        private string noterlikKoduField;
        
        private string noterlikKullaniciAdiField;
        
        private DisKullaniciTipi disKullaniciTipiField;
        
        private string sifreField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public ProgramAdi ProgramAdi {
            get {
                return this.programAdiField;
            }
            set {
                this.programAdiField = value;
                this.RaisePropertyChanged("ProgramAdi");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string KimlikNO {
            get {
                return this.kimlikNOField;
            }
            set {
                this.kimlikNOField = value;
                this.RaisePropertyChanged("KimlikNO");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public KimlikNOTipi KimlikNOTipi {
            get {
                return this.kimlikNOTipiField;
            }
            set {
                this.kimlikNOTipiField = value;
                this.RaisePropertyChanged("KimlikNOTipi");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string NoterlikKodu {
            get {
                return this.noterlikKoduField;
            }
            set {
                this.noterlikKoduField = value;
                this.RaisePropertyChanged("NoterlikKodu");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string NoterlikKullaniciAdi {
            get {
                return this.noterlikKullaniciAdiField;
            }
            set {
                this.noterlikKullaniciAdiField = value;
                this.RaisePropertyChanged("NoterlikKullaniciAdi");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public DisKullaniciTipi DisKullaniciTipi {
            get {
                return this.disKullaniciTipiField;
            }
            set {
                this.disKullaniciTipiField = value;
                this.RaisePropertyChanged("DisKullaniciTipi");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string Sifre {
            get {
                return this.sifreField;
            }
            set {
                this.sifreField = value;
                this.RaisePropertyChanged("Sifre");
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum ProgramAdi {
        
        /// <remarks/>
        Belirtilmemis,
        
        /// <remarks/>
        Vezne,
        
        /// <remarks/>
        YazimNet,
        
        /// <remarks/>
        VezneNet,
        
        /// <remarks/>
        TNBOnline,
        
        /// <remarks/>
        NBS,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum KimlikNOTipi {
        
        /// <remarks/>
        Belirtilmemis,
        
        /// <remarks/>
        TCKN,
        
        /// <remarks/>
        VKN,
        
        /// <remarks/>
        MTKN,
        
        /// <remarks/>
        TCSN,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum DisKullaniciTipi {
        
        /// <remarks/>
        Belirtilmemis,
        
        /// <remarks/>
        NoterlikKullanicisi,
        
        /// <remarks/>
        MaliMusavir,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DisKullaniciKimlik))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public abstract partial class Kayit : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int idField;
        
        private DataRowState kayitDurumuField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("ID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public DataRowState KayitDurumu {
            get {
                return this.kayitDurumuField;
            }
            set {
                this.kayitDurumuField = value;
                this.RaisePropertyChanged("KayitDurumu");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.FlagsAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum DataRowState {
        
        /// <remarks/>
        Detached = 1,
        
        /// <remarks/>
        Unchanged = 2,
        
        /// <remarks/>
        Added = 4,
        
        /// <remarks/>
        Deleted = 8,
        
        /// <remarks/>
        Modified = 16,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Zaman : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int yilField;
        
        private int ayField;
        
        private int gunField;
        
        private int saatField;
        
        private int dakikaField;
        
        private int saniyeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Yil {
            get {
                return this.yilField;
            }
            set {
                this.yilField = value;
                this.RaisePropertyChanged("Yil");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int Ay {
            get {
                return this.ayField;
            }
            set {
                this.ayField = value;
                this.RaisePropertyChanged("Ay");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int Gun {
            get {
                return this.gunField;
            }
            set {
                this.gunField = value;
                this.RaisePropertyChanged("Gun");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public int Saat {
            get {
                return this.saatField;
            }
            set {
                this.saatField = value;
                this.RaisePropertyChanged("Saat");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public int Dakika {
            get {
                return this.dakikaField;
            }
            set {
                this.dakikaField = value;
                this.RaisePropertyChanged("Dakika");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public int Saniye {
            get {
                return this.saniyeField;
            }
            set {
                this.saniyeField = value;
                this.RaisePropertyChanged("Saniye");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ServisHata : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string mesajField;
        
        private string kodField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Mesaj {
            get {
                return this.mesajField;
            }
            set {
                this.mesajField = value;
                this.RaisePropertyChanged("Mesaj");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Kod {
            get {
                return this.kodField;
            }
            set {
                this.kodField = value;
                this.RaisePropertyChanged("Kod");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ServisHataHeader : object, System.ComponentModel.INotifyPropertyChanged {
        
        private ServisHata servisHataField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public ServisHata ServisHata {
            get {
                return this.servisHataField;
            }
            set {
                this.servisHataField = value;
                this.RaisePropertyChanged("ServisHata");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum NPSIslemTipi {
        
        /// <remarks/>
        Belirtilmemis,
        
        /// <remarks/>
        AracSatisi,
        
        /// <remarks/>
        DefterOnayi,
        
        /// <remarks/>
        SendikaUyelik,
        
        /// <remarks/>
        BelgeOnayi,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DisKullaniciKimlikDogrula", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DisKullaniciKimlikDogrulaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlik disKullaniciKimlik;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public Tacmin.Core.NPSKimlikDogrulamaServisi.NPSIslemTipi islemTipi;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public System.DateTime istemciTarihi;
        
        public DisKullaniciKimlikDogrulaRequest() {
        }
        
        public DisKullaniciKimlikDogrulaRequest(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlik disKullaniciKimlik, Tacmin.Core.NPSKimlikDogrulamaServisi.NPSIslemTipi islemTipi, System.DateTime istemciTarihi) {
            this.disKullaniciKimlik = disKullaniciKimlik;
            this.islemTipi = islemTipi;
            this.istemciTarihi = istemciTarihi;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DisKullaniciKimlikDogrulaResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DisKullaniciKimlikDogrulaResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public Tacmin.Core.NPSKimlikDogrulamaServisi.ServisHataHeader ServisHataHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public long DisKullaniciKimlikDogrulaResult;
        
        public DisKullaniciKimlikDogrulaResponse() {
        }
        
        public DisKullaniciKimlikDogrulaResponse(Tacmin.Core.NPSKimlikDogrulamaServisi.ServisHataHeader ServisHataHeader, long DisKullaniciKimlikDogrulaResult) {
            this.ServisHataHeader = ServisHataHeader;
            this.DisKullaniciKimlikDogrulaResult = DisKullaniciKimlikDogrulaResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DisKullaniciKimlikDogrula2", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DisKullaniciKimlikDogrula2Request {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlik disKullaniciKimlik;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public Tacmin.Core.NPSKimlikDogrulamaServisi.NPSIslemTipi islemTipi;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public Tacmin.Core.NPSKimlikDogrulamaServisi.Zaman istemciZamani;
        
        public DisKullaniciKimlikDogrula2Request() {
        }
        
        public DisKullaniciKimlikDogrula2Request(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlik disKullaniciKimlik, Tacmin.Core.NPSKimlikDogrulamaServisi.NPSIslemTipi islemTipi, Tacmin.Core.NPSKimlikDogrulamaServisi.Zaman istemciZamani) {
            this.disKullaniciKimlik = disKullaniciKimlik;
            this.islemTipi = islemTipi;
            this.istemciZamani = istemciZamani;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DisKullaniciKimlikDogrula2Response", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DisKullaniciKimlikDogrula2Response {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public Tacmin.Core.NPSKimlikDogrulamaServisi.ServisHataHeader ServisHataHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public long DisKullaniciKimlikDogrula2Result;
        
        public DisKullaniciKimlikDogrula2Response() {
        }
        
        public DisKullaniciKimlikDogrula2Response(Tacmin.Core.NPSKimlikDogrulamaServisi.ServisHataHeader ServisHataHeader, long DisKullaniciKimlikDogrula2Result) {
            this.ServisHataHeader = ServisHataHeader;
            this.DisKullaniciKimlikDogrula2Result = DisKullaniciKimlikDogrula2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BaglantiTesti", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BaglantiTestiRequest {
        
        public BaglantiTestiRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BaglantiTestiResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BaglantiTestiResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public Tacmin.Core.NPSKimlikDogrulamaServisi.ServisHataHeader ServisHataHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.DateTime BaglantiTestiResult;
        
        public BaglantiTestiResponse() {
        }
        
        public BaglantiTestiResponse(Tacmin.Core.NPSKimlikDogrulamaServisi.ServisHataHeader ServisHataHeader, System.DateTime BaglantiTestiResult) {
            this.ServisHataHeader = ServisHataHeader;
            this.BaglantiTestiResult = BaglantiTestiResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceSoapChannel : Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceSoapClient : System.ServiceModel.ClientBase<Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap>, Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap {
        
        public ServiceSoapClient() {
        }
        
        public ServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaResponse Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap.DisKullaniciKimlikDogrula(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaRequest request) {
            return base.Channel.DisKullaniciKimlikDogrula(request);
        }
        
        public Tacmin.Core.NPSKimlikDogrulamaServisi.ServisHataHeader DisKullaniciKimlikDogrula(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlik disKullaniciKimlik, Tacmin.Core.NPSKimlikDogrulamaServisi.NPSIslemTipi islemTipi, System.DateTime istemciTarihi, out long DisKullaniciKimlikDogrulaResult) {
            Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaRequest inValue = new Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaRequest();
            inValue.disKullaniciKimlik = disKullaniciKimlik;
            inValue.islemTipi = islemTipi;
            inValue.istemciTarihi = istemciTarihi;
            Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaResponse retVal = ((Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap)(this)).DisKullaniciKimlikDogrula(inValue);
            DisKullaniciKimlikDogrulaResult = retVal.DisKullaniciKimlikDogrulaResult;
            return retVal.ServisHataHeader;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaResponse> Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap.DisKullaniciKimlikDogrulaAsync(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaRequest request) {
            return base.Channel.DisKullaniciKimlikDogrulaAsync(request);
        }
        
        public System.Threading.Tasks.Task<Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaResponse> DisKullaniciKimlikDogrulaAsync(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlik disKullaniciKimlik, Tacmin.Core.NPSKimlikDogrulamaServisi.NPSIslemTipi islemTipi, System.DateTime istemciTarihi) {
            Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaRequest inValue = new Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrulaRequest();
            inValue.disKullaniciKimlik = disKullaniciKimlik;
            inValue.islemTipi = islemTipi;
            inValue.istemciTarihi = istemciTarihi;
            return ((Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap)(this)).DisKullaniciKimlikDogrulaAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Response Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap.DisKullaniciKimlikDogrula2(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Request request) {
            return base.Channel.DisKullaniciKimlikDogrula2(request);
        }
        
        public Tacmin.Core.NPSKimlikDogrulamaServisi.ServisHataHeader DisKullaniciKimlikDogrula2(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlik disKullaniciKimlik, Tacmin.Core.NPSKimlikDogrulamaServisi.NPSIslemTipi islemTipi, Tacmin.Core.NPSKimlikDogrulamaServisi.Zaman istemciZamani, out long DisKullaniciKimlikDogrula2Result) {
            Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Request inValue = new Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Request();
            inValue.disKullaniciKimlik = disKullaniciKimlik;
            inValue.islemTipi = islemTipi;
            inValue.istemciZamani = istemciZamani;
            Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Response retVal = ((Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap)(this)).DisKullaniciKimlikDogrula2(inValue);
            DisKullaniciKimlikDogrula2Result = retVal.DisKullaniciKimlikDogrula2Result;
            return retVal.ServisHataHeader;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Response> Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap.DisKullaniciKimlikDogrula2Async(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Request request) {
            return base.Channel.DisKullaniciKimlikDogrula2Async(request);
        }
        
        public System.Threading.Tasks.Task<Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Response> DisKullaniciKimlikDogrula2Async(Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlik disKullaniciKimlik, Tacmin.Core.NPSKimlikDogrulamaServisi.NPSIslemTipi islemTipi, Tacmin.Core.NPSKimlikDogrulamaServisi.Zaman istemciZamani) {
            Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Request inValue = new Tacmin.Core.NPSKimlikDogrulamaServisi.DisKullaniciKimlikDogrula2Request();
            inValue.disKullaniciKimlik = disKullaniciKimlik;
            inValue.islemTipi = islemTipi;
            inValue.istemciZamani = istemciZamani;
            return ((Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap)(this)).DisKullaniciKimlikDogrula2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiResponse Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap.BaglantiTesti(Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiRequest request) {
            return base.Channel.BaglantiTesti(request);
        }
        
        public Tacmin.Core.NPSKimlikDogrulamaServisi.ServisHataHeader BaglantiTesti(out System.DateTime BaglantiTestiResult) {
            Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiRequest inValue = new Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiRequest();
            Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiResponse retVal = ((Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap)(this)).BaglantiTesti(inValue);
            BaglantiTestiResult = retVal.BaglantiTestiResult;
            return retVal.ServisHataHeader;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiResponse> Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap.BaglantiTestiAsync(Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiRequest request) {
            return base.Channel.BaglantiTestiAsync(request);
        }
        
        public System.Threading.Tasks.Task<Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiResponse> BaglantiTestiAsync() {
            Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiRequest inValue = new Tacmin.Core.NPSKimlikDogrulamaServisi.BaglantiTestiRequest();
            return ((Tacmin.Core.NPSKimlikDogrulamaServisi.ServiceSoap)(this)).BaglantiTestiAsync(inValue);
        }
    }
}