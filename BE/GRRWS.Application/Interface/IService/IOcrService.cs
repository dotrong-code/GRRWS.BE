using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.OCR;

namespace GRRWS.Application.Interface.IService
{
    public interface IOcrService
    {
        Task<Result> ExtractTextFromImageAsync(OcrRequest request);
    }
}