using System;
using System.Linq;
using System.Threading.Tasks;
using GildMallKata;

namespace GildedMallKata.Reports
{
    public class GenerateDressFinancialReportHandler : IHandle<GenerateFinancialReport>
    {
        private readonly GildedDressStockManagerFactory _gildedDressStockManagerFactory;

        public GenerateDressFinancialReportHandler(GildedDressStockManagerFactory gildedDressStockManagerFactory)
        {
            _gildedDressStockManagerFactory = gildedDressStockManagerFactory;
        }

        public async Task Handle(GenerateFinancialReport command)
        {
//            var startOfPeriod =     command.ReportDate.AddMonths(-1).AddTicks(-1);
//            var beforeReportingPeriod = await _gildedDressStockManagerFactory.GetStockList(startOfPeriod);
//            var valueOfStockAtStartOfPeriod = beforeReportingPeriod.Sum(x => x.Price);
//
//            var atEndOfReportingPeriod = await _gildedDressStockManagerFactory.GetStockList(command.ReportDate);
//            var valueOfStock = atEndOfReportingPeriod.Sum(x => x.Price);
//
//            var costOfDepreciation = Math.Abs(Math.Min(valueOfStock - valueOfStockAtStartOfPeriod, 0));

//            return new FinancialReport(valueOfStock, costOfDepreciation);
        }
    }
}