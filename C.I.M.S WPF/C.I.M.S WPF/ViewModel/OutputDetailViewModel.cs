using C.I.M.S_WPF.Model;
using C.I.M.S_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.ViewModel
{
    public class OutputDetailViewModel : ViewModelBase
    {
        public InvestInfo InvestInfo { get; private set; }
        private TradeManager _tradeManager;

        public CommandBase TradeRecordDeleteButton { get; private set; }

        public OutputDetailViewModel(InvestInfo investInfo)
        {
            InvestInfo = investInfo;
            _tradeManager = new TradeManager();

            TradeRecordDeleteButton = new CommandBase(TradeRecordDeleteButtonExecute);
        }

        private void TradeRecordDeleteButtonExecute(object obj)
        {
            _tradeManager.Delete(InvestInfo, InvestInfo.TradeRecords.Last());
        }
    }
}
