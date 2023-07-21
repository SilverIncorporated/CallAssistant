using Microsoft.JSInterop;

namespace CallAssistant.Helpers
{
    public static class JSInterop
    {
        public static async ValueTask LogMessage(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeAsync<string>("LogMessage", message);
        }
    }
}
