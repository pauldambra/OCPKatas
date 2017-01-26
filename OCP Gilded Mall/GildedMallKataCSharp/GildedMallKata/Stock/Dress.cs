using System;

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
        {
            return new Dress(DateAdded)
            {
                Name = Name,
                Price = _addedTenWeeksAgo(stockCheckDate, DateAdded)
                    ? Price * 0.75M
                    : Price
            };
        }
    }
}