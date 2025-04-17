using Newtonsoft.Json;
using QACORDMS.Client.Helpers.Models;
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
        //private const string _BaseUrl = "https://localhost:44372/api/"; // Local
        private const string _BaseUrl = "https://qako.ddns.net/api/"; // Prod
        private readonly HttpClient _httpClient;

        public QACOAPIHelper(HttpClient httpClient)
        {
            // Configure HttpClient to accept self-signed certificates (for development)
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignore certificate errors in dev
            };
            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = new Uri(_BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
            var response = await _httpClient.PostAsJsonAsync("auth/login", new { username, password });
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

        public async Task<User> ValidateTokenAsync(string token)
        {
            var tokenModel = new
            {
                Token = token
            };

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(tokenModel), Encoding.UTF8, "application/json");
                Console.WriteLine($"ValidateToken Request: {JsonConvert.SerializeObject(tokenModel)}"); // Debug logging

                // Use the correct endpoint with proper casing
                var response = await _httpClient.PostAsync("Auth/validate-token", content); 

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"ValidateToken Response: {responseContent}"); // Debug logging
                    var validateResponse = JsonConvert.DeserializeObject<ValidateTokenResponse>(responseContent);
                    if (validateResponse.TokenValid)
                    {
                        return new User
                        {
                            Id = validateResponse.Id,
                            Username = validateResponse.UserName,
                            Email = validateResponse.Email,
                            FirstName = validateResponse.FirstName,
                            LastName = validateResponse.LastName,
                            Role = validateResponse.Role 
                        };
                    }
                    else
                    {
                        Console.WriteLine("Token is not valid according to API response."); // Debug logging
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"ValidateToken Failed: Status {response.StatusCode}, Error: {errorContent}"); // Debug logging
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ValidateToken Exception: {ex.Message}"); // Debug logging
                throw; // Re-throw the exception for further debugging
            }

            return null;
        }

        public async Task<PagedClientResponse> GetClientsAsync(string search = "", int page = 1, int pageSize = 10)
        {
            AddAuthorizationHeader();
            var url = $"client/all?search={Uri.EscapeDataString(search)}&page={page}&pageSize={pageSize}";
            var response = await _httpClient.GetFromJsonAsync<PagedClientResponse>(url);
            return response ?? new PagedClientResponse { Clients = new List<Client>(), TotalCount = 0, TotalPages = 0 };
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<Client>($"client/{id}");
        }

        public async Task<bool> CreateClientAsync(Client client)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("client/register", client);
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
                var response = await _httpClient.PutAsJsonAsync("client", client);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteClientAsync(Guid id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"client/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ClientProject>> GetClientProjectsAsync(Guid clientId)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetFromJsonAsync<List<ClientProject>>($"Client/{clientId}/projectsbyType");
            return response ?? new List<ClientProject>();
        }

        public async Task<ClientProject> GetClientProjectByIdAsync(int id)
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<ClientProject>($"clientproject/{id}");
        }

        public async Task<HttpResponseMessage> CreateClientProjectAsync(ClientProject clientProject)
        {
            AddAuthorizationHeader();
            return await _httpClient.PostAsJsonAsync("client/create-project", clientProject);
        }

        public async Task<List<string>> GetDirectoriesAsync(string path)
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<List<string>>($"directory?path={path}");
        }

        public async Task<List<GoogleDriveItem>> GetGoogleDriveFilesAsync(string folderId)
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<List<GoogleDriveItem>>($"DocumentSync/{folderId}/list-items");
        }

        public async Task<string> CreateFolderAsync(string folderName, string parentFolderId)
        {
            AddAuthorizationHeader();

            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(folderName), "folderName");
                content.Add(new StringContent(parentFolderId), "parentFolderId");

                Console.WriteLine($"Sending request to {_BaseUrl}DocumentSync/create-folder");
                Console.WriteLine($"FolderName: {folderName}, ParentFolderId: {parentFolderId}");

                var response = await _httpClient.PostAsync("DocumentSync/create-folder", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to create folder: {response.StatusCode} - {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<FolderCreationResponse>();
                if (result == null || string.IsNullOrEmpty(result.FolderId))
                    throw new Exception("Invalid response from server: Folder ID not found.");

                return result.FolderId;
            }
        }

        public class FolderCreationResponse
        {
            public string FolderId { get; set; }
        }

        public async Task DownloadFileAsync(string fileId, Stream outputStream)
        {
            try
            {
                AddAuthorizationHeader();
                var response = await _httpClient.GetAsync($"DocumentSync/download?fileId={fileId}", HttpCompletionOption.ResponseContentRead);
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

                var response = await _httpClient.PostAsync("DocumentSync/replace-file", multipartContent);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<dynamic>();
                return result.FileId;
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

                var response = await _httpClient.PostAsync("DocumentSync/upload-file", multipartContent);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<UploadResponse>();
                if (result == null || string.IsNullOrEmpty(result.FileId))
                    throw new Exception("Invalid response from server: File ID not found.");

                return result.FileId;
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<User>>("Auth/users");
                return response ?? new List<User>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch users: {ex.Message}");
            }
        }

        public async Task<bool> CreateUserAsync(Client user)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync("Client/register", user);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"Client/delete/{userId}"); // Fixed extra slash
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<UserResponse> GetUsersAsync(string search = "", int pageNumber = 1, int pageSize = 10)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.GetFromJsonAsync<UserResponse>($"Auth/users?search={Uri.EscapeDataString(search)}&pageNumber={pageNumber}&pageSize={pageSize}");
                return response ?? new UserResponse();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch users: {ex.Message}");
            }
        }

        public async Task<UserResponse> GetRoleUsersAsync(string search = "", int pageNumber = 1, int pageSize = 10)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.GetFromJsonAsync<UserResponse>($"Auth/users?search={Uri.EscapeDataString(search)}&pageNumber={pageNumber}&pageSize={pageSize}&role=User");
                return response ?? new UserResponse();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch users: {ex.Message}");
            }
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Auth/register", user);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<UserProjectPermissionDto>> GetPermissionsByProjectIdAsync(Guid projectId)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetFromJsonAsync<List<UserProjectPermissionDto>>($"Client/project/{projectId}/permissions");
            return response ?? new List<UserProjectPermissionDto>();
        }

        public async Task<bool> AddProjectPermissionAsync(UserProjectPermissionDto permission)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync("Client/project/permission/add", permission);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EditProjectPermissionAsync(Guid permissionId, UserProjectPermissionDto permission)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"Client/project/permission/edit/{permissionId}", permission);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProjectPermissionAsync(Guid permissionId)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"Client/project/permission/delete/{permissionId}");
            return response.IsSuccessStatusCode;
        }

        public class UploadResponse
        {
            public string FileId { get; set; }
        }

        public async Task<FolderStructureResponse> GetFolderStructureListAsync(string search = "", int page = 1, int pageSize = 10)
        {
            try
            {
                var query = $"Client/folder-structure?search={Uri.EscapeDataString(search)}&page={page}&pageSize={pageSize}";
                var response = await _httpClient.GetFromJsonAsync<FolderStructureResponse>(query);
                return response ?? new FolderStructureResponse();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch folder structure: {ex.Message}");
            }
        }

        public async Task<HttpResponseMessage> CheckEmailAsync(string email)
        {
            var requestData = new { email };
            var content = new StringContent(
                JsonConvert.SerializeObject(requestData),
                Encoding.UTF8,
                "application/json"
            );

            return await _httpClient.PostAsync("Auth/check-email", content);
        }

        public async Task<HttpResponseMessage> SendOtpAsync(string email)
        {
            var requestData = new { email };
            var content = new StringContent(
                JsonConvert.SerializeObject(requestData),
                Encoding.UTF8,
                "application/json"
            );

            return await _httpClient.PostAsync("Auth/send-otp", content);
        }

        public async Task<HttpResponseMessage> ResetPasswordAsync(string email, string otp, string newPassword, string confirmPassword)
        {
            var requestData = new { email, otp, newPassword, confirmPassword };
            var content = new StringContent(
                JsonConvert.SerializeObject(requestData),
                Encoding.UTF8,
                "application/json"
            );

            return await _httpClient.PostAsync("Auth/reset-password", content);
        }
    }
}













//using Newtonsoft.Json;
//using QACORDMS.Client.Helpers.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using System.Text;
//using System.Threading.Tasks;

//namespace QACORDMS.Client.Helpers
//{
//    public class QACOAPIHelper
//    {
//        private const string _BaseUrl = "https://localhost:44372/api/"; //local
//        //private const string _BaseUrl = "https://qako.ddns.net/api/"; //Prod
//        private readonly HttpClient _httpClient;

//        public QACOAPIHelper(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        private void AddAuthorizationHeader()
//        {
//            if (SessionHelper.CurrentUser?.Token != null)
//            {
//                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionHelper.CurrentUser.Token);
//            }
//        }

//        public async Task<APIUserModel> LoginAsync(string username, string password)
//        {
//            var response = await _httpClient.PostAsJsonAsync(_BaseUrl + "auth/login", new { username, password });
//            try
//            {
//                response.EnsureSuccessStatusCode();
//                return await response.Content.ReadFromJsonAsync<APIUserModel>();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Invalid username or Password.");
//            }
//        }

//        public async Task<PagedClientResponse> GetClientsAsync(string search = "", int page = 1, int pageSize = 10)
//        {
//            AddAuthorizationHeader();
//            var url = $"{_BaseUrl}client/all?search={Uri.EscapeDataString(search)}&page={page}&pageSize={pageSize}";
//            var response = await _httpClient.GetFromJsonAsync<PagedClientResponse>(url);
//            return response ?? new PagedClientResponse { Clients = new List<Client>(), TotalCount = 0, TotalPages = 0 };
//        }

//        public async Task<Client> GetClientByIdAsync(int id)
//        {
//            AddAuthorizationHeader();
//            return await _httpClient.GetFromJsonAsync<Client>(_BaseUrl + $"client/{id}");
//        }

//        public async Task<bool> CreateClientAsync(Client client)
//        {
//            AddAuthorizationHeader();

//            try
//            {
//                var response = await _httpClient.PostAsJsonAsync(_BaseUrl + "client/register", client);
//                response.EnsureSuccessStatusCode();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }

//        public async Task<bool> UpdateClientAsync(Client client)
//        {
//            AddAuthorizationHeader();
//            try
//            {
//                var response = await _httpClient.PutAsJsonAsync(_BaseUrl + "client", client);
//                response.EnsureSuccessStatusCode();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }

//        public async Task<bool> DeleteClientAsync(Guid id)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.DeleteAsync($"{_BaseUrl}client/{id}");
//            return response.IsSuccessStatusCode;
//        }

//        //public async Task<List<ClientProject>> GetClientProjectsAsync(Guid clientId)
//        //{
//        //    AddAuthorizationHeader();
//        //    return await _httpClient.GetFromJsonAsync<List<ClientProject>>(_BaseUrl + $"client/{clientId}/projects");
//        //}

//        public async Task<List<ClientProject>> GetClientProjectsAsync(Guid clientId)
//        {
//            AddAuthorizationHeader(); // Token with role yahan pass hota hai
//            var response = await _httpClient.GetFromJsonAsync<List<ClientProject>>(_BaseUrl + $"Client/{clientId}/projectsbyType");
//            return response ?? new List<ClientProject>();
//        }

//        public async Task<ClientProject> GetClientProjectByIdAsync(int id)
//        {
//            AddAuthorizationHeader();
//            return await _httpClient.GetFromJsonAsync<ClientProject>(_BaseUrl + $"clientproject/{id}");
//        }

//        public async Task<HttpResponseMessage> CreateClientProjectAsync(ClientProject clientProject)
//        {
//            AddAuthorizationHeader();
//            return await _httpClient.PostAsJsonAsync(_BaseUrl + "client/create-project", clientProject);
//        }

//        public async Task<List<string>> GetDirectoriesAsync(string path)
//        {
//            AddAuthorizationHeader();
//            return await _httpClient.GetFromJsonAsync<List<string>>(_BaseUrl + $"directory?path={path}");
//        }

//        public async Task<List<GoogleDriveItem>> GetGoogleDriveFilesAsync(string folderId)
//        {
//            AddAuthorizationHeader();
//            return await _httpClient.GetFromJsonAsync<List<GoogleDriveItem>>(_BaseUrl + $"DocumentSync/{folderId}/list-items");
//        }

//        public async Task<string> CreateFolderAsync(string folderName, string parentFolderId)
//        {
//            AddAuthorizationHeader();

//            using (var content = new MultipartFormDataContent())
//            {
//                content.Add(new StringContent(folderName), "folderName");
//                content.Add(new StringContent(parentFolderId), "parentFolderId");

//                // Debugging: Log the request
//                Console.WriteLine($"Sending request to {_BaseUrl}DocumentSync/create-folder");
//                Console.WriteLine($"FolderName: {folderName}, ParentFolderId: {parentFolderId}");

//                var response = await _httpClient.PostAsync(_BaseUrl + "DocumentSync/create-folder", content);

//                if (!response.IsSuccessStatusCode)
//                {
//                    var errorContent = await response.Content.ReadAsStringAsync();
//                    throw new Exception($"Failed to create folder: {response.StatusCode} - {errorContent}");
//                }

//                var result = await response.Content.ReadFromJsonAsync<FolderCreationResponse>();
//                if (result == null || string.IsNullOrEmpty(result.FolderId))
//                    throw new Exception("Invalid response from server: Folder ID not found.");

//                return result.FolderId;
//            }
//        }

//        public class FolderCreationResponse
//        {
//            public string FolderId { get; set; }
//        }

//        public async Task DownloadFileAsync(string fileId, Stream outputStream)
//        {
//            try
//            {
//                AddAuthorizationHeader();
//                var response = await _httpClient.GetAsync(_BaseUrl + $"DocumentSync/download?fileId={fileId}", HttpCompletionOption.ResponseContentRead);
//                response.EnsureSuccessStatusCode();

//                using (var stream = await response.Content.ReadAsStreamAsync())
//                {
//                    await stream.CopyToAsync(outputStream);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public async Task<string> ReplaceFileAsync(string fileId, string filePath)
//        {
//            AddAuthorizationHeader();

//            using (var multipartContent = new MultipartFormDataContent())
//            {
//                var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
//                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
//                multipartContent.Add(fileContent, "newFile", Path.GetFileName(filePath));
//                multipartContent.Add(new StringContent(fileId), "fileId");

//                var response = await _httpClient.PostAsync(_BaseUrl + "DocumentSync/replace-file", multipartContent);
//                response.EnsureSuccessStatusCode();

//                var result = await response.Content.ReadFromJsonAsync<dynamic>();
//                return result.FileId; // Assuming the API returns the updated file ID
//            }
//        }

//        public async Task<string> UploadFileAsync(string filePath, string parentFolderId)
//        {
//            AddAuthorizationHeader();

//            using (var multipartContent = new MultipartFormDataContent())
//            {
//                var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
//                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
//                multipartContent.Add(fileContent, "file", Path.GetFileName(filePath));
//                multipartContent.Add(new StringContent(parentFolderId), "ParentFolderId");

//                var response = await _httpClient.PostAsync(_BaseUrl + "DocumentSync/upload-file", multipartContent);
//                response.EnsureSuccessStatusCode();

//                // Strongly-typed response handling
//                var result = await response.Content.ReadFromJsonAsync<UploadResponse>();
//                if (result == null || string.IsNullOrEmpty(result.FileId))
//                    throw new Exception("Invalid response from server: File ID not found.");

//                return result.FileId;
//            }
//        }

//        public async Task<List<User>> GetUsersAsync()
//        {
//            AddAuthorizationHeader();
//            try
//            {
//                var response = await _httpClient.GetFromJsonAsync<List<User>>(_BaseUrl + "Auth/users");
//                return response ?? new List<User>();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Failed to fetch users: {ex.Message}");
//            }
//        }

//        public async Task<bool> CreateUserAsync(Client user)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.PostAsJsonAsync(_BaseUrl + "Client/register", user);
//            response.EnsureSuccessStatusCode();
//            return true;
//        }

//        public async Task<bool> DeleteUserAsync(Guid userId)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.DeleteAsync(_BaseUrl + $"Client/delete//{userId}");
//            response.EnsureSuccessStatusCode();
//            return true;
//        }

//        public async Task<UserResponse> GetUsersAsync(string search = "", int pageNumber = 1, int pageSize = 10)
//        {
//            AddAuthorizationHeader();
//            try
//            {
//                var response = await _httpClient.GetFromJsonAsync<UserResponse>(_BaseUrl + $"Auth/users?search={Uri.EscapeDataString(search)}&pageNumber={pageNumber}&pageSize={pageSize}");
//                return response ?? new UserResponse();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Failed to fetch users: {ex.Message}");
//            }
//        }
//        public async Task<UserResponse> GetRoleUsersAsync(string search = "", int pageNumber = 1, int pageSize = 10)
//        {
//            AddAuthorizationHeader();
//            try
//            {
//                var response = await _httpClient.GetFromJsonAsync<UserResponse>(_BaseUrl + $"Auth/users?search={Uri.EscapeDataString(search)}&pageNumber={pageNumber}&pageSize={pageSize}&role=User");
//                return response ?? new UserResponse();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Failed to fetch users: {ex.Message}");
//            }
//        }

//        public async Task<bool> RegisterUserAsync(User user)
//        {
//            AddAuthorizationHeader();
//            try
//            {
//                var response = await _httpClient.PostAsJsonAsync(_BaseUrl + "Auth/register", user);
//                response.EnsureSuccessStatusCode();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                // Optional: Log the exception if needed
//                return false; // Ya exception throw karo agar detailed error chahiye
//            }
//        }
//        public async Task<List<UserProjectPermissionDto>> GetPermissionsByProjectIdAsync(Guid projectId)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.GetFromJsonAsync<List<UserProjectPermissionDto>>(_BaseUrl + $"Client/project/{projectId}/permissions");
//            return response ?? new List<UserProjectPermissionDto>();
//        }

//        public async Task<bool> AddProjectPermissionAsync(UserProjectPermissionDto permission)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.PostAsJsonAsync(_BaseUrl + "Client/project/permission/add", permission);
//            return response.IsSuccessStatusCode;
//        }

//        public async Task<bool> EditProjectPermissionAsync(Guid permissionId, UserProjectPermissionDto permission)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.PutAsJsonAsync(_BaseUrl + $"Client/project/permission/edit/{permissionId}", permission);
//            return response.IsSuccessStatusCode;
//        }

//        public async Task<bool> DeleteProjectPermissionAsync(Guid permissionId)
//        {
//            AddAuthorizationHeader();
//            var response = await _httpClient.DeleteAsync(_BaseUrl + $"Client/project/permission/delete/{permissionId}");
//            return response.IsSuccessStatusCode;
//        }
//        public class UploadResponse
//        {
//            public string FileId { get; set; }
//        }

//        public async Task<FolderStructureResponse> GetFolderStructureListAsync(string search = "", int page = 1, int pageSize = 10)
//        {
//            try
//            {
//                var query = $"Client/folder-structure?search={Uri.EscapeDataString(search)}&page={page}&pageSize={pageSize}";
//                var response = await _httpClient.GetFromJsonAsync<FolderStructureResponse>(_BaseUrl + query);
//                return response ?? new FolderStructureResponse();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Failed to fetch folder structure: {ex.Message}");
//            }
//        }

//        public async Task<User> ValidateTokenAsync(string token)
//        {
//            var tokenModel = new
//            {
//                Token = token
//            };

//            var content = new StringContent(JsonConvert.SerializeObject(tokenModel), Encoding.UTF8, "application/json");
//            var response = await _httpClient.PostAsync($"{_BaseUrl}/auth/validate-token", content);

//            if (response.IsSuccessStatusCode)
//            {
//                var responseContent = await response.Content.ReadAsStringAsync();
//                var validateResponse = JsonConvert.DeserializeObject<ValidateTokenResponse>(responseContent);
//                if (validateResponse.TokenValid)
//                {
//                    return new User
//                    {
//                        Id = validateResponse.Id,
//                        Username = validateResponse.UserName,
//                        Email = validateResponse.Email,
//                        FirstName = validateResponse.FirstName,
//                        LastName = validateResponse.LastName,
//                        //RoleNames = validateResponse.Role
//                    };
//                }
//            }

//            return null;
//        }
//    }
//}