using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.Model
{
    public class MarketType
    {
        public string market { get; set; }
        public string korean_name { get; set; }
        public string english_name { get; set; }
        public string coin_name
        {
            get { return market.Split(delim)[1]; }
        }

        private char[] delim = { '-' };
    }
}
