using C.I.M.S_WPF.Model;
using C.I.M.S_WPF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace C.I.M.S_WPF.ViewModel
{
    public class OutputSummaryViewModel : ViewModelBase
    {
        public InvestInfo InvestInfo { get; private set; }

        private double _currentCapital;
        public double CurrentCapital
        {
            get { return _currentCapital; }
            set
            {
                SetProperty(ref _currentCapital, value);
                CalculateProfit();
            }
        }

        private double _profit;
        public double Profit
        {
            get { return _profit; }
            set
            {
                SetProperty(ref _profit, value);
                SetProfitColor(value);
            }
        }

        private Brush _profitColor;
        public Brush ProfitColor
        {
            get { return _profitColor; }
            set { SetProperty(ref _profitColor, value); }
        }

        public OutputSummaryViewModel(InvestInfo investInfo)
        {
            InvestInfo = investInfo;

            Messenger.Instance.Register<bool>(this, CalculateCurrentCapital, Context.EVALUATE_CAPITAL);
        }

        private void CalculateCurrentCapital(bool state)
        {
            if (!state) return;

            var assets = InvestInfo?.HoldingAssets;
            if (assets == null) return;

            double capital = 0;
            foreach(HoldingAsset item in assets)
            {
                if(item.AssetName.Equals("WON"))
                {
                    capital += item.Amount; 
                }
                else
                {
                    capital += item.CurrentPrice;
                }
            }

            CurrentCapital = capital;
        }

        private void CalculateProfit()
        {
            double principal = InvestInfo.Principal;
            Profit = principal != 0 ? (CurrentCapital / principal) - 1 : 0;
        }

        private void SetProfitColor(double value)
        {
            if(value < 0)
            {
                ProfitColor = Brushes.Blue;
            }
            else if(value > 0)
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
