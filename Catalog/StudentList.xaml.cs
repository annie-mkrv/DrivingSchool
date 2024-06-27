using Catalog.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
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
    public sealed partial class StudentList : Page
    {

        private int? id;
        public StudentList() 
        { 
            this.InitializeComponent();

            Student student = Auth.UserStudent;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.StudentListBox.ItemsSource = await StudentService.getAll();
        }
        private void BackAdminMainPage(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(NavPage));
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            
            frm.Navigate(typeof(StudentEditor) );
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(StudentEditor), (sender as Button).DataContext/*ПЕРЕДАВАТЬ СТУДЕНТА*/);
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            var student = (sender as Button).DataContext as Student;
            await StudentService.Delete(student);

            this.StudentListBox.ItemsSource = await StudentService.getAll();
        }



    }
}
