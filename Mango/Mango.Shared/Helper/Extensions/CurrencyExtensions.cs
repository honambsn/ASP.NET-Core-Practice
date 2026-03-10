using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Shared.Helper.Extensions
{
    public static class CurrencyExtensions
    {
        private static readonly CultureInfo USCulture = new CultureInfo("en-US");

        public static string ToUSD(this decimal value)
        {
            return USCulture.NumberFormat.CurrencySymbol + " " + value.ToString("N2", USCulture);
        }

        public static string ToUSD(this decimal? value)
        {
            if (value == null) return "$ 0.00";
            return USCulture.NumberFormat.CurrencySymbol + " " + value.Value.ToString("N2", USCulture);
        }

        public static string ToUSD(this double value)
        {
            return USCulture.NumberFormat.CurrencySymbol + " " + value.ToString("N2", USCulture);
        }

        public static string ToUSD(this double? value)
        {
            if (value == null) return "$ 0.00";
            return USCulture.NumberFormat.CurrencySymbol + " " + value.Value.ToString("N2", USCulture);
        }
    }
}
