using System;
using GildMallKata;

namespace GildedMallKata.AddStock
{
    public class AddDatedStockHandler : IHandle<AddStock, GildedStockManager>
    {
        private readonly GildedStockManager _shop;
        private readonly DateTime _stockAdded;

        public AddDatedStockHandler(GildedStockManager shop, DateTime stockAdded)
        {
            _shop = shop;
            _stockAdded = stockAdded;
        }

        public GildedStockManager Handle(AddStock addStock)
        {
            _shop.StockList.Add(DatedStockItem.For(addStock.StockItem, _stockAdded));
            return _shop;
        }
    }
}