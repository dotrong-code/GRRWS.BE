namespace GRRWS.Infrastructure.Common.StringHelper
{
    public class TaskString
    {
        public static string GetInstallTaskName(string areaCode, string zoneCode, string positionCode)
        {

            return $"Thay thế tạm thời-{areaCode}-{zoneCode}-{positionCode}";
        }
        public static string GetReInstallTaskName(string areaCode, string zoneCode, string positionCode)
        {

            return $"Lắp đặt lại thiết bị-{areaCode}-{zoneCode}-{positionCode}";
        }
        public static string GetWarrantyTaskName(string areaCode, string zoneCode, string positionCode)
        {

            return $"Mang thiết bị đi bảo hành-{areaCode}-{zoneCode}-{positionCode}";
        }
        public static string GetTaskDescription(string areaName, string zoneName, string positionName)
        {
            return $"Thiết bị tại {areaName} - {zoneName} - {positionName} ";
        }
        public static string GetReInstallTaskDescription(string areaName, string zoneName, string positionName)
        {
            return $"Lắp đặt lại thiết bị tại {areaName} - {zoneName} - {positionName} sau khi đã bảo hành thành công ";
        }


    }
}
