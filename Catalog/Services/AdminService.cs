using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Services
{
    public class Admin
    {
        public int? id { get; set; }
        public string fio { get; set; }
        public string phone { get; set; }
        public string passport { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public string displayLine { get => $"{id}. {fio}"; }


    }

    //class credentials
    //{
    //    public string username { get; set; }
    //    public string password { get; set; }
    //}

    public class AdminService
    {
        public static Admin Profile { get; private set; }
        public static async Task<ObservableCollection<Admin>> getAll()
        {
            var result = await App.HttpClient.GetAsync(App.API_HOST + "/admin/list");
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            var admins = JsonConvert.DeserializeObject<ObservableCollection<Admin>>(body);
            return admins;
        }

        public static async Task Add(Admin admin)
        {
            var body = JsonConvert.SerializeObject(admin);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync(App.API_HOST + "/admin/add", httpContent);
        }

        public static async Task Update(Admin admin)
        {
            var body = JsonConvert.SerializeObject(admin);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync(App.API_HOST + "/admin/update", httpContent);
        }

        public static async Task Delete(Admin admin)
        {
            var result = await App.HttpClient.DeleteAsync(App.API_HOST + $"/admin/delete/{admin.id}");
            result.EnsureSuccessStatusCode();
        }

        public static async Task<Admin> Login(string username, string password)
        {
            var credentials = new Credentials
            {
                username = username,
                password = password
            };
            var body = JsonConvert.SerializeObject(credentials);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync(App.API_HOST + "/login", httpContent);
            result.EnsureSuccessStatusCode();
            var resultBody = await result.Content.ReadAsStringAsync();
            var resultEmployee = JsonConvert.DeserializeObject<Admin>(resultBody);

            AdminService.Profile = resultEmployee;

            return resultEmployee;
        }

        public static void Logout()
        {
            AdminService.Profile = null;
        }
    }
}
