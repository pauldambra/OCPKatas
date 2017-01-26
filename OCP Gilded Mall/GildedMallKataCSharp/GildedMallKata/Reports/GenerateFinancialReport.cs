using System;

namespace GildedMallKata.Reports
{
    public class GenerateFinancialReport
    {
        public DateTime ReportDate { get; }

        public GenerateFinancialReport(DateTime reportDate)
        {
            ReportDate = reportDate;
        }
    }
}