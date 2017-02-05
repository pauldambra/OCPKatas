using System;

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

        public bool IsSaleable(DateTime stockCheckDate)
        {
            return _lessThanAYearOld(stockCheckDate, DateAdded);
        }

        public static TinnedFood FromStockItem(DateTime dateAdded, StockItem stockItem)
        {
            return new TinnedFood(dateAdded)
            {
                Name = stockItem.Name,
                Price = stockItem.Price
            };
        }
    }
}