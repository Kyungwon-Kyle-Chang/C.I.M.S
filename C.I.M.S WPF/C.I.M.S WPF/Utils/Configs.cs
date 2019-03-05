using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.Utils
{
    public static class Configs
    {
        public const string MAIN_TITLE = "Cryptocurrency Investment Management System";
        public const string webAPI = "https://api.upbit.com/";
        public const string uriGetMarkets = "v1/market/all";

        public static string UriGetTickerOf(string market) { return $"v1/ticker?markets={market}"; }
    }
}
