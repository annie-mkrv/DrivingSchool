//АВТОМОБИЛИ - СОЗДАНИЕ/РЕДАКТИРОВАНИЕ

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Catalog.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Protection.PlayReady;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace Catalog
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class CarEditor : Page
    {
        private int? id;

        public CarEditor()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter == null)
            {
                UpdateButton.Visibility = Visibility.Collapsed;
                UpdateHeader.Visibility = Visibility.Collapsed;
                BackArrowUpdate.Visibility = Visibility.Collapsed;
            }
            else
            {
                var car = e.Parameter as Car;

                AddButton.Visibility = Visibility.Collapsed;
                AddHeader.Visibility = Visibility.Collapsed;
                BackArrowUpdate.Visibility = Visibility.Collapsed;

                UpdateHeader.Text = $"Редактирование автомобиля №{car.id}";

                id = car.id;

                Number.Text = car.car_number;
                Brand.Text = car.brand;

                if (!car.transmission)
                {
                    Transmission_ComboBox.SelectedIndex = 1;
                }
            }
        }


        // Проверка валидации
        public bool Validate()
        {
            if (String.IsNullOrEmpty(Number.Text) || String.IsNullOrEmpty(Brand.Text))
            {
                var dialog = new ContentDialog()
                {
                    Title = "Ошибка при заполнении формы",
                    IsPrimaryButtonEnabled = true,
                    PrimaryButtonText = "Ok"
                };
                //dialog.ShowAsync();
                return false;
            }
            return true;
        }


        // Кнопка "Добавить"
        private async void Add(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                var car = new Car
                {
                    car_number = Number.Text,
                    brand = Brand.Text,
                    transmission = Transmission_ComboBox.SelectedIndex != 0
                };
                await CarService.Add(car);
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();
            }
        }


        //Кнопка "Изменить"
        private async void Update(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                var car = new Car
                {
                    id = id,
                    car_number = Number.Text,
                    brand = Brand.Text,
                    transmission = Transmission_ComboBox.SelectedIndex != 0
                };
                await CarService.Update(car);

                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();

            }
        }

        // Кнопка "Отменить"
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }

        private void BackCarList(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(CarList));
        }

    }
}

