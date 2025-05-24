namespace GRRWS.Application.Common
{
    public static class TitleHelper
    {
        public static string GenerateRequestTitle(string areaCode, string zoneCode, int positionIndex, string devideCode)
        {
            if (string.IsNullOrWhiteSpace(areaCode))
                throw new ArgumentException("AreaCode is required", nameof(areaCode));
            if (string.IsNullOrWhiteSpace(zoneCode))
                throw new ArgumentException("ZoneCode is required", nameof(zoneCode));
            if (positionIndex < 0)
                throw new ArgumentException("Position index must be non-negative", nameof(positionIndex));
            string positionCode = $"P{positionIndex:D2}";
            return $"{areaCode}-{zoneCode}-{positionCode}-{devideCode}";
        }
    }
}