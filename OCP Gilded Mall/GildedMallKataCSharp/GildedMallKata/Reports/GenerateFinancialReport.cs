using System;
using GildMallKata;

namespace GildedMallKata.Reports
{
    public class GenerateFinancialReport : Command
    {
        public DateTime ReportDate { get; }

        public GenerateFinancialReport(DateTime reportDate)
        {
            ReportDate = reportDate;
        }
    }
}