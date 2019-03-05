using C.I.M.S_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.Utils
{
    class TradeRecordProcessor
    {
        public void RecordDeposit(InvestInfo investInfo, TradeRecord record)
        {
            var targetItem = investInfo.HoldingAssets.Where(x => x.AssetName.Equals(record.ItemName));

            if (targetItem.Count() == 0)
            {
                investInfo.HoldingAssets.Add(new HoldingAsset(record.ItemName, record.Amount, record.UnitPrice));
            }
            else
            {
                targetItem.First().Amount += record.Amount;
            }

            Messenger.Instance.Send("입금 완료", Context.WRITE_LOG);
        }



        public void RecordWithdrawal(InvestInfo investInfo, TradeRecord record)
        {
            var capital = investInfo.HoldingAssets.Where(x => x.AssetName.Equals("WON"));

            if (capital.Count() == 0)
            {
                Messenger.Instance.Send("원화를 가지고 있지 않습니다.", Context.WRITE_LOG);
                return;
            }

            double withdrawalCost = record.Amount * (1 + record.RelativeFee * 0.01) + record.AbsoluteFee;

            if (capital.First().Amount < withdrawalCost)
            {
                Messenger.Instance.Send("원화가 부족합니다.", Context.WRITE_LOG);
            }
            else
            {
                capital.First().Amount -= withdrawalCost;
                Messenger.Instance.Send("출금 완료", Context.WRITE_LOG);
            }
        }



        public void RecordTransfer(InvestInfo investInfo, TradeRecord record)
        {
            var targetItem = investInfo.HoldingAssets.Where(x => x.AssetName.Equals(record.ItemName));

            if (targetItem.Count() == 0)
            {
                Messenger.Instance.Send("보유 코인이 없습니다.", Context.WRITE_LOG);
                return;
            }

            if (targetItem.First().AssetName.Equals("WON"))
            {
                Messenger.Instance.Send("원화는 송금할 수 없습니다.", Context.WRITE_LOG);
                return;
            }

            if (targetItem.First().Amount < record.AbsoluteFee + (record.Amount * (record.RelativeFee * 0.01)))
            {
                Messenger.Instance.Send("코인이 부족합니다.", Context.WRITE_LOG);
                return;
            }

            targetItem.First().Amount -= record.AbsoluteFee + (record.Amount * (record.RelativeFee * 0.01));
            Messenger.Instance.Send("송금 완료", Context.WRITE_LOG);
        }



        public void RecordBuy(InvestInfo investInfo, TradeRecord record)
        {
            var capital = investInfo.HoldingAssets.Where(x => x.AssetName.Equals("WON"));
            var targetItem = investInfo.HoldingAssets.Where(x => x.AssetName.Equals(record.ItemName));

            if (capital.Count() == 0)
            {
                Messenger.Instance.Send("원화를 가지고 있지 않습니다.", Context.WRITE_LOG);
                return;
            }

            if (record.ItemName.Equals("WON"))
            {
                Messenger.Instance.Send("원화는 매수할 수 없습니다.", Context.WRITE_LOG);
                return;
            }

            double buyCost = (record.Amount * record.UnitPrice) * (1 + record.RelativeFee * 0.01);
            double buyAmount = record.Amount - record.AbsoluteFee;
            if (capital.First().Amount < buyCost)
            {
                Messenger.Instance.Send("원화가 부족합니다.", Context.WRITE_LOG);
                return;
            }

            if (targetItem.Count() == 0)
            {
                investInfo.HoldingAssets.Add(new HoldingAsset(record.ItemName, buyAmount, record.UnitPrice));
            }
            else
            {
                var newAmount = targetItem.First().Amount + buyAmount;
                var newBuyPrice = targetItem.First().BuyPrice + (buyAmount * record.UnitPrice);

                targetItem.First().Amount = newAmount;
                targetItem.First().BuyUnitPrice = newAmount != 0 ? newBuyPrice / newAmount : 0;
            }
            capital.First().Amount -= buyCost;
            Messenger.Instance.Send("매수 완료", Context.WRITE_LOG);
        }



        public void RecordSell(InvestInfo investInfo, TradeRecord record)
        {
            var capital = investInfo.HoldingAssets.Where(x => x.AssetName.Equals("WON"));
            var targetItem = investInfo.HoldingAssets.Where(x => x.AssetName.Equals(record.ItemName));

            if (targetItem.Count() == 0)
            {
                Messenger.Instance.Send("보유 코인이 없습니다.", Context.WRITE_LOG);
                return;
            }

            if (targetItem.First().AssetName.Equals("WON"))
            {
                Messenger.Instance.Send("원화는 매도할 수 없습니다.", Context.WRITE_LOG);
                return;
            }

            if (targetItem.First().Amount < record.Amount)
            {
                Messenger.Instance.Send("코인이 부족합니다.", Context.WRITE_LOG);
                return;
            }

            targetItem.First().Amount -= record.Amount;
            if (targetItem.First().Amount == 0 || targetItem.First().Amount < 0.00000001f)
            {
                investInfo.HoldingAssets.Remove(targetItem.First());
            }

            double sellCost = (record.Amount * record.UnitPrice) * (1 - record.RelativeFee * 0.01) - record.AbsoluteFee;
            if (capital.Count() == 0)
            {
                investInfo.HoldingAssets.Add(new HoldingAsset("WON", sellCost, 0));
            }
            else
            {
                capital.First().Amount += sellCost;
            }

            Messenger.Instance.Send("매도 완료", Context.WRITE_LOG);
        }
    }
}
