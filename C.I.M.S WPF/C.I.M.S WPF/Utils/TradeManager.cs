using C.I.M.S_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.Utils
{
    public class TradeManager
    {
        private TradeRecordProcessor _recordProcessor;
        private TradeDeleteProcessor _deleteProcessor;

        private Dictionary<TradeType, Action<InvestInfo, TradeRecord>> _recordProcessDictionary;
        private Dictionary<TradeType, Action<InvestInfo, TradeRecord>> _deleteProcessDictionary;

        public TradeManager()
        {
            _recordProcessor = new TradeRecordProcessor();
            _deleteProcessor = new TradeDeleteProcessor();

            _recordProcessDictionary = new Dictionary<TradeType, Action<InvestInfo, TradeRecord>>();
            _recordProcessDictionary.Add(TradeType.DEPOSIT, _recordProcessor.RecordDeposit);
            _recordProcessDictionary.Add(TradeType.WITHDRAWAL, _recordProcessor.RecordWithdrawal);
            _recordProcessDictionary.Add(TradeType.TRANSFER, _recordProcessor.RecordTransfer);
            _recordProcessDictionary.Add(TradeType.BUY, _recordProcessor.RecordBuy);
            _recordProcessDictionary.Add(TradeType.SELL, _recordProcessor.RecordSell);

            _deleteProcessDictionary = new Dictionary<TradeType, Action<InvestInfo, TradeRecord>>();
            _deleteProcessDictionary.Add(TradeType.DEPOSIT, _deleteProcessor.DeleteDeposit);
            _deleteProcessDictionary.Add(TradeType.WITHDRAWAL, _deleteProcessor.DeleteWithdrawal);
            _deleteProcessDictionary.Add(TradeType.TRANSFER, _deleteProcessor.DeleteTransfer);
            _deleteProcessDictionary.Add(TradeType.BUY, _deleteProcessor.DeleteBuy);
            _deleteProcessDictionary.Add(TradeType.SELL, _deleteProcessor.DeleteSell);
        }

        public void Record(InvestInfo investInfo, IEnumerable<TradeRecord> records)
        {
            var recordsArray = records.ToArray();
            for (int i = 0; i < recordsArray.Length; i++)
            {
                Record(investInfo, recordsArray[i]);
            }
        }

        public void Record(InvestInfo investInfo, TradeRecord record)
        {
            investInfo.TradeRecords.Add(record);
            _recordProcessDictionary[record.TradeType].Invoke(investInfo, record);
        }

        public void Delete(InvestInfo investInfo, IEnumerable<TradeRecord> records)
        {
            var recordsArray = records.ToArray();
            for (int i = 0; i < recordsArray.Length; i++)
            {
                Delete(investInfo, recordsArray[i]);
            }
        }

        public void Delete(InvestInfo investInfo, TradeRecord record)
        {
            _deleteProcessDictionary[record.TradeType].Invoke(investInfo, record);
            investInfo.TradeRecords.Remove(record);
        }
    }
}
