namespace GRRWS.Domain.Enum
{
    public enum Status
    {
        Pending = 0, // Request is pending approval or action
        Approved = 1, // Request has been approved
        Rejected = 2, // Request has been rejected
        InProgress = 3, // Work is currently being done on the request
        Completed = 4, // Work on the request has been completed
        Cancelled = 5, // Request has been cancelled
        OnHold = 6 // Work on the request is temporarily paused
    }
}
//[EnumDataType(typeof(Status))]