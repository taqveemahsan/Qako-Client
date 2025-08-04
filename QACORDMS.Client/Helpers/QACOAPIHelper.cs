using Newtonsoft.Json;
using QACORDMS.Client.Helpers.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace QACORDMS.Client.Helpers
{
    public class UploadProgressEventArgs : EventArgs
    {
        public int TotalFiles { get; set; }
        public int CompletedFiles { get; set; }
        public string CurrentFileName { get; set; }
        public double ProgressPercentage { get; set; }
    }

    public class QACOAPIHelper
    {
        private const string _BaseUrl = "https://localhost:44372/api/"; // Local
        //private const string _BaseUrl = "https://test.ibt-learning.com/api/"; // Prod
        private readonly HttpClient _httpClient;

        // Add event for progress updates
        public event EventHandler<UploadProgressEventArgs> UploadProgressChanged;

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

        public async Task<HttpResponseMessage> CreateClientAsync(Client client)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("client/registerV1", client);
                return response; // Return the full HttpResponseMessage
            }
            catch (HttpRequestException ex)
            {
                // Wrap network-related errors in a response-like object
                var errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    ReasonPhrase = $"Network error: {ex.Message}"
                };
                return errorResponse;
            }
            catch (Exception ex)
            {
                // Wrap other unexpected errors
                var errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = $"Unexpected error: {ex.Message}"
                };
                return errorResponse;
            }
        }

        public async Task<bool> UpdateClientAsyncV1(Client client)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Client/updateClient", client);
                return true; // Return the full HttpResponseMessage
            }
            catch (HttpRequestException ex)
            {
                // Wrap network-related errors in a response-like object
                var errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    ReasonPhrase = $"Network error: {ex.Message}"
                };
                return false;
            }
            catch (Exception ex)
            {
                // Wrap other unexpected errors
                var errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = $"Unexpected error: {ex.Message}"
                };
                return false;
            }
        }

        public async Task<HttpResponseMessage> UpdateClientAsync(ClientDto client)
        {
            AddAuthorizationHeader();
            try
            {
                // Configure JSON serialization options
                var jsonOptions = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter(null, false) }
                };

                // Serialize the client object to JSON
                var jsonContent = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(client, jsonOptions),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                // Log the JSON for debugging
                string jsonString = await jsonContent.ReadAsStringAsync();
                Console.WriteLine($"Request JSON: {jsonString}");

                // Make the POST request
                var response = await _httpClient.PostAsync("Client/updateClient", jsonContent);

                // Check the response
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"HTTP Error: {response.StatusCode}, Content: {errorContent}");
                    return response;
                }

                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException ex)
            {
                // Wrap network-related errors in a response-like object
                var errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    ReasonPhrase = $"Network error: {ex.Message}"
                };
                return errorResponse;
            }
            catch (Exception ex)
            {
                // Wrap other unexpected errors
                var errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = $"Unexpected error: {ex.Message}"
                };
                return errorResponse;
            }
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

            try
            {
                AddAuthorizationHeader();
                return await _httpClient.GetFromJsonAsync<List<GoogleDriveItem>>($"DocumentSync/{folderId}/list-items");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> CreateFolderAsync(string folderName, string parentFolderId)
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
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

        public async Task<string> ReplaceFileAsyncV1(string fileId, string filePath)
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

        public async Task<HttpResponseMessage> ReplaceFileAsync(string fileId, string filePath)
        {
            try
            {
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(fileId), "fileId");
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    formData.Add(fileContent, "newFile", Path.GetFileName(filePath));

                    var response = await _httpClient.PostAsync("DocumentSync/replace-file", formData);

                    if (!response.IsSuccessStatusCode)
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"HTTP Error: {response.StatusCode}, Content: {errorContent}");
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error replacing file: {ex.Message}");
                throw;
            }
        }

        public async Task<string> UploadFileAsync(string filePath, string parentFolderId)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    throw new Exception($"File not found: {filePath}");

                if (string.IsNullOrEmpty(parentFolderId))
                    throw new Exception("Parent folder ID is required for file upload.");

                AddAuthorizationHeader();

                using (var multipartContent = new MultipartFormDataContent())
                using (var fileStream = File.OpenRead(filePath))
                {
                    var fileContent = new ProgressableStreamContent(
                        fileStream,
                        81920, // 80KB buffer for efficiency
                        (sent, total) =>
                        {
                            double percent = total > 0 ? (sent * 100.0 / total) : 0;
                            UploadProgressChanged?.Invoke(this, new UploadProgressEventArgs
                            {
                                TotalFiles = 1,
                                CompletedFiles = sent == total ? 1 : 0,
                                CurrentFileName = Path.GetFileName(filePath),
                                ProgressPercentage = percent
                            });
                        });

                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                    multipartContent.Add(fileContent, "file", Path.GetFileName(filePath));
                    multipartContent.Add(new StringContent(parentFolderId), "ParentFolderId");

                    var response = await _httpClient.PostAsync("DocumentSync/upload-file", multipartContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Upload failed with status {response.StatusCode}: {errorContent}");
                    }

                    var result = await response.Content.ReadFromJsonAsync<UploadResponse>();
                    if (result == null || string.IsNullOrEmpty(result.FileId))
                        throw new Exception("Invalid response from server: File ID not found.");

                    return result.FileId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upload file: {ex.Message}");
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

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.PostAsync($"Auth/delete/{id}", null);

                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete client: {ex.Message}");
            }
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

        public async Task<HttpResponseMessage> RegisterUserAsync(User user)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Auth/register", user);
                return response; // Return the full HttpResponseMessage
            }
            catch (HttpRequestException ex)
            {
                // Wrap network-related errors in a response-like object
                var errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    ReasonPhrase = $"Network error: {ex.Message}"
                };
                return errorResponse;
            }
            catch (Exception ex)
            {
                // Wrap other unexpected errors
                var errorResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = $"Unexpected error: {ex.Message}"
                };
                return errorResponse;
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
            try
            {
                Console.WriteLine($"Attempting to delete permission with ID: {permissionId}");
                // Try the exact route from the backend API
                var response = await _httpClient.DeleteAsync($"project/permission/delete/{permissionId}");

                Console.WriteLine($"Delete response status: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Delete permission failed: {response.StatusCode} - {errorContent}");
                    throw new Exception($"Failed to delete permission: {response.StatusCode} - {errorContent}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in DeleteProjectPermissionAsync: {ex.Message}");
                throw new Exception($"Error deleting permission: {ex.Message}");
            }
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
        public async Task<bool> DeleteClientAsync(Guid id)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.PostAsync($"Client/delete/{id}", null);

                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete client: {ex.Message}");
            }
        }
        public async Task<bool> UpdateClientAsync(int id, string newName)
        {
            AddAuthorizationHeader();
            try
            {
                // Fetch the existing client to preserve other fields
                var existingClient = await GetClientByIdAsync(id); // Note: Adjust if GetClientByIdAsync expects Guid
                if (existingClient == null)
                {
                    throw new Exception("Client not found.");
                }

                // Update only the name
                existingClient.Name = newName;

                // Send the updated client to the API
                var response = await _httpClient.PutAsJsonAsync("client", existingClient);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update client: {ex.Message}");
            }
        }

        public async Task<HttpResponseMessage> RenameFileAsync(string itemId, string newName)
        {
            try
            {
                var requestBody = new
                {
                    ItemId = itemId,
                    NewName = newName
                };

                var jsonContent = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(requestBody),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                string jsonString = await jsonContent.ReadAsStringAsync();
                Console.WriteLine($"Request JSON: {jsonString}");

                var response = await _httpClient.PatchAsync("DocumentSync/rename", jsonContent);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"HTTP Error: {response.StatusCode}, Content: {errorContent}");
                    return response;
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error renaming item: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordRequest request)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Auth/change-password", request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Change password failed: {response.StatusCode} - {errorContent}");
                    throw new Exception($"Failed to change password: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in ChangePasswordAsync: {ex.Message}");
                throw new Exception($"Error changing password: {ex.Message}");
            }
        }

        public async Task<bool> DeleteFileAsync(string fileId)
        {
            AddAuthorizationHeader();
            try
            {
                var response = await _httpClient.PostAsync($"DocumentSync/delete-file/{fileId}", null);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // File not found, treat as already deleted
                    return false;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to delete file: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting file: {ex.Message}");
            }
        }

        private async Task WaitForFileRelease(string filePath)
        {
            int maxRetries = 20;
            int delayMs = 1000;
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    {
                        return;
                    }
                }
                catch (IOException)
                {
                    await Task.Delay(delayMs);
                }
            }
            throw new IOException($"File {filePath} remained locked after {maxRetries} attempts.");
        }
    }

    // Custom StreamContent that supports progress reporting
    public class ProgressableStreamContent : HttpContent
    {
        private const int defaultBufferSize = 4096;
        private readonly Stream _content;
        private readonly int _bufferSize;
        private readonly Action<long, long> _progress;

        public ProgressableStreamContent(Stream content, int bufferSize, Action<long, long> progress)
        {
            _content = content ?? throw new ArgumentNullException(nameof(content));
            _bufferSize = bufferSize > 0 ? bufferSize : defaultBufferSize;
            _progress = progress;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            var buffer = new byte[_bufferSize];
            long size = _content.Length;
            long uploaded = 0;
            int read;
            _content.Position = 0;

            while ((read = await _content.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await stream.WriteAsync(buffer, 0, read);
                uploaded += read;
                _progress?.Invoke(uploaded, size);
            }
        }

        protected override bool TryComputeLength(out long length)
        {
            length = _content.Length;
            return true;
        }
    }
}