using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using static afterimage.Client.Constants.Image;

namespace afterimage.Client.Shared
{
    public partial class UploadForm : ComponentBase
    {
        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public HttpClient HttpClient  { get; set; }
        
        protected string? _albumTitle = null;
        protected Dictionary<string, string> _urls = new();
        protected Dictionary<string, IBrowserFile> _files = new();

        protected ElementReference _form { get; set; }

        protected async Task DisplayImage(InputFileChangeEventArgs args)
        {
            var files = args.GetMultipleFiles();
            // TODO: this could probably be optimised better
            foreach (var file in files)
            {
                var resizedImage = await file.RequestImageFileAsync(FileType, MaxPxDimension, MaxPxDimension);
                var imageStream = resizedImage.OpenReadStream();
                var streamReference = new DotNetStreamReference(imageStream);
                var url = await JsRuntime.InvokeAsync<string>("interop.getImageUrl", streamReference);
                _urls.Add(file.Name, url);
                _files.Add(file.Name, file);
            }
        }

        protected async Task UploadAlbum()
        {
            // TODO: add better user-facing validation
            if (_albumTitle == null || _files.Count == 0) return;

            var success = await JsRuntime.InvokeAsync<bool>("interop.submitForm", _form);

            // TODO: user-facing error handling
        }
    }
}
