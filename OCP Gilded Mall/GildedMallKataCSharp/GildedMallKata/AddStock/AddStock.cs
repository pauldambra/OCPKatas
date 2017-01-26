using GildMallKata;

namespace GildedMallKata.AddStock
{
    public class AddStock : Command
    {
        public StockItem StockItem { get; }

        public AddStock(StockItem stockItem)
        {
            StockItem = stockItem;
        }
    }
}