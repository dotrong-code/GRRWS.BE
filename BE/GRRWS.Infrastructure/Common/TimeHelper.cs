namespace GRRWS.Infrastructure.Common
{
    public static class TimeHelper
    {
        public static DateTime GetHoChiMinhTime()
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(GetTimeZoneId());
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        }

        private static string GetTimeZoneId()
        {
            // TimeZone ID differs between Windows and Linux/macOS
            if (OperatingSystem.IsWindows())
                return "SE Asia Standard Time"; // Windows
            else
                return "Asia/Ho_Chi_Minh"; // Linux/macOS
        }
    }
}
