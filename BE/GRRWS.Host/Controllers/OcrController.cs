using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.OCR;
using Microsoft.AspNetCore.Mvc;

namespace GRRWS.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcrController : ControllerBase
    {
        private readonly IOcrService _ocrService;
        private readonly ILogger<OcrController> _logger;

        public OcrController(IOcrService ocrService, ILogger<OcrController> logger)
        {
            _ocrService = ocrService;
            _logger = logger;
        }

        /// <summary>
        /// Extract text from image using Google Cloud Vision OCR
        /// </summary>
        /// <param name="request">OCR request containing base64 encoded image</param>
        /// <returns>Extracted text from the image</returns>
        [HttpPost]
        public async Task<IResult> ExtractText([FromBody] OcrRequest request)
        {
            try
            {
                _logger.LogInformation("OCR text extraction request received");

                if (request == null)
                {
                    return Results.BadRequest(new { error = "Request body is required" });
                }

                var result = await _ocrService.ExtractTextFromImageAsync(request);

                return result.IsSuccess
                    ? ResultExtensions.ToSuccessDetails(result, "Text extracted successfully")
                    : ResultExtensions.ToProblemDetails(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in OCR controller");
                return Results.Problem(
                    title: "Internal Server Error",
                    detail: "An unexpected error occurred while processing the OCR request",
                    statusCode: 500
                );
            }
        }
    }
}