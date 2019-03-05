using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.Model
{
    public class Ticker
    {
        public string market;
        public string trade_date;
        public string trade_time;
        public string trade_date_kst;
        public string trade_time_kst;
        public long trade_timestamp;
        public long opening_price;
        public long high_price;
        public long low_price;
        public double trade_price;
        public long prev_closing_price;
        public string change;
        public long change_price;
        public long change_rate;
        public long signed_change_price;
        public long signed_change_rate;
        public long trade_volume;
        public long acc_trade_price;
        public long acc_trade_price_24h;
        public long acc_trade_volume;
        public long acc_trade_volume_24h;
        public long highest_52_week_price;
        public string highest_52_week_date;
        public long lowest_52_week_price;
        public string lowest_52_week_date;
        public long timestamp;
    }
}
