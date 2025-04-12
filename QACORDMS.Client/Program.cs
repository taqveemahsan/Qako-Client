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
                using (var loginForm = new Login(apiHelper))
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new MainForm(apiHelper, SessionHelper.CurrentUser.Role));
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







//using QACORDMS.Client.Helpers;

//namespace QACORDMS.Client
//{
//    internal static class Program
//    internal static class Program
//    {
//        /// <summary>
//        ///  The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            // To customize application configuration such as set high DPI settings or default font,
//            // see https://aka.ms/applicationconfiguration.
//            ApplicationConfiguration.Initialize();
//            Application.Run(new Login());
//        }
//    }
//}