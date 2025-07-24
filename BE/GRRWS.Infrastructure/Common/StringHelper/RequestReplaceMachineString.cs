namespace GRRWS.Infrastructure.Common.StringHelper
{
    public class RequestReplaceMachineString
    {
        public static string ReturnDeviceToStockKeeper(string deviceName)
        {
            return $"Trả thiết bị về kho-{deviceName}";
        }

        public static string RequesReplaceMachine(string areaName, string zoneName, int positionIndex)
        {
            return $"Yêu cầu thay thế thiết bị-{areaName}/{zoneName}/{positionIndex}";
        }
        public static string RequesReplaceMachineNote(string oldDeviceName, string newDeviceName)
        {
            return $"Thay thế tạm thời máy {oldDeviceName} bằng {newDeviceName}";
        }
        public static string NoteWarrantyReturnFailed()
        {
            return "Thiết bị bảo hành không được, cần đem về kho";
        }
        public static string NoteWarrantyReturnSuccess()
        {
            return "Thiết bị bảo hành thành công";
        }
        public static string WarrantyReturnReceive(string deviceName)
        {
            return $"Nhận thiết bị đã bảo hành-{deviceName}";
        }
        public static string StocOutRequest(string deviceName)
        {
            return $"Yêu cầu xuất kho thiết bị-{deviceName}";
        }
        public static string StockOutConfirmation(string areaName, string zoneName, int positionIndex)
        {
            return $"STOCKOUT-{areaName}/{zoneName}/{positionIndex}-{TimeHelper.GetHoChiMinhTime():yyyyMMddHHmmss}";
        }

        public static string StockInConfirmation(string deviceName)
        {
            return $"STOCKIN-{deviceName}-{TimeHelper.GetHoChiMinhTime():yyyyMMddHHmmss}";
        }

        public static string InstallationConfirmation(string deviceName)
        {
            return $"INSTALL-{deviceName}-{TimeHelper.GetHoChiMinhTime():yyyyMMddHHmmss}";
        }

        public static string WarrantySubmissionConfirmation(string deviceName)
        {
            return $"Lấy thiết bị đi bảo hành-{deviceName}-{TimeHelper.GetHoChiMinhTime():yyyyMMddHHmmss}";
        }

        public static string StockOutNote(string newDeviceName, string oldDeviceName)
        {
            return $"Retrieve replacement device {newDeviceName} for {oldDeviceName}";
        }

        public static string StockInNote(string deviceName)
        {
            return $"Return faulty device {deviceName} to stock";
        }

        public static string InstallationNote(string deviceName)
        {
            return $"Confirm installation of {deviceName}";
        }

        public static string WarrantySubmissionNote(string deviceName)
        {
            return $"Submit {deviceName} for warranty repair";
        }

        


    }
}
