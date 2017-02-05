using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedMallKata.Stock
{
    public class TinnedFood : DatedStockItem
    {
        private static readonly TimeSpan OneYear = TimeSpan.FromDays(365.25);

        private static bool _lessThanAYearOld(DateTime stockCheckDate, DateTime stockDate) =>
            stockCheckDate - stockDate <= OneYear;

        public TinnedFood(DateTime dateAdded) : base(dateAdded)
        {
        }

        private bool IsSaleable(DateTime stockCheckDate)
            => _lessThanAYearOld(stockCheckDate, DateAdded);

        public static TinnedFood FromStockItem(DateTime dateAdded, StockItem stockItem)
            => new TinnedFood(dateAdded)
            {
                Name = stockItem.Name,
                Price = stockItem.Price
            };

        public static IEnumerable<TinnedFood> ExpireItems(IEnumerable<TinnedFood> tins, DateTime stockCheckDate)
            => tins.Where(t => t.IsSaleable(stockCheckDate));
    }
}