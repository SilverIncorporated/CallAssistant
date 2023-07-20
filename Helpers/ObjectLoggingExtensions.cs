using Microsoft.JSInterop;
using CallAssistant.ViewModels.Robot;
using CallAssistant.ViewModels.Orchestrator;

namespace CallAssistant.Helpers
{
    static class ObjectLoggingExtensions
    {

        public static async ValueTask LogFunction(this IJSRuntime jsRuntime, ProcessDto process)
        {
            await jsRuntime.InvokeVoidAsync("LogFunction", process);
        }

    }
}
