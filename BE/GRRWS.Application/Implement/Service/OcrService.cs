using Google.Cloud.Vision.V1;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.OCR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace GRRWS.Application.Implement.Service
{
    public class OcrService : IOcrService
    {
        private readonly ImageAnnotatorClient _visionClient;
        private readonly ILogger<OcrService> _logger;
        private readonly IConfiguration _configuration;

        public OcrService(ILogger<OcrService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _visionClient = CreateVisionClient();
        }

        public async Task<Result> ExtractTextFromImageAsync(OcrRequest request)
        {
            try
            {
                _logger.LogInformation("Starting OCR text extraction");

                // Validate input
                if (string.IsNullOrWhiteSpace(request.ImageBase64))
                {
                    return Result.Failure(Error.Validation("InvalidInput", "Base64 image string is required"));
                }

                // Decode base64 image
                byte[] imageBytes;
                try
                {
                    imageBytes = DecodeBase64Image(request.ImageBase64);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to decode base64 image");
                    return Result.Failure(Error.Validation("InvalidBase64", "Invalid base64 image format"));
                }

                // Validate image size (optional - limit to 10MB)
                if (imageBytes.Length > 10 * 1024 * 1024)
                {
                    return Result.Failure(Error.Validation("ImageTooLarge", "Image size exceeds 10MB limit"));
                }

                // Create Google Vision API image object
                var image = Google.Cloud.Vision.V1.Image.FromBytes(imageBytes);

                // Perform OCR using Google Cloud Vision API
                var response = await _visionClient.DetectTextAsync(image);

                if (response == null || !response.Any())
                {
                    _logger.LogInformation("No text detected in the image");
                    return Result.SuccessWithObject(new OcrResponse
                    {
                        ExtractedText = string.Empty,
                        Success = true,
                        ErrorMessage = ""
                    });
                }

                // Extract and clean text
                var extractedText = ExtractAndCleanText(response);

                _logger.LogInformation("OCR text extraction completed successfully. Text length: {Length}", extractedText.Length);

                var result = new OcrResponse
                {
                    ExtractedText = extractedText,
                    Success = true,
                    ErrorMessage = ""
                };

                return Result.SuccessWithObject(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during OCR text extraction");
                return Result.Failure(Error.Failure("OcrFailed", $"Failed to extract text from image: {ex.Message}"));
            }
        }

        private ImageAnnotatorClient CreateVisionClient()
        {
            try
            {
                // Get the service account key file path from configuration
                var serviceAccountKeyPath = _configuration["GoogleCloudVision:ServiceAccountKeyPath"];
                
                if (string.IsNullOrWhiteSpace(serviceAccountKeyPath))
                {
                    throw new InvalidOperationException("Google Cloud Vision service account key path not configured");
                }

                if (!File.Exists(serviceAccountKeyPath))
                {
                    throw new FileNotFoundException($"Google Cloud Vision service account key file not found: {serviceAccountKeyPath}");
                }

                // Set the environment variable for Google Cloud authentication
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", serviceAccountKeyPath);

                // Create the Vision API client
                var client = ImageAnnotatorClient.Create();
                
                _logger.LogInformation("Google Cloud Vision client created successfully");
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create Google Cloud Vision client");
                throw;
            }
        }

        private byte[] DecodeBase64Image(string base64String)
        {
            // Remove data URL prefix if present (e.g., "data:image/jpeg;base64,")
            var base64Data = base64String;
            if (base64String.Contains(","))
            {
                base64Data = base64String.Split(',')[1];
            }

            // Remove any whitespace
            base64Data = Regex.Replace(base64Data, @"\s+", "");

            return Convert.FromBase64String(base64Data);
        }

        private string ExtractAndCleanText(IReadOnlyList<EntityAnnotation> annotations)
        {
            if (!annotations.Any())
                return string.Empty;

            // The first annotation contains the full detected text
            var fullText = annotations.FirstOrDefault()?.Description ?? string.Empty;

            // Clean up the text
            var cleanedText = CleanExtractedText(fullText);

            return cleanedText;
        }

        private string CleanExtractedText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            // Basic text cleaning
            text = text.Trim();
            
            // Remove excessive whitespace
            text = Regex.Replace(text, @"\s+", " ");
            
            // Remove null characters
            text = text.Replace("\0", "");

            return text;
        }

        protected virtual void Dispose(bool disposing)
        {
            // No resources to dispose explicitly for _visionClient
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}