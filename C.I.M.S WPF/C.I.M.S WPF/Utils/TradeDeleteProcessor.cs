using C.I.M.S_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.Utils
{
    class TradeDeleteProcessor
    {
        public void DeleteDeposit(InvestInfo investInfo, TradeRecord record)
        {
            var targetItem = investInfo.HoldingAssets.Where(x => x.AssetName.Equals(record.ItemName));
            targetItem.First().Amount -= record.Amount;

            if (targetItem.First().Amount == 0)
            {
                investInfo.HoldingAssets.Remove(targetItem.First());
            }
        }

        public void DeleteWithdrawal(InvestInfo investInfo, TradeRecord record)
        {
            var capital = investInfo.HoldingAssets.Where(x => x.AssetName.Equals("WON"));
            double withdrawalCost = record.Amount * (1 + record.RelativeFee * 0.01) + record.AbsoluteFee;
            capital.First().Amount += withdrawalCost;
        }

        public void DeleteTransfer(InvestInfo investInfo, TradeRecord record)
        {
            var targetItem = investInfo.HoldingAssets.Where(x => x.AssetName.Equals(record.ItemName));
            targetItem.First().Amount += record.AbsoluteFee + (record.Amount * (record.RelativeFee * 0.01));
        }

        public void DeleteBuy(InvestInfo investInfo, TradeRecord record)
        {
            var targetItem = investInfo.HoldingAssets.Where(x => x.AssetName.Equals(record.ItemName));
            var capital = investInfo.HoldingAssets.Where(x => x.AssetName.Equals("WON"));
            var newAmount = targetItem.First().Amount - (record.Amount - record.AbsoluteFee);
            var newBuyPrice = targetItem.First().BuyPrice - ((record.Amount - record.AbsoluteFee) * record.UnitPrice);

            if (newAmount == 0)
            {
                investInfo.HoldingAssets.Remove(targetItem.First());
            }
            else
            {
                targetItem.First().Amount = newAmount;
                targetItem.First().BuyUnitPrice = newBuyPrice / newAmount;
            }

            double buyCost = (record.Amount * record.UnitPrice) * (1 + record.RelativeFee * 0.01);
            capital.First().Amount += buyCost;
        }

        public void DeleteSell(InvestInfo investInfo, TradeRecord record)
        {
            var targetItem = investInfo.HoldingAssets.Where(x => x.AssetName.Equals(record.ItemName));
            var capital = investInfo.HoldingAssets.Where(x => x.AssetName.Equals("WON"));

            if (targetItem.Count() == 0)
            {
                investInfo.HoldingAssets.Add(new HoldingAsset(record.ItemName, record.Amount, record.UnitPrice));
            }
            else
            {
                targetItem.First().Amount += record.Amount;
            }

            double sellCost = (record.Amount * record.UnitPrice) * (1 - record.RelativeFee * 0.01) - record.AbsoluteFee;
            capital.First().Amount -= sellCost;
        }
    }
}
