using afterimage.Server.Models;

namespace afterimage.Server.Services
{
    public interface IStorageService
    {
        Task<bool> UploadFiles(UploadFilesRequest request);
    }
}
