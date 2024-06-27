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
    public class Instructor
    {
        public int? id { get; set; }
        public string fio { get; set; }
        public string phone { get; set; }
        public int carId { get; set; }
        public Car car { get; set; }
        public string displayLine { get => $"{id}. {fio}"; }

        public string displayInstructor { get => $"{id}. {fio}, {carId}"; }
    }

    public class InstructorService
    {
        public static Instructor Profile { get; private set; }
        public static async Task<ObservableCollection<Instructor>> getAll()
        {
            var result = await App.HttpClient.GetAsync(App.API_HOST + "/instructor/list");
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            var instructors = JsonConvert.DeserializeObject<ObservableCollection<Instructor>>(body);
            return instructors;
        }

        public static async Task Add(Instructor instructor)
        {
            var body = JsonConvert.SerializeObject(instructor);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync(App.API_HOST + "/instructor/add", httpContent);
        }

        public static async Task Update(Instructor instructor)
        {
            var body = JsonConvert.SerializeObject(instructor);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync(App.API_HOST + "/instructor/update", httpContent);
        }

        public static async Task Delete(Instructor instructor)
        {
            var result = await App.HttpClient.DeleteAsync(App.API_HOST + $"/instructor/delete/{instructor.id}");
            result.EnsureSuccessStatusCode();
        }
    }
}

