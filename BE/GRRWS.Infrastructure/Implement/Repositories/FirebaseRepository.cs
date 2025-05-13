using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Firebase.AddImage;
using GRRWS.Infrastructure.DTOs.Firebase.GetImage;
using GRRWS.Infrastructure.Interfaces.IRepositories;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class FirebaseRepository : IFirebaseRepository
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName = "koiveterinaryservicecent-925db.appspot.com";

        public FirebaseRepository(StorageClient storageClient)
        {
            _storageClient = storageClient;
        }
        public async Task<AddImageResponse> UploadImageAsync(AddImageRequest request)
        {
            // Check for null request
            if (request == null)
            {
                return new AddImageResponse(string.Empty, false, Error.Validation("RequestNull", "The provided request is null."));
            }

            // Validate file
            if (request.ImageFile == null || request.ImageFile.Length == 0)
            {
                return new AddImageResponse(string.Empty, false, Error.Validation("FileEmpty", "The provided file is empty."));
            }

            //Ensure storage client and bucket name are initialized
            if (_storageClient == null)
            {
                return new AddImageResponse(string.Empty, false, Error.Failure("StorageClientNull", "The storage client is not initialized."));
            }

            if (string.IsNullOrEmpty(_bucketName))
            {
                return new AddImageResponse(string.Empty, false, Error.Failure("BucketNameNull", "The bucket name is not initialized."));
            }

            // Generate file name and path
            var fileName = $"{Guid.NewGuid()}_{request.ImageFile.FileName}";
            var filePath = $"{request.Folder}/{fileName}";

            try
            {
                // Upload to Firebase
                await using (var stream = request.ImageFile.OpenReadStream())
                {
                    await _storageClient.UploadObjectAsync(_bucketName, filePath, "image/png", stream);
                }

                // Return success with file path
                return new AddImageResponse(filePath, true, Error.None);
            }
            catch (Exception ex)
            {
                // Return failure with error message
                return new AddImageResponse(string.Empty, false, Error.Failure("UploadFailed", $"Failed to upload image: {ex.Message}"));
            }
        }

        public async Task<GetImageResponse> GetImageAsync(GetImageRequest request)
        {
            var response = new GetImageResponse();

            try
            {
                var memoryStream = new MemoryStream();
                var obj = await _storageClient.GetObjectAsync(_bucketName, request.FilePath);

                // Download the image to the MemoryStream
                var imageUrl = GenerateImageUrl(obj); // Implement this method according to your requirements

                response.ImageUrl = imageUrl; // Assign the generated URL
                response.ContentType = obj.ContentType;  // Assuming _storageClient provides this information
                response.Success = true;
                response.Error = Error.None;  // No error occurred
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = Error.Failure("GET_IMAGE_FAILED", $"Failed to get image: {ex.Message}");
            }

            return response;
        }
        private string GenerateImageUrl(Google.Apis.Storage.v1.Data.Object obj)
        {
            // Construct the Firebase Storage URL based on the object's name
            return $"https://firebasestorage.googleapis.com/v0/b/{_bucketName}/o/{Uri.EscapeDataString(obj.Name)}?alt=media&token={obj.Generation}";

        }

    }
}
