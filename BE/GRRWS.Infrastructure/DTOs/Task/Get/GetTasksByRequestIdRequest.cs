namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetTasksByRequestIdRequest
    {
        public Guid RequestId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}