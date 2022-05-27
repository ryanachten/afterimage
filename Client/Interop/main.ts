// Stream returned by .NET
// unsure as to full contract
interface IStream {
  arrayBuffer: () => ArrayBuffer;
}

async function getImageUrl(imageStream: IStream) {
  const buffer = await imageStream.arrayBuffer();
  const blob = new Blob([buffer]);
  const url = URL.createObjectURL(blob);
  return url;
}

function Load() {
  window["interop"] = {
    getImageUrl,
  };
}
Load();
