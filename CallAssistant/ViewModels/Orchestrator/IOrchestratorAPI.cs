namespace CallAssistant.ViewModels.Orchestrator
{
    public interface IOrchestratorAPI
    {
        Task<OdataCollectionWrapper<FolderDto>?> GetFolders(string fullyQualifiedName);
        Task<OdataCollectionWrapper<ProcessDto>?> GetProcesses(FolderDto folder);
    }
}
