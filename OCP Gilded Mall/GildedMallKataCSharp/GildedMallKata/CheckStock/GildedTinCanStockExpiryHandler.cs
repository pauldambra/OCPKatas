using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using GildedMallKata.Stock;
using GildMallKata;

namespace GildedMallKata.CheckStock
{
    public class GildedTinCanStockExpiryHandler : IHandle<GenerateStockList>
    {
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly string _streamName;

        public GildedTinCanStockExpiryHandler(IEventStoreConnection eventStoreConnection, string streamName)
        {
            _eventStoreConnection = eventStoreConnection;
            _streamName = streamName;
        }

        public async Task Handle(GenerateStockList generateStockList)
        {
            var streamEvents = await _eventStoreConnection.ReadEntireStream(_streamName);

            var tins = streamEvents.FindAddedStock(TinnedFood.FromStockItem);

            var nonExpiredStock = TinnedFood.ExpireItems(tins, generateStockList.StockCheckDate);

            await _eventStoreConnection.WriteStockListGenerated(_streamName, nonExpiredStock, generateStockList.CorrelationId);
        }
    }
}