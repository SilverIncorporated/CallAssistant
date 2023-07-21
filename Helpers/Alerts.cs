using Microsoft.JSInterop;

namespace CallAssistant.Helpers
{
    static class AlertHelpers
    {

        public static async ValueTask SweetAlert(this IJSRuntime jsRuntime, bool success, string message)
        {
            if (success)
            {
                await jsRuntime.InvokeVoidAsync("SweetAlert", "success", message);
            }
            else
            {
                await jsRuntime.InvokeVoidAsync("SweetAlert", "error", message);
            }

        }
        
        public static async ValueTask Toastr(this IJSRuntime jsRuntime, bool success, string message)
        {
            await jsRuntime.InvokeVoidAsync("ShowToastr", success ? "success" : "error", message);
        }
        
    }
}