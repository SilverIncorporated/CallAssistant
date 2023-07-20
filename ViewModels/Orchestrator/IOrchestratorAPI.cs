namespace CallAssistant.ViewModels.Orchestrator
{
    public interface IOrchestratorAPI
    {
        Task<FolderDto[]> GetFolders(string fullyQualifiedName);
        Task<ProcessDto[]> GetProcesses(FolderDto folder);
    }
}
