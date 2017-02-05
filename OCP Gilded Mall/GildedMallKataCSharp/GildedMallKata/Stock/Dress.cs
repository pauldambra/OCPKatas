using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedMallKata.Stock
{
    public class Dress : DatedStockItem
    {
        private static readonly TimeSpan TenWeeks = TimeSpan.FromDays(70);

        private static bool _addedTenWeeksAgo(DateTime stockCheckDate, DateTime stockDate) =>
            stockCheckDate - stockDate >= TenWeeks;

        public Dress(DateTime dateAdded) : base(dateAdded)
        {
        }

        public Dress AsAtDate(DateTime stockCheckDate)
            => new Dress(DateAdded)
            {
                Name = Name,
                Price = _addedTenWeeksAgo(stockCheckDate, DateAdded)
                    ? Price * 0.75M
                    : Price
            };

        public static Dress FromStockItem(
            DateTime dateAdded,
            StockItem stockItem)
                => new Dress(dateAdded)
                {
                    Name = stockItem.Name,
                    Price = stockItem.Price
                };

        public static IEnumerable<Dress> DepreciateItems(
            IEnumerable<Dress> dresses,
            DateTime stockCheckDate)
                 => dresses.Select(dsi => dsi.AsAtDate(stockCheckDate));

    }
}