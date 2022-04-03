using afterimage.Server.Models;

namespace afterimage.Server.Repositories
{
    public interface IFileStorageRepository
    {
        Task<IEnumerable<string>> GetFiles();
        Task UploadFiles(UploadFilesRequest request);
    }
}
