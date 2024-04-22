using ContactHub.Model.DTOs;

namespace ContactHub.Services.Interfaces
{
    public interface IPhotoManager
    {
        Task<PhotoResult> UploadImage(IFormFile file);
        Task<bool> DeleteImage(string publicId);
        Task<UserToReturnDTO> AddPhotoAsync(string id, IFormFile photo);
        Task<UserToReturnDTO> DeletePhotoAsync(string id);
    }
}
