using System;
using System.Windows.Forms;
using QACORDMS.Client.Helpers;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace QACORDMS.Client
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var apiHelper = new QACOAPIHelper(new HttpClient());

            var user = CheckForSavedToken(apiHelper).Result;
            if (user != null)
            {
                SessionHelper.CurrentUser = user;
                Application.Run(new MainForm(apiHelper, user.Role));
            }
            else
            {
                bool loggedIn = false;
                while (!loggedIn)
                {
                    using (var loginForm = new Login(apiHelper))
                    {
                        if (loginForm.ShowDialog() == DialogResult.OK)
                        {
                            loggedIn = true;
                            Application.Run(new MainForm(apiHelper, SessionHelper.CurrentUser.Role));
                        }
                        else
                        {
                            // User cancelled or closed the form, exit app
                            break;
                        }
                    }
                }
            }
        }

        private static async Task<APIUserModel> CheckForSavedToken(QACOAPIHelper apiHelper)
        {
            var savedToken = LoadToken();
            if (savedToken != null && !IsTokenExpired(savedToken))
            {
                try
                {
                    var user = await apiHelper.ValidateTokenAsync(savedToken.Token);
                    if (user != null)
                    {
                        return new APIUserModel
                        {
                            FirstName = user.FirstName,
                            Id = Guid.Parse(user.Id),
                            Role = user.Role,
                            SecondName = user.LastName,
                            Token = savedToken.Token
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error validating token: {ex.Message}");
                }

                DeleteToken();
            }

            return null;
        }

        private static TokenData LoadToken()
        {
            string tokenPath = Path.Combine(Application.LocalUserAppDataPath, "token.json");
            if (File.Exists(tokenPath))
            {
                string json = File.ReadAllText(tokenPath);
                return JsonConvert.DeserializeObject<TokenData>(json);
            }
            return null;
        }

        private static void DeleteToken()
        {
            string tokenPath = Path.Combine(Application.LocalUserAppDataPath, "token.json");
            if (File.Exists(tokenPath))
            {
                File.Delete(tokenPath);
            }
        }

        private static bool IsTokenExpired(TokenData tokenData)
        {
            TimeSpan tokenAge = DateTime.Now - tokenData.SavedAt;
            return tokenAge.TotalDays > 5;
        }
    }

    public class TokenData
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public DateTime SavedAt { get; set; }
    }
}