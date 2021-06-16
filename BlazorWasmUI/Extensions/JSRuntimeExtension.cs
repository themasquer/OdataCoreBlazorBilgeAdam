using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorWasmUI.Extensions
{
    public static class JSRuntimeExtension
    {
        public static async Task SaveFileAsAsync(this IJSRuntime jsRuntime, string fileName, byte[] data)
        {
            await jsRuntime.InvokeAsync<object>("saveFileAs", fileName, Convert.ToBase64String(data));
        }
    }
}
