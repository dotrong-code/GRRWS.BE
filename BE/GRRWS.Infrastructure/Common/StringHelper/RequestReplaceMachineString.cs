namespace GRRWS.Infrastructure.Common.StringHelper
{
    public class RequestReplaceMachineString
    {
        public static string ReturnDeviceToStockKeeper(string deviceName)
        {
            return $"Trả thiết bị về kho-{deviceName}";
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
