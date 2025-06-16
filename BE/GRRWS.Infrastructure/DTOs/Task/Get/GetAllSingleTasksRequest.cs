namespace GRRWS.Infrastructure.DTOs.Task.Get
{
    public class GetAllSingleTasksRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? TaskType { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public string? Order { get; set; }
    }
}