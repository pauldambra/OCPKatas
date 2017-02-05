using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using GildedMallKata.AddStock;
using GildedMallKata.Stock;
using GildMallKata;
using Newtonsoft.Json;

namespace GildedMallKata.CheckStock
{
    public class GildedDressStockCheckHandler : IHandle<GenerateStockList>
    {
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly string _streamName;

        public GildedDressStockCheckHandler(IEventStoreConnection eventStoreConnection, string streamName)
        {
            _eventStoreConnection = eventStoreConnection;
            _streamName = streamName;
        }

        public async Task Handle(GenerateStockList generateStockList)
        {
            var streamEvents = await _eventStoreConnection.ReadEntireStream(_streamName);

            var dresses = streamEvents.FindAddedStock(Dress.FromStockItem);

            var depreciatedStock = dresses.Select(dsi => dsi.AsAtDate(generateStockList.StockCheckDate));

            await _eventStoreConnection.WriteStockListGenerated(_streamName, depreciatedStock, generateStockList.CorrelationId);
        }
    }
}