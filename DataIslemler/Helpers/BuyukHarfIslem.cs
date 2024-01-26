using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIslemler.Helpers
{
    public class BuyukHarfIslem
    {
        private bool is_buyuk_harf;

        public BuyukHarfIslem(bool _is_buyuk_harf)
        {

            is_buyuk_harf = _is_buyuk_harf;
        }

        public string GetValueStr(string _value)
        {

            var value = _value;

            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            if (is_buyuk_harf == true)
                value = value.ToUpper();

            if (is_buyuk_harf == false)
                value = value.ToLower();

            return value;
        }
    }
}
