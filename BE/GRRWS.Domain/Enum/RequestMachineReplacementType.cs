namespace GRRWS.Domain.Enum
{
    public enum RequestMachineReplacementType
    {
        Replacement, // Yêu cầu thay thế máy (lấy máy mới)
        StockReturn,  // Yêu cầu trả máy hư về kho
        StockIn,
        StockOut,

    }
}
