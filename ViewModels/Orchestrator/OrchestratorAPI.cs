﻿using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Authenticators;

namespace CallAssistant.ViewModels.Orchestrator
{
    public class OrchestratorAPI : IOrchestratorAPI
    {
        public OrchestratorAPI(IConfiguration config)
        {
            appId = config.GetValue<string>("ORCH_APP_ID");
            appSecret = config.GetValue<string>("ORCH_APP_SECRET");
            orchUrl = config.GetValue<string>("ORCH_URL");
            Authenticate();
        }
        private string orchUrl { get; set; }
        private string token { get; set; }
        private DateTime tokenExpiration { get; set; }
        private string appId { get; set; }
        private string appSecret { get; set; }

        private RestClient client { get; set; }

        private void Authenticate()
        {
            var authOptions = new RestClientOptions("https://cloud.uipath.com/identity_/connect/token");

            var authClient = new RestClient(authOptions);

            var authRequest = new RestRequest();
            authRequest.AddParameter("grant_type", "client_credentials");
            authRequest.AddParameter("client_id", appId);
            authRequest.AddParameter("client_secret", appSecret);
            authRequest.AddParameter("scope", "OR.Execution.Read OR.Folders.Read");

            var response = authClient.Post(authRequest);

            var responseObject = JsonConvert.DeserializeObject<JObject>(response.Content);

            token = responseObject.GetValue("access_token").ToString();

            var options = new RestClientOptions(orchUrl)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer")
            };

            client = new RestClient(options);
        }

        public async Task<FolderDto[]> GetFolders(string fullyQualifiedName)
        {
            var request = new RestRequest("/odata/Folders");
            request.AddQueryParameter("$filter", "startsWith(FullyQualifiedName,'" + fullyQualifiedName + "')");
            FolderDto[] folders = null;
            try
            {
                var response = await client.GetAsync(request);
                var responseObject = JsonConvert.DeserializeObject<JObject>(response.Content);
                var vals = responseObject.GetValue("value").ToString();
                folders = JsonConvert.DeserializeObject<FolderDto[]>(vals);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return folders;
        }

        public async Task<ProcessDto[]> GetProcesses(FolderDto folder)
        {
            var request = new RestRequest("/odata/Releases");
            request.AddHeader("X-UIPATH-OrganizationUnitId", folder.Id);
            ProcessDto[] processes = null;
            try
            {
                var response = await client.GetAsync(request);
                var responseObject = JsonConvert.DeserializeObject<JObject>(response.Content);
                var vals = responseObject.GetValue("value").ToString();
                processes = JsonConvert.DeserializeObject<ProcessDto[]>(vals);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return processes;
        }
    }
}
