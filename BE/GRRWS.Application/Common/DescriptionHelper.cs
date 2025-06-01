namespace GRRWS.Application.Common
{
    public static class DescriptionHelper
    {
        public static string GenerateRequestDescription(string deviceName)
        {
            if (string.IsNullOrWhiteSpace(deviceName))
                throw new ArgumentException("DeviceName is required", nameof(deviceName));

            return $"Yêu cầu kiểm tra máy {deviceName}";
        }
    }
}
