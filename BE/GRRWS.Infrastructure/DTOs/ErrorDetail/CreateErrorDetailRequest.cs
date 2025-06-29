namespace GRRWS.Infrastructure.DTOs.ErrorDetail
{
    public class CreateErrorDetailRequest
    {
        public List<Guid>? ErrorId { get; set; }
        public Guid RequestId { get; set; }
    }
}