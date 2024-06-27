using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class AdminMainPage : Page
    {
        public AdminMainPage()
        {
            this.InitializeComponent();
        }

        private void OpenAdminLK(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(AdminLK));
        }

        private void OpenAdminClasses(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(AdminClasses));
        }

        private void OpenStudentList(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(StudentList));
        }

        private void OpenEmployeeList(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(InstructorList));
        }

        private void OpenCarList(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(CarList));
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(LoginPage));
        }
    }
}
