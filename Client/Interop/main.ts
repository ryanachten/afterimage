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

interface ICreateAlbumRequest {
  title: string;
  images: File[];
}

async function submitForm(formElement: HTMLFormElement) {
  try {
    const formData = new FormData(formElement);

    const res = await fetch(formElement.action, {
      method: "POST",
      body: formData,
    });
    return res.ok;
  } catch (e) {
    console.log("error submitting form", e);
    return false;
  }
}

function Load() {
  window["interop"] = {
    getImageUrl,
    submitForm,
  };
}
Load();
