using RestSharp;
using RestSharp.Authenticators;

namespace CallAssistant.ViewModels.Orchestrator
{
    public class OrchestratorAPI : IOrchestratorAPI
    {
        public OrchestratorAPI(string AppId, string AppSecret, string OrchestratorUrl)
        {
            appId = AppId;
            appSecret = AppSecret;
            orchUrl = OrchestratorUrl;
            Authenticate();
        }
        private string orchUrl { get; set; }
        private string token { get; set; }
        private DateTime tokenExpiration { get; set; }
        private string appId { get; set; }
        private string appSecret { get; set; }

        private RestClient client { get; set; }

        private RestClientOptions options { get; set; }

        private void Authenticate()
        {
            options = new RestClientOptions(orchUrl)
            {
                Authenticator = new HttpBasicAuthenticator(appId, appSecret)
            };

            client = new RestClient(options);
        }

        public async Task<OdataCollectionWrapper<FolderDto>?> GetFolders(string fullyQualifiedName)
        {
            var request = new RestRequest("/odata/Folders");
            request.AddQueryParameter("$filter", "FullyQualifiedName eq '" + fullyQualifiedName + "'");
            return await client.GetAsync<OdataCollectionWrapper<FolderDto>>(request);
        }

        public async Task<OdataCollectionWrapper<ProcessDto>?> GetProcesses(FolderDto folder)
        {
            var request = new RestRequest("/odata/Releases");
            request.AddHeader("X-UIPATH-OrganizationUnitId", folder.Id);
            return await client.GetAsync<OdataCollectionWrapper<ProcessDto>>(request);
        }
    }
}
