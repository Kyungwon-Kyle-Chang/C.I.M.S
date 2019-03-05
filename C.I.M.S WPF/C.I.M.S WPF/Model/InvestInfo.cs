using C.I.M.S_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.Model
{
    [Serializable]
    public class InvestInfo : BindableBase
    {
        private string _investmentTitle;
        public string InvestmentTitle
        {
            get { return _investmentTitle; }
            set { SetProperty(ref _investmentTitle, value); }
        }

        private double _principal = 0;
        public double Principal
        {
            get { return _principal; }
            set { SetProperty(ref _principal, value); }
        }

        private TradeType _currentTradeType = TradeType.DEFAULT;
        public TradeType CurrentTradeType
        {
            get { return _currentTradeType; }
            set
            {
                _currentTradeType = value;
                Messenger.Instance.Send(_currentTradeType, Context.TRADETYPE_CHANGED);
            }
        }

        public ObservableCollection<HoldingAsset> HoldingAssets { get; set; }
        public ObservableCollection<TradeRecord> TradeRecords { get; private set; }

        public InvestInfo(string title, double principal)
        {
            InvestmentTitle = title;
            Principal = principal;

            HoldingAssets = new ObservableCollection<HoldingAsset>();
            TradeRecords = new ObservableCollection<TradeRecord>();
        }

        public InvestInfo Run()
        {
            foreach(HoldingAsset item in HoldingAssets)
            {
                item.GetCurrentUnitPrice();
            }

            return this;
        }
    }
}
