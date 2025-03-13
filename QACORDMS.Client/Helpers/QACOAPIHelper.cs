using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace QACORDMS.Client.Helpers
{
    public class QACOAPIHelper
    {
        private const string _BaseUrl = "https://localhost:7170/api/";
        private readonly HttpClient _httpClient;

        public QACOAPIHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void AddAuthorizationHeader()
        {
            if (SessionHelper.CurrentUser?.Token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionHelper.CurrentUser.Token);
            }
        }

        public async Task<APIUserModel> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync(_BaseUrl + "auth/login", new { username, password });
            try
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<APIUserModel>();
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid username or Password.");
            }
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<List<Client>>(_BaseUrl + "client/all");
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<Client>(_BaseUrl + $"client/{id}");
        }

        public async Task<bool> CreateClientAsync(Client client)
        {
            AddAuthorizationHeader();

            try
            {
                var response = await _httpClient.PostAsJsonAsync(_BaseUrl + "client/register", client);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateClientAsync(Client client)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.PutAsJsonAsync(_BaseUrl + "client", client);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<HttpResponseMessage> DeleteClientAsync(int id)
        {
            AddAuthorizationHeader();
            return await _httpClient.DeleteAsync(_BaseUrl + $"client/{id}");
        }

        public async Task<List<ClientProject>> GetClientProjectsAsync(Guid clientId)
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<List<ClientProject>>(_BaseUrl + $"client/{clientId}/projects");
        }

        public async Task<ClientProject> GetClientProjectByIdAsync(int id)
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<ClientProject>(_BaseUrl + $"clientproject/{id}");
        }

        public async Task<HttpResponseMessage> CreateClientProjectAsync(ClientProject clientProject)
        {
            AddAuthorizationHeader();
            return await _httpClient.PostAsJsonAsync(_BaseUrl + "client/create-project", clientProject);
        }

        public async Task<List<string>> GetDirectoriesAsync(string path)
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<List<string>>(_BaseUrl + $"directory?path={path}");
        }

        public async Task<List<GoogleDriveItem>> GetGoogleDriveFilesAsync(string folderId)
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<List<GoogleDriveItem>>(_BaseUrl + $"DocumentSync/{folderId}/list-items");
        }

        public async Task DownloadFileAsync(string fileId, Stream outputStream)
        {
            try
            {
                AddAuthorizationHeader();
                var response = await _httpClient.GetAsync(_BaseUrl + $"DocumentSync/download?fileId={fileId}", HttpCompletionOption.ResponseContentRead);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    await stream.CopyToAsync(outputStream);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> ReplaceFileAsync(string fileId, string filePath)
        {
            AddAuthorizationHeader();

            using (var multipartContent = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                multipartContent.Add(fileContent, "newFile", Path.GetFileName(filePath));
                multipartContent.Add(new StringContent(fileId), "fileId");

                var response = await _httpClient.PostAsync(_BaseUrl + "DocumentSync/replace-file", multipartContent);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<dynamic>();
                return result.FileId; // Assuming the API returns the updated file ID
            }
        }

        public async Task<string> UploadFileAsync(string filePath, string parentFolderId)
        {
            AddAuthorizationHeader();

            using (var multipartContent = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                multipartContent.Add(fileContent, "file", Path.GetFileName(filePath));
                multipartContent.Add(new StringContent(parentFolderId), "ParentFolderId");

                var response = await _httpClient.PostAsync(_BaseUrl + "DocumentSync/upload-file", multipartContent);
                response.EnsureSuccessStatusCode();

                // Strongly-typed response handling
                var result = await response.Content.ReadFromJsonAsync<UploadResponse>();
                if (result == null || string.IsNullOrEmpty(result.FileId))
                    throw new Exception("Invalid response from server: File ID not found.");

                return result.FileId;
            }
        }

        public class UploadResponse
        {
            public string FileId { get; set; }
        }
    }
}
