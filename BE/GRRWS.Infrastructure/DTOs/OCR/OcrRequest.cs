using System.ComponentModel.DataAnnotations;

namespace GRRWS.Infrastructure.DTOs.OCR
{
    public class OcrRequest
    {
        [Required]
        public string ImageBase64 { get; set; }
    }
}