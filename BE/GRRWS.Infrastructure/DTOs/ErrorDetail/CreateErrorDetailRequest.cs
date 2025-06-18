namespace GRRWS.Infrastructure.DTOs.ErrorDetail
{
    public class CreateErrorDetailRequest
    {
        public Guid ErrorId { get; set; }
        public Guid RequestId { get; set; }
    }
}