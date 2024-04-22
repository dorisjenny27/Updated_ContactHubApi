using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ContactHub.Model.DTOs;
using ContactHub.Model.Entity;
using ContactHub.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ContactHub.Services
{
    public class PhotoManager : IPhotoManager
    {
        private readonly Cloudinary _cloudinary;
        private readonly UserManager<User> _userManager;
        //private readonly IPhotoManager _photoManager;
        private readonly IMapper _mapper;

        public PhotoManager(IConfiguration config, UserManager<User> userManager, IMapper mapper)
        {
          var cloudName = config.GetSection("CloudinarySettings:CloudName").Value;
            var apiKey = config.GetSection("CloudinarySettings:ApiKey").Value;
            var apiSecret = config.GetSection("CloudinarySettings:ApiSecret").Value;

            Account account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);

            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<bool> DeleteImage(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);
            if (result.Result.ToLower().Equals("ok"))
                return true;
            return false;
        }

        public async Task<PhotoResult> UploadImage(IFormFile file)
        {
            var allowedTypes = new List<string> { "image/png", "image/jpg", "image/jpeg" };
            //Validate the image size
            if(file.Length > 0 && file.Length <= (2 * 1024 * 1024))
            {
                //Validate the type
                if(allowedTypes.Any(x => x.ToLower().Equals(file.ContentType.ToLower())))
                {
                    var uploadResult = new ImageUploadResult();
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(file.FileName, stream),
                            Transformation = new Transformation().Width("200").Height("200").Crop("fill").Gravity("face")
                        };
                        uploadResult = _cloudinary.Upload(uploadParams);
                    }
                    if (uploadResult.Url != null)
                        return new PhotoResult
                        {
                            IsSuccess = true,
                            Message = "Uploaded successfully",
                            PublicId = uploadResult.PublicId,
                            PhotoUrl = uploadResult.Url.ToString()
                        };
                    return new PhotoResult
                    {
                        IsSuccess = false,
                        Message = "Upload failed"
                    };
                }
                else
                {
                    return new PhotoResult
                    {
                        IsSuccess = false,
                        Message = "Invalid file cannot type",
                    };
                }
            }
            else
            {
                return new PhotoResult
                {
                    IsSuccess = false,
                    Message = "Invalid size",
                };
            }
        }
        public async Task<UserToReturnDTO> AddPhotoAsync(string id, IFormFile photo)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new ArgumentException($"No record found for user with Id: {id}");

            var uploadResult = await UploadImage(photo);
            if (!uploadResult.IsSuccess)
                throw new InvalidOperationException($"Failed to upload photo: {uploadResult.Message}");

            user.PhotoUrl = uploadResult.PhotoUrl;
            user.PhotoId = uploadResult.PublicId;
            await _userManager.UpdateAsync(user);

            return _mapper.Map<UserToReturnDTO>(user);
        }

        public async Task<UserToReturnDTO> DeletePhotoAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new ArgumentException($"No record found for user with Id: {id}");

            var deleteResult = await DeleteImage(user.PhotoId);
            if (!deleteResult)
                throw new InvalidOperationException("Failed to delete photo");

            user.PhotoUrl = null;
            user.PhotoId = null;
            await _userManager.UpdateAsync(user);

            return _mapper.Map<UserToReturnDTO>(user);
        }
    }
}
