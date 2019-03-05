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
    public class HoldingAsset : BindableBase
    {
        private string _assetName;
        public string AssetName
        {
            get { return _assetName; }
            set { SetProperty(ref _assetName, value); }
        }

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set
            {
                double newValue = Equals(AssetName, "WON") ? Math.Round(value) : value;
                SetProperty(ref _amount, newValue);
            }
        }

        private double _buyUnitPrice;
        public double BuyUnitPrice
        {
            get { return _buyUnitPrice; }
            set
            {
                SetProperty(ref _buyUnitPrice, value);
            }
        }

        private double _buyPrice = 0;
        public double BuyPrice
        {
            get { return _buyPrice; }
            set { SetProperty(ref _buyPrice, value); }
        }

        private double _currentPrice = 0;
        public double CurrentPrice
        {
            get { return _currentPrice; }
            set { SetProperty(ref _currentPrice, value); }
        }

        private double _currentUnitPrice = 0;
        public double CurrentUnitPrice
        {
            get { return _currentUnitPrice; }
            set
            {
                SetProperty(ref _currentUnitPrice, value);
                CalculatePrices();
                Messenger.Instance.Send(true, Context.EVALUATE_CAPITAL);
            }
        }

        private double _profit = 0;
        public double Profit
        {
            get { return _profit; }
            set
            {
                SetProperty(ref _profit, value);
                SetProfitColor(value);
            }
        }

        [NonSerialized]
        private Brush _profitColor = Brushes.Black;
        public Brush ProfitColor
        {
            get { return _profitColor; }
            set { SetProperty(ref _profitColor, value); }
        }

        [NonSerialized]
        private WebConnector _webConnector;

        public HoldingAsset(string assetName, double amount, double buyUnitPrice)
        {
            AssetName = assetName;
            Amount = amount;
            BuyUnitPrice = buyUnitPrice;

            GetCurrentUnitPrice(); 
        }

        ~HoldingAsset()
        {
            _webConnector?.CancelAsyncCall();
        }

        public void GetCurrentUnitPrice()
        {
            if (_webConnector != null) return;

            _webConnector = AssetName != "WON" ? new WebConnector() : null;
            _webConnector?.CallAPIAsync<Ticker>(
                Configs.webAPI,
                Configs.UriGetTickerOf("KRW-" + AssetName),
                500,
                (t) => { CurrentUnitPrice = t.First().trade_price; });
        }

        private void CalculatePrices()
        {
            BuyPrice = BuyUnitPrice * Amount;
            CurrentPrice = CurrentUnitPrice * Amount;
            Profit = BuyPrice != 0 ? ((CurrentPrice / BuyPrice) - 1) : 0;
        }

        private void SetProfitColor(double value)
        {
            if (value < 0)
            {
                ProfitColor = Brushes.Blue;
            }
            else if (value > 0)
            {
                ProfitColor = Brushes.Red;
            }
            else
            {
                ProfitColor = Brushes.Black;
            }
        }
    }
}
