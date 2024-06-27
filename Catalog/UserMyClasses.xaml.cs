using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
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
    public sealed partial class UserMyClasses : Page
    {
        private int? id;
        
        private DateTime date = DateTime.Now;
        public UserMyClasses()
        {
            this.InitializeComponent();
            Student student = Auth.UserStudent;
        }

        private void BackMainPage(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(NavPageStudent));
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Student student = Auth.UserStudent;
            this.ClassesListBox.ItemsSource = await ClassesService.GetForStudent(student);
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            Student student = Auth.UserStudent;
            var classes = (sender as Button).DataContext as Classes;
            await ClassesService.Delete(classes);

            this.ClassesListBox.ItemsSource = await ClassesService.GetAll(date);
            this.ClassesListBox.ItemsSource = await ClassesService.GetForStudent(student);
        }
    }
}
