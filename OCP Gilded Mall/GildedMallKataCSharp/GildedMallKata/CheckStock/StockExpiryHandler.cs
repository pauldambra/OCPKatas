using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GildedMallKata.Stock;
using GildMallKata;

namespace GildedMallKata.CheckStock
{
    public class StockExpiryHandler : IHandle<CheckStockAsAtDate>
    {
        private readonly GildedStockManager _shop;

        public StockExpiryHandler(GildedStockManager shop)
        {
            _shop = shop;
        }

        public Task Handle(CheckStockAsAtDate checkStock)
        {
//            return _shop.StockList
//                .OfType<TinnedFood>()
//                .Where(dsi => dsi.IsSaleable(checkStock.StockCheckDate));
            return Task.FromResult(0);
        }
    }
}