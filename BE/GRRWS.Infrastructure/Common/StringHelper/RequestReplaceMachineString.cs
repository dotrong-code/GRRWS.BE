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
            return "Thiết bị bảo hành thành công, đem trả thiết bị thay thế";
        }
    }
}
