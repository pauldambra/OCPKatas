namespace GildedMallKata.Reports
{
    public class FinancialReport
    {
        public decimal ValueOfStock { get; }
        public decimal Depreciation { get; }

        public FinancialReport(decimal valueOfStock, decimal depreciation)
        {
            ValueOfStock = valueOfStock;
            Depreciation = depreciation;
        }
    }
}