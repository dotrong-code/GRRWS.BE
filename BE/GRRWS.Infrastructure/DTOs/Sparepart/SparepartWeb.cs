namespace GRRWS.Infrastructure.DTOs.Sparepart
{
    public class SparepartWeb
    {
        public Guid SpartpartId { get; set; }
        public string SpartpartName { get; set; }
        public string ErrorName { get; set; }
        public string ErrorCode { get; set; }
        public int QuantityNeed { get; set; }
        public int StockQuatity { get; set; }
        public string Unit { get; set; }
    }
}
