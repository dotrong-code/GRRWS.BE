namespace GRRWS.Domain.Enum
{
    public enum TaskType
    {
        Warranty,// Dùng cho task group
        Repair,// Dùng cho task group
        Replacement,// Dùng cho task group
        WarrantySubmission, //Dùng cho task bên trong Warranty task group
        WarrantyReturn, // Dùng cho task bên trong Warranty task group
        Uninstallation,
        Installation,// Dùng cho task bên trong task Group
        StorageReturn,
    }
}
