using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Bdaya.Blazor.Helpers;

public partial class FileUploader
{
    [Parameter]
    public Func<InputFileChangeEventArgs, Task<string>> UploadToFileServer { get; set; } = null!;

    [Parameter]
    public EventCallback<string> OnUpload { get; set; }

    [Parameter] public string Width { get; set; } = "100%";
    [Parameter] public string Height { get; set; } = "15rem";

    [Parameter] public string DefaultImage { get; set; } = "https://www.chinkung.org/wp-content/themes/consultix/images/no-image-found-360x250.png";

    private Guid _id = Guid.NewGuid();


    [Parameter]
    public string Image { get; set; } = null!;

    private async Task UploadFile(InputFileChangeEventArgs e)
    {
        var res = await UploadToFileServer.Invoke(e);
        if (!string.IsNullOrEmpty(res))
        {
            Image = res;
            await OnUpload.InvokeAsync(Image);
        }
    }
}
