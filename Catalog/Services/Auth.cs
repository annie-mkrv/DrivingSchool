using Catalog.Services;
using Catalog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;

namespace Catalog.Services
{
    class ProfileResult
    {
        public Admin UserAdmin { get; set; }
        public Student UserStudent { get; set; }
    }
    public class Credentials
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    class Auth
    {
        public static Admin UserAdmin;
        public static Student UserStudent;

        public static async Task<bool> SignIn(string username, string password)
        {
            var credentials = new Credentials
            {
                username = username,
                password = password
            };
            var body = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync(App.API_HOST + "/auth", httpContent);
            result.EnsureSuccessStatusCode();
            var resultBody = await result.Content.ReadAsStringAsync();
            var profiles = JsonConvert.DeserializeObject<ProfileResult>(resultBody);
            if (profiles.UserAdmin != null)
            {
                UserAdmin = profiles.UserAdmin;
                return true;
            }
            if (profiles.UserStudent != null)
            {
                UserStudent = profiles.UserStudent;
                return false;
            }
            throw new Exception("Unauthorized");
        }
    }
}
