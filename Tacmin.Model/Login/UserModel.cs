using Tacmin.Core.Model;

namespace Tacmin.Model.Login
{
    public class UserModel : BaseUserModel
    {
        public string NtbKodu { get; set; }

        public string NtbSifre { get; set; }

        public string GibKodu { get; set; }

        public string GibSifre { get; set; }
        public string DataAdi { get; set; }

        public bool IsBuyukHarf { get; set; }
    }
}
