using Microsoft.JSInterop;
using CallAssistant.ViewModels.Robot;

namespace CallAssistant.Helpers
{
    static class RobotJSExtensions
    {

        public static async ValueTask GetProcesses(this IJSRuntime jsRuntime, object reference, string beginsWith)
        {
            var objectRef = DotNetObjectReference.Create(reference);
            await jsRuntime.InvokeAsync<List<BotProcess>>("GetProcesses", objectRef, beginsWith);
        }

        public static async ValueTask GetNextFile(this IJSRuntime jsRuntime, object reference)
        {
            var objectRef = DotNetObjectReference.Create(reference);
            await jsRuntime.InvokeAsync<string>("GetNextFile", objectRef);
        }

        public static async ValueTask StartJob(this IJSRuntime jsRuntime, object reference, BotProcess process)
        {
            var objectRef = DotNetObjectReference.Create(reference);
            await jsRuntime.InvokeAsync<string>("RunProcess", objectRef, process);
        }

        public static async ValueTask SaveTags(this IJSRuntime jsRuntime, object reference, string json, string name)
        {
            var objectRef = DotNetObjectReference.Create(reference);
            await jsRuntime.InvokeAsync<string>("SaveTagging", objectRef, json, name);
        }

    }
}
