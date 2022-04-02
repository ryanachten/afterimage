using afterimage.Server.Models;
using afterimage.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace afterimage.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public AlbumController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost]
        public async Task Post([FromForm] CreateAlbumRequest request)
        {
            // TODO: security considerations:
            // - use safe file name
            // - only allow approved extensions
            // - check size of uploaded file w/ set max size
            // - run virus and malware scanner on uploaded content

            await _storageService.UploadFiles(new UploadFilesRequest()
            {
                FolderName = request.Title,
                Files = request.Images
            });
        }
    }
}
