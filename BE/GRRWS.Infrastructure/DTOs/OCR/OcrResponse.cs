namespace GRRWS.Infrastructure.DTOs.OCR
{
    public class OcrResponse
    {
        public string ExtractedText { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}