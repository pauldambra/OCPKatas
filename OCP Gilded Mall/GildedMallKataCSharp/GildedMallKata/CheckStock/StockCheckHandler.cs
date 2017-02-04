﻿using System;
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
    public class StockCheckHandler : IHandle<GenerateStockList>
    {
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly string _streamName;

        public StockCheckHandler(IEventStoreConnection eventStoreConnection, string streamName)
        {
            _eventStoreConnection = eventStoreConnection;
            _streamName = streamName;
        }

        public async Task Handle(GenerateStockList generateStockList)
        {
            var streamEvents = await ReadEntireStream();

            List<Dress> dresses;
            try
            {
                dresses = FindAddedDresses(streamEvents).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            var depreciatedStock = dresses
                .Where(dsi => dsi != null)
                .Select(dsi => dsi.AsAtDate(generateStockList.StockCheckDate));

            await WriteStockListGenerated(generateStockList, depreciatedStock);
        }

        private async Task<List<ResolvedEvent>> ReadEntireStream()
        {
            var streamEvents = new List<ResolvedEvent>();

            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;
            do
            {
                currentSlice = await _eventStoreConnection.ReadStreamEventsForwardAsync(
                    _streamName,
                    nextSliceStart,
                    200,
                    false);

                nextSliceStart = currentSlice.NextEventNumber;

                streamEvents.AddRange(currentSlice.Events);
            } while (!currentSlice.IsEndOfStream);
            return streamEvents;
        }

        private static IEnumerable<Dress> FindAddedDresses(IEnumerable<ResolvedEvent> streamEvents)
        {
            return streamEvents.Where(se => se.Event.EventType == typeof(StockAdded).Name)
                .Select(se =>
                    JsonConvert.DeserializeObject<StockAdded>(Encoding.UTF8.GetString(se.Event.Data))
                )
                .Select(sa => Dress.FromStockItem(sa.DateAdded, sa.StockItem));
        }

        private async Task WriteStockListGenerated(GenerateStockList generateStockList, IEnumerable<Dress> depreciatedStock)
        {
            try
            {
                var stockListGenerated = Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(
                        new StockListGenerated(generateStockList.CorrelationId, depreciatedStock)
                    )
                );
                var eventData = new List<EventData>
                {
                    new EventData(Guid.NewGuid(), typeof(StockListGenerated).Name, true, stockListGenerated, null)
                };

                await _eventStoreConnection.AppendToStreamAsync(_streamName, ExpectedVersion.Any, eventData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}