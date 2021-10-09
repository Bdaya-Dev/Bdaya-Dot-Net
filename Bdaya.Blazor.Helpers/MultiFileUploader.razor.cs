using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bdaya.Blazor.Helpers;


public partial class MultiFileUploader
{
    [Parameter]
    public Func<InputFileChangeEventArgs, Task<List<string>>> UploadToFileServer { get; set; } = null!;

    [Parameter]
    public EventCallback<List<string>> OnUpload { get; set; }

    [Parameter] public string Width { get; set; } = "100%";
    [Parameter] public string Height { get; set; } = "15rem";

    [Parameter] public string DefaultImage { get; set; } = "https://www.chinkung.org/wp-content/themes/consultix/images/no-image-found-360x250.png";

    public int Index { get; set; } = 0;
    private Guid _id = Guid.NewGuid();


    [Parameter] public List<string> Images { get; set; } = new List<string>();

    private async Task UploadFiles(InputFileChangeEventArgs e)
    {
        var res = await UploadToFileServer.Invoke(e);
        if (res.Any())
        {
            Images.AddRange(res);
            await OnUpload.InvokeAsync(Images);
        }
    }

    private async Task OnDelete()
    {
        Images.Remove(Images[Index]);
        await OnUpload.InvokeAsync(Images);
    }

    private bool HasPrev()
    {
        return Index > 0;
    }
    private bool HasNext()
    {
        if (!Images.Any())
        {
            return false;
        }
        return Index < (Images.Count - 1);
    }

    private bool CanDelete()
    {
        return Images.Any();
    }
}