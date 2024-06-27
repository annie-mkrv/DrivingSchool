using Catalog.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class AddClass : Page
    {
        private int? id;
        public AddClass()
        {
            this.InitializeComponent();
        }
        private void BackAdminClasses(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(AdminClasses));
        }

        private void InstructorId_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InstructorId_ComboBox.SelectedValue != null)
            {
                InstructorId_ComboBox.Text = InstructorId_ComboBox.SelectedValue.ToString();
            }
        }

        private void StudentId_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudentId_ComboBox.SelectedValue != null)
            {
                StudentId_ComboBox.Text = StudentId_ComboBox.SelectedValue.ToString();
            }
        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var instructors = await InstructorService.getAll();
            var students = await StudentService.getAll();

            InstructorId_ComboBox.ItemsSource = instructors;
            StudentId_ComboBox.ItemsSource = students;
            //if (e.parameter == null)
            //{
            //    updatebutton.visibility = visibility.collapsed;
            //    updateheader.visibility = visibility.collapsed;
            //    backarrowupdate.visibility = visibility.collapsed;
            //}
            //else
            //{
            //    var instructor = e.parameter as instructor;

            //    addbutton.visibility = visibility.collapsed;
            //    addheader.visibility = visibility.collapsed;
            //    backarrowadd.visibility = visibility.collapsed;

            //    updateheader.text = $"редактирование инструктора №{instructor.id}";

            //    id = instructor.id;

            //    fio.text = instructor.fio;
            //    phone.text = instructor.phone;

            //InstructorId_ComboBox.SelectedItem = instructors.Where(x => x.id == classes.instructorId).First();
            //}
        }


        // Проверка валидации
        public bool Validate()
        {
            //if (String.IsNullOrEmpty(Fio.Text) || String.IsNullOrEmpty(Phone.Text))
            //{
            //    var dialog = new ContentDialog()
            //    {
            //        Title = "Ошибка при заполнении формы",
            //        IsPrimaryButtonEnabled = true,
            //        PrimaryButtonText = "Ok"
            //    };
            //    //dialog.ShowAsync();
            //    return false;
            //}

            return true;
        }


        // Кнопка "Добавить"
        private async void Add(object sender, RoutedEventArgs e)
        {
            //    if (Validate())
            //    {
            //        var classes = new Classes
            //        {
            //            Date = Date.text,
            //            InstructorId = (InstructorId_ComboBox.SelectedItem as Instructor).id ?? 1,
            //            StudentId = (StudentId_ComboBox.SelectedItem as Student).id ?? 1
            //        };
            //        await ClassesService.Add(classes);
            //        Frame rootframe = Window.Current.Content as Frame;
            //        rootframe.GoBack();
            //    }
        }


    //Кнопка "Изменить"
    //private async void Update(object sender, RoutedEventArgs e)
    //{
    //    if (Validate())
    //    {
    //        var instructor = new Instructor
    //        {
    //            id = id,
    //            fio = Fio.Text,
    //            phone = Phone.Text,
    //            carId = (CarId_ComboBox.SelectedItem as Car).id ?? 1
    //        };
    //        await InstructorService.Update(instructor);

    //        Frame rootFrame = Window.Current.Content as Frame;
    //        rootFrame.GoBack();

    //    }
    //}


    // Кнопка "Отменить"
    private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }

    }
}
