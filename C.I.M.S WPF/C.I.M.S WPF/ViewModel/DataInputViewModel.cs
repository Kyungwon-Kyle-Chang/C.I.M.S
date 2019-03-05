using C.I.M.S_WPF.Model;
using C.I.M.S_WPF.Utils;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace C.I.M.S_WPF.ViewModel
{
    public class DataInputViewModel : ViewModelBase
    {
        public InvestInfo InvestInfo { get; private set; }

        public ObservableCollection<string> CoinTypes { get; private set; }

        private string _selectedCoin;
        public string SelectedCoin
        {
            get { return _selectedCoin; }
            set
            {
                if (CoinTypes.Contains(value))
                {
                    SetProperty(ref _selectedCoin, value);
                    EnableInputBoxes(InvestInfo.CurrentTradeType);
                }
            }
        }

        private string _amountText = "0";
        public string AmountText
        {
            get { return _amountText; }
            set
            {
                SetProperty(ref _amountText, value);
                CalculateTotalCost();
            }
        }

        private string _unitPriceText = "0";
        public string UnitPriceText
        {
            get { return _unitPriceText; }
            set
            {
                SetProperty(ref _unitPriceText, value);
                CalculateTotalCost();
            }
        }

        private string _rFeeText = "0";
        public string RFeeText
        {
            get { return _rFeeText; }
            set
            {
                SetProperty(ref _rFeeText, value);
                CalculateTotalCost();
            }
        }

        private string _aFeeText = "0";
        public string AFeeText
        {
            get { return _aFeeText; }
            set
            {
                SetProperty(ref _aFeeText, value);
                CalculateTotalCost();
            }
        }

        private string _descText;
        public string DescText
        {
            get { return _descText; }
            set { SetProperty(ref _descText, value); }
        }

        private double _totalCostText;
        public double TotalCostText
        {
            get { return _totalCostText; }
            set { SetProperty(ref _totalCostText, value); }
        }

        private bool _isCointypeEnabled = true;
        public bool IsCointypeEnabled
        {
            get { return _isCointypeEnabled; }
            set { SetProperty(ref _isCointypeEnabled, value); }
        }

        private bool _isUnitPriceEnabled = true;
        public bool IsUnitPriceEnabled
        {
            get { return _isUnitPriceEnabled; }
            set { SetProperty(ref _isUnitPriceEnabled, value); }
        }

        private bool _isRelativeFeeEnabled = true;
        public bool IsRelativeFeeEnabled
        {
            get { return _isRelativeFeeEnabled; }
            set { SetProperty(ref _isRelativeFeeEnabled, value); }
        }

        private bool _isAbsoluteFeeEnabled = true;
        public bool IsAbsoluteFeeEnabled
        {
            get { return _isAbsoluteFeeEnabled; }
            set { SetProperty(ref _isAbsoluteFeeEnabled, value); }
        }

        private bool _isNotCaptialTradeMode = true;

        public CommandBase EnterCommand { get; private set; }

        private WebConnector _webConnector;
        private TradeManager _tradeManager;

        public DataInputViewModel(InvestInfo investInfo)
        {
            InvestInfo = investInfo;

            Messenger.Instance.Register<TradeType>(this, OnTradeTypeChanged, Context.TRADETYPE_CHANGED);

            EnterCommand = new CommandBase(EnterCommandExecute, EnterCommandCanExecute);
            _tradeManager = new TradeManager();

            ConnectWebAPIs();
        }

        private void ConnectWebAPIs()
        {
            _webConnector = new WebConnector();
            var marketTypes = _webConnector.CallAPI<MarketType>(Configs.webAPI, Configs.uriGetMarkets, "Downloading Coin Types...");

            CoinTypes = new ObservableCollection<string>(marketTypes.Where(x => x.market.Contains("KRW")).Select(x => x.coin_name));
            CoinTypes.Insert(0, "WON");
            SelectedCoin = CoinTypes[0];
        }

        private void OnTradeTypeChanged(TradeType type)
        {
            if (type == TradeType.WITHDRAWAL)
                SelectedCoin = "WON";

            EnableInputBoxes(type);
        }

        private void EnableInputBoxes(TradeType type)
        {
            IsCointypeEnabled = true;
            IsUnitPriceEnabled = true;
            IsRelativeFeeEnabled = true;
            IsAbsoluteFeeEnabled = true;
            _isNotCaptialTradeMode = true;

            switch (type)
            {
                case TradeType.DEPOSIT:
                    AFeeText = "0";
                    RFeeText = "0";
                    IsRelativeFeeEnabled = false;
                    IsAbsoluteFeeEnabled = false;
                    if (SelectedCoin.Equals("WON"))
                    {
                        IsUnitPriceEnabled = false;
                        UnitPriceText = "0";
                    }
                    break;
                case TradeType.WITHDRAWAL:
                    IsCointypeEnabled = false;
                    UnitPriceText = "0";
                    IsUnitPriceEnabled = false;
                    break;
                case TradeType.TRANSFER:
                    UnitPriceText = "0";
                    IsUnitPriceEnabled = false;
                    break;
                case TradeType.BUY:
                    if (SelectedCoin.Equals("WON")) _isNotCaptialTradeMode = false;
                    break;
                case TradeType.SELL:
                    if (SelectedCoin.Equals("WON")) _isNotCaptialTradeMode = false;
                    break;
            }
        }

        private void EnterCommandExecute(object obj)
        {
            double amount, unitPrice, aFee, rFee;

            double.TryParse(AmountText, out amount);
            double.TryParse(UnitPriceText, out unitPrice);
            double.TryParse(AFeeText, out aFee);
            double.TryParse(RFeeText, out rFee);

            _tradeManager.Record(InvestInfo, 
                                new TradeRecord(InvestInfo.CurrentTradeType,
                                                SelectedCoin,
                                                amount,
                                                unitPrice,
                                                rFee,
                                                aFee,
                                                DescText));
        }

        private bool EnterCommandCanExecute(object obj)
        {
            return InvestInfo.CurrentTradeType != TradeType.DEFAULT && _isNotCaptialTradeMode
                    && RegexManager.IsNotBlank(SelectedCoin)
                    && RegexManager.IsTextUnsignedDouble(AmountText) && RegexManager.IsNotBlank(AmountText)
                    && RegexManager.IsTextUnsignedDouble(UnitPriceText) && RegexManager.IsNotBlank(UnitPriceText)
                    && RegexManager.IsTextUnsignedDouble(AFeeText) && RegexManager.IsNotBlank(AFeeText)
                    && RegexManager.IsTextUnsignedDouble(RFeeText) && RegexManager.IsNotBlank(RFeeText);
        }

        private void CalculateTotalCost()
        {
            double unitPrice, amount, aFee, rFee;
            if(double.TryParse(UnitPriceText, out unitPrice) && double.TryParse(AmountText, out amount) &&
                double.TryParse(AFeeText, out aFee) && double.TryParse(RFeeText, out rFee))
            {
                switch(InvestInfo.CurrentTradeType)
                {
                    case TradeType.DEPOSIT:
                        TotalCostText = amount;
                        break;
                    case TradeType.WITHDRAWAL:
                        TotalCostText = amount - aFee - (amount * rFee * 0.01);
                        break;
                    case TradeType.TRANSFER:
                        TotalCostText = amount - aFee - (amount * rFee * 0.01);
                        break;
                    case TradeType.BUY:
                        TotalCostText = amount * unitPrice * (1 + rFee * 0.01); 
                        break;
                    case TradeType.SELL:
                        TotalCostText = amount * unitPrice * (1 - rFee * 0.01) - aFee;
                        break;
                }
            }
        }
    }
}
