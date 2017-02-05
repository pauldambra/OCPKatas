using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using GildedMallKata.AddStock;
using GildedMallKata.Stock;
using Newtonsoft.Json;

namespace GildedMallKata.CheckStock
{
    public static class StockRepository
    {
        public static async Task<List<ResolvedEvent>> ReadEntireStream(this IEventStoreConnection connection, string streamName)
        {
            var streamEvents = new List<ResolvedEvent>();

            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;
            do
            {
                currentSlice = await connection.ReadStreamEventsForwardAsync(
                    streamName,
                    nextSliceStart,
                    200,
                    false);

                nextSliceStart = currentSlice.NextEventNumber;

                streamEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);
            return streamEvents;
        }

        public static async Task WriteStockListGenerated(
            this IEventStoreConnection connection,
            string streamName,
            IEnumerable<DatedStockItem> stockItems,
            Guid correlationId)
        {
            try
            {
                var stockListGenerated = Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(
                        new StockListGenerated(correlationId, stockItems)
                    )
                );
                var eventData = new List<EventData>
                {
                    new EventData(Guid.NewGuid(), typeof(StockListGenerated).Name, true, stockListGenerated, null)
                };

                await connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static IEnumerable<T> FindAddedStock<T>(
            this IEnumerable<ResolvedEvent> streamEvents,
            Func<DateTime, StockItem, T> mapFunc)
        {
            return streamEvents.Where(se => se.Event.EventType == typeof(StockAdded).Name)
                .Select(se =>
                    JsonConvert.DeserializeObject<StockAdded>(Encoding.UTF8.GetString(se.Event.Data))
                )
                .Select(sa => mapFunc(sa.DateAdded, sa.StockItem))
                .Where(result => result != null);
        }
    }
}