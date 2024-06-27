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
    public class Student
    {
        public int? id { get; set; }
        public string fio { get; set; }
        public string phone { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public string displayLine { get => $"{id}. {fio}"; }

        public string displayStudent { get => $"{id}. {fio}"; }

    }

    //class Credentials
    //{
    //    public string username { get; set; }
    //    public string password { get; set; }
    //}

    public class StudentService
    {
        public static Student Profile { get; private set; }
        public static async Task<ObservableCollection<Student>> getAll()
        {
            var result = await App.HttpClient.GetAsync(App.API_HOST + "/student/list");
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            var students = JsonConvert.DeserializeObject<ObservableCollection<Student>>(body);
            return students;
        }

        public static async Task Add(Student student)
        {
            var body = JsonConvert.SerializeObject(student);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync(App.API_HOST + "/student/add", httpContent);
        }

        public static async Task Update(Student student)
        {
            var body = JsonConvert.SerializeObject(student);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync(App.API_HOST + "/student/update", httpContent);
        }

        public static async Task Delete(Student student)
        {
            var result = await App.HttpClient.DeleteAsync(App.API_HOST + $"/student/delete/{student.id}");
            result.EnsureSuccessStatusCode();
        }

        public static async Task<Student> Login(string username, string password)
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
            var resultStudent = JsonConvert.DeserializeObject<Student>(resultBody);

            StudentService.Profile = resultStudent;

            return resultStudent;
        }

        public static void Logout()
        {
            StudentService.Profile = null;
        }
    }
}

