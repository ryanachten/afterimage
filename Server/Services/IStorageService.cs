using afterimage.Server.Models;

namespace afterimage.Server.Services
{
    public interface IStorageService
    {
        Task<IEnumerable<string>> GetFiles();
        Task UploadFiles(UploadFilesRequest request);
    }
}
