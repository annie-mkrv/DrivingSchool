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
    public class Car
    {
        public int? id { get; set; }
        public string car_number { get; set; }
        public string brand { get; set; }
        public bool transmission { get; set; }

        public string displayLine { get => $"{id}. {car_number}"; }

        public string displayComboLine { get => $"{id}. {car_number} {brand}"; }
    }
    
    public class CarService
    {
        public static Car Profile { get; private set; }
        public static async Task<ObservableCollection<Car>> getAll()
        {
            var result = await App.HttpClient.GetAsync(App.API_HOST + "/car/list");
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<ObservableCollection<Car>>(body);
            return cars;
        }

        public static async Task Add(Car car)
        {
            var body = JsonConvert.SerializeObject(car);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync(App.API_HOST + "/car/add", httpContent);
        }


        public static async Task Update(Car car)
        {
            var body = JsonConvert.SerializeObject(car);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync(App.API_HOST + "/car/update", httpContent);
        }

        public static async Task Delete(Car car)
        {
            var result = await App.HttpClient.DeleteAsync(App.API_HOST + $"/car/delete/{car.id}");
            result.EnsureSuccessStatusCode();
        }
    }
}

