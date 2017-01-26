using GildedMallKata.Stock;
using GildMallKata;

namespace GildedMallKata.AddStock
{
    public class AddDress : Command
    {
        public Dress StockItem { get; }

        public AddDress(Dress stockItem)
        {
            StockItem = stockItem;
        }
    }
}