using afterimage.Server.Models;
using afterimage.Server.Repositories;
using afterimage.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static afterimage.Shared.Constants.Api;

namespace afterimage.Server.Controllers
{
    [Authorize]
    [Route(Endpoints.Album)]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IFileStorageRepository _storageService;

        public AlbumController(IFileStorageRepository storageService)
        {
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var fileKeys = await _storageService.GetFiles();
            return Ok(new GetAlbumsResponse(fileKeys.ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> UploadAlbum([FromForm] CreateAlbumRequest request)
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
            return NoContent();
        }
    }
}
