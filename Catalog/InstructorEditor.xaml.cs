//АДМИНИСТРАТОРЫ - СОЗДАНИЕ/РЕДАКТИРОВАНИЕ

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.WindowsRuntime;
using Catalog.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class InstructorEditor : Page
    {
        private int? id;
        public InstructorEditor()
        {
            this.InitializeComponent();
        }

        private void CarId_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarId_ComboBox.SelectedValue != null)
            {
                CarId_ComboBox.Text = CarId_ComboBox.SelectedValue.ToString();
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var cars = await CarService.getAll();

            CarId_ComboBox.ItemsSource = cars;
            if (e.Parameter == null)
            {
                UpdateButton.Visibility = Visibility.Collapsed;
                UpdateHeader.Visibility = Visibility.Collapsed;
                BackArrowUpdate.Visibility = Visibility.Collapsed;
            }
            else
            {
                var instructor = e.Parameter as Instructor;

                AddButton.Visibility = Visibility.Collapsed;
                AddHeader.Visibility = Visibility.Collapsed;
                BackArrowAdd.Visibility = Visibility.Collapsed;

                UpdateHeader.Text = $"Редактирование инструктора №{instructor.id}";

                id = instructor.id;

                Fio.Text = instructor.fio;
                Phone.Text = instructor.phone;


                CarId_ComboBox.SelectedItem = cars.Where(x => x.id == instructor.carId).First();
            }
        }


        // Проверка валидации
        public bool Validate()
        {
            if (String.IsNullOrEmpty(Fio.Text) || String.IsNullOrEmpty(Phone.Text))
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
                var instructor = new Instructor
                {
                    fio = Fio.Text,
                    phone = Phone.Text,
                    carId = (CarId_ComboBox.SelectedItem as Car).id ?? 1
            };
                await InstructorService.Add(instructor);
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();
            }
        }


        //Кнопка "Изменить"
        private async void Update(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                var instructor = new Instructor
                {
                    id = id,
                    fio = Fio.Text,
                    phone = Phone.Text,
                    carId = (CarId_ComboBox.SelectedItem as Car).id ?? 1
                };
                await InstructorService.Update(instructor);

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

        private void BackInstructorList(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(InstructorList));
        }
    }
}
