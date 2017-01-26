using GildMallKata;

namespace GildedMallKata.AddStock
{
    public class AddStockHandler : IHandle<AddStock, GildedStockManager>
    {
        private readonly GildedStockManager _shop;

        public AddStockHandler(GildedStockManager shop)
        {
            _shop = shop;
        }

        public GildedStockManager Handle(AddStock addStock)
        {
            _shop.StockList.Add(addStock.StockItem);
            return _shop;
        }
    }
}