using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using static afterimage.Client.Constants.Image;

namespace afterimage.Client.Shared
{
    public partial class UploadForm : ComponentBase
    {
        [Inject]
        public IJSRuntime JsRuntime { get; set; } = default!;

        [Inject]
        public HttpClient HttpClient  { get; set; } = default!;

        protected string? _albumTitle = null;
        protected Dictionary<string, string> _urls = new();
        protected Dictionary<string, IBrowserFile> _files = new();

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

            long maxFileSize = 1024 * 1024 * 15; // TODO: this max size needs to be represented in the UI somewhere

            using var content = new MultipartFormDataContent();

            content.Add(
                content: new StringContent(_albumTitle),
                name: "\"title\""
            );

            foreach (var file in _files)
            {
                var fileContent = new StreamContent(
                    file.Value.OpenReadStream(maxFileSize)
                );

                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.Value.ContentType);

                content.Add(
                    content: fileContent,
                    name: "\"images\"",
                    fileName: file.Value.Name
                );
            }

            await HttpClient.PostAsync("/album", content);

            // TODO: user-facing error handling
        }
    }
}
