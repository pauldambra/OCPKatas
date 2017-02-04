using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using GildedMallKata.Stock;
using GildMallKata;
using Newtonsoft.Json;

namespace GildedMallKata.AddStock
{
    public class AddStockHandler : IHandle<AddStock>
    {
        private readonly IEventStoreConnection _eventstoreConnection;
        private readonly string _streamName;

        public AddStockHandler(IEventStoreConnection eventstoreConnection, string streamName)
        {
            _eventstoreConnection = eventstoreConnection;
            _streamName = streamName;
        }

        public async Task Handle(AddStock addStock)
        {
            var additionDate = ((DatedStockItem) addStock.StockItem)?.DateAdded ?? DateTime.UtcNow;
            var jsonData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new StockAdded(additionDate, addStock.StockItem)));
            var metaData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
                StockType = addStock.StockItem?.GetType().Name ?? "N/A"
            }));
            await _eventstoreConnection.AppendToStreamAsync(_streamName, ExpectedVersion.Any, new List<EventData>
            {
                new EventData(Guid.NewGuid(), typeof(StockAdded).Name, true, jsonData, metaData)
            });
        }
    }
}