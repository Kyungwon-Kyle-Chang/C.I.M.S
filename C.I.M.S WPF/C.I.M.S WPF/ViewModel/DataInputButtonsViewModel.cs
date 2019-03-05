using C.I.M.S_WPF.Model;
using C.I.M.S_WPF.Utils;
using C.I.M.S_WPF.Utils.DialogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.ViewModel
{
    public class DataInputButtonsViewModel : ViewModelBase
    {
        public InvestInfo InvestInfo { get; private set; }

        private bool _isDepositChecked;
        public bool IsDepositChecked
        {
            get { return _isDepositChecked; }
            set { SetProperty(ref _isDepositChecked, value); }
        }

        private bool _isWithdrawalChecked;
        public bool IsWithdrawalChecked
        {
            get { return _isWithdrawalChecked; }
            set { SetProperty(ref _isWithdrawalChecked, value); }
        }

        private bool _isTransferChecked;
        public bool IsTransferChecked
        {
            get { return _isTransferChecked; }
            set { SetProperty(ref _isTransferChecked, value); }
        }

        private bool _isBuyChecked;
        public bool IsBuyChecked
        {
            get { return _isBuyChecked; }
            set { SetProperty(ref _isBuyChecked, value); }
        }

        private bool _isSellChecked;
        public bool IsSellChecked
        {
            get { return _isSellChecked; }
            set { SetProperty(ref _isSellChecked, value); }
        }

        public CommandBase DepositCommand { get; private set; }
        public CommandBase WithdrawalCommand { get; private set; }
        public CommandBase TransferCommand { get; private set; }
        public CommandBase BuyCommand { get; private set; }
        public CommandBase SellCommand { get; private set; }
        public CommandBase ReviseCommand { get; private set; }
        
        public DataInputButtonsViewModel(InvestInfo investInfo)
        {
            InvestInfo = investInfo;

            Messenger.Instance.Register<TradeType>(this, SetButtonStates, Context.TRADETYPE_CHANGED);

            DepositCommand =    new CommandBase(x => InvestInfo.CurrentTradeType = TradeType.DEPOSIT);
            WithdrawalCommand = new CommandBase(x => InvestInfo.CurrentTradeType = TradeType.WITHDRAWAL);
            TransferCommand =   new CommandBase(x => InvestInfo.CurrentTradeType = TradeType.TRANSFER);
            BuyCommand =        new CommandBase(x => InvestInfo.CurrentTradeType = TradeType.BUY);
            SellCommand =       new CommandBase(x => InvestInfo.CurrentTradeType = TradeType.SELL);
            ReviseCommand =     new CommandBase(CallReviseInvestInfoWindow);
        }

        private void SetButtonStates(TradeType type)
        {
            IsDepositChecked = type == TradeType.DEPOSIT;
            IsWithdrawalChecked = type == TradeType.WITHDRAWAL;
            IsTransferChecked = type == TradeType.TRANSFER;
            IsBuyChecked = type == TradeType.BUY;
            IsSellChecked = type == TradeType.SELL;
        }

        private void CallReviseInvestInfoWindow(object obj)
        {
            DialogService.OpenDialog(new ReviseInvestInfoViewModel(InvestInfo));
        }
        
    }
}
