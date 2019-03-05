using C.I.M.S_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace C.I.M.S_WPF.Model
{
    [Serializable]
    public class TradeRecord : BindableBase
    {
        public TradeType TradeType { get; set; }
        public string ItemName { get; set; }
        public double Amount { get; set; }
        public double UnitPrice { get; set; }
        public double TotalCost { get; set; }
        public double RelativeFee { get; set; }
        public double AbsoluteFee { get; set; }
        public string Description { get; set; }
        public DateTime RegisterTime { get; set; }

        public TradeRecord(TradeType type, string name, double amount, double unitPrice, double rFee, double aFee, string desc)
        {
            TradeType = type;
            ItemName = name;
            Amount = amount;
            UnitPrice = unitPrice;
            TotalCost = amount * unitPrice;
            RelativeFee = rFee;
            AbsoluteFee = aFee;
            Description = desc;
            RegisterTime = DateTime.Now;
        }
    }
}
