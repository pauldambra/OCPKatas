using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildMallKata
{

    public class GildedStockManager
    {
        public List<StockItem> StockList { get; } = new List<StockItem>();
    }

    public class DatedStockItem : StockItem
    {
        public DateTime DateAdded { get; }

        public DatedStockItem(DateTime dateAdded)
        {
            DateAdded = dateAdded;
        }

        public static DatedStockItem For(StockItem stockItem, DateTime stockAdded)
        {
            return new DatedStockItem(stockAdded)
            {
                Name =  stockItem.Name,
                Price = stockItem.Price
            };
        }
    }

    public class StockItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

}