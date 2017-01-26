using System;
using System.Linq;
using GildMallKata;

namespace GildedMallKata.Reports
{
    public class GenerateTinCanFinancialReportHandler : IHandle<GenerateFinancialReport, FinancialReport>
    {
        private readonly GildedTinCanStockManagerFactory _gildedTinCanStockManagerFactory;

        public GenerateTinCanFinancialReportHandler(GildedTinCanStockManagerFactory gildedTinCanStockManagerFactory)
        {
            _gildedTinCanStockManagerFactory = gildedTinCanStockManagerFactory;
        }

        public FinancialReport Handle(GenerateFinancialReport command)
        {
            var startOfPeriod = command.ReportDate.AddMonths(-1).AddTicks(-1);
            var beforeReportingPeriod = _gildedTinCanStockManagerFactory.GetStockList(startOfPeriod);
            var valueOfStockAtStartOfPeriod = beforeReportingPeriod.Sum(x => x.Price);

            var atEndOfReportingPeriod = _gildedTinCanStockManagerFactory.GetStockList(command.ReportDate);
            var valueOfStock = atEndOfReportingPeriod.Sum(x => x.Price);

            var costOfDepreciation = Math.Abs(Math.Min(valueOfStock - valueOfStockAtStartOfPeriod, 0));

            return new FinancialReport(valueOfStock, costOfDepreciation);
        }
    }

    public class GenerateDressFinancialReportHandler : IHandle<GenerateFinancialReport, FinancialReport>
    {
        private readonly GildedDressStockManagerFactory _gildedDressStockManagerFactory;

        public GenerateDressFinancialReportHandler(GildedDressStockManagerFactory gildedDressStockManagerFactory)
        {
            _gildedDressStockManagerFactory = gildedDressStockManagerFactory;
        }

        public FinancialReport Handle(GenerateFinancialReport command)
        {
            var startOfPeriod = command.ReportDate.AddMonths(-1).AddTicks(-1);
            var beforeReportingPeriod = _gildedDressStockManagerFactory.GetStockList(startOfPeriod);
            var valueOfStockAtStartOfPeriod = beforeReportingPeriod.Sum(x => x.Price);

            var atEndOfReportingPeriod = _gildedDressStockManagerFactory.GetStockList(command.ReportDate);
            var valueOfStock = atEndOfReportingPeriod.Sum(x => x.Price);

            var costOfDepreciation = Math.Abs(Math.Min(valueOfStock - valueOfStockAtStartOfPeriod, 0));

            return new FinancialReport(valueOfStock, costOfDepreciation);
        }
    }
}