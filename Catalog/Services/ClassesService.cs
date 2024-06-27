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
    public class Classes
    {
        public int? id { get; set; }
        public string date { get; set; }
        public int studentId { get; set; }
        public int instructorId { get; set; }
        public Student student { get; set; }
        public Instructor instructor { get; set; }
    }

    public class ClassesService
    {
        public static Classes Profile { get; private set; }
        public static async Task<ObservableCollection<Classes>> GetAll(DateTime date)
        {
            var dateString = date.ToString("yyyy-MM-dd"); // Format the date as 'YYYY-MM-DD'
            var requestUri = new Uri($"{App.API_HOST}/classes/list?date={dateString}");

            var result = await App.HttpClient.GetAsync(requestUri);
            result.EnsureSuccessStatusCode();

            var body = await result.Content.ReadAsStringAsync();
            var classes = JsonConvert.DeserializeObject<ObservableCollection<Classes>>(body);
            return classes;
        }

        public static async Task<ObservableCollection<Classes>> GetForStudent(Student student)
        {
            var requestUri = new Uri($"{App.API_HOST}/classes/list-by-student?date={student.id}");

            var result = await App.HttpClient.GetAsync(requestUri);
            result.EnsureSuccessStatusCode();

            var body = await result.Content.ReadAsStringAsync();
            var classes = JsonConvert.DeserializeObject<ObservableCollection<Classes>>(body);
            return classes;
        }
        public static async Task Add(Classes classes)
        {
            var body = JsonConvert.SerializeObject(classes);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync(App.API_HOST + "/classes/add", httpContent);
        }

        public static async Task Delete(Classes classes)
        {
            var result = await App.HttpClient.DeleteAsync(App.API_HOST + $"/classes/delete/{classes.id}");
            result.EnsureSuccessStatusCode();
        }
    }
}

