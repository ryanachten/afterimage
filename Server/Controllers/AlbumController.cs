using afterimage.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace afterimage.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        [HttpPost]
        public void Post([FromForm] CreateAlbumRequest request)
        {
            var title = request.Title;

            // TODO: security considerations:
            // - use safe fiile name
            // - only allow approved extensions
            // - check size of uploaded file w/ set max size
            // - run virus and malware scanner on uploaded content
        }
    }
}
