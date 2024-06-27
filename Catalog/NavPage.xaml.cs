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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Catalog
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NavPage : Page
    {
        public NavPage()
        {
            this.InitializeComponent();
            Admin_MainPage.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(AdminMainPage));
        }


        private void Admin_MainPage_Click(object sender, RoutedEventArgs e)
        {
            Admin_Instructor.Style = null;
            Admin_Classes.Style = null;
            Admin_Student.Style = null;
            Admin_Car.Style = null;
            Admin_MainPage.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(AdminMainPage));
        }

        private void Admin_Classes_Click(object sender, RoutedEventArgs e)
        {
            Admin_Instructor.Style = null;
            Admin_MainPage.Style = null;
            Admin_Student.Style = null;
            Admin_Car.Style = null;
            Admin_Classes.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(AdminClasses));
        }

        private void Admin_Student_Click(object sender, RoutedEventArgs e)
        {
            Admin_Instructor.Style = null;
            Admin_MainPage.Style = null;
            Admin_Classes.Style = null;
            Admin_Car.Style = null;
            Admin_Student.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(StudentList));

        }

        private void Admin_Instructor_Click(object sender, RoutedEventArgs e)
        {
            Admin_Student.Style = null;
            Admin_MainPage.Style = null;
            Admin_Classes.Style = null;
            Admin_Car.Style = null;
            Admin_Instructor.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(InstructorList));

        }

        private void Admin_Car_Click(object sender, RoutedEventArgs e)
        {
            Admin_Student.Style = null;
            Admin_MainPage.Style = null;
            Admin_Classes.Style = null;
            Admin_Instructor.Style = null;
            Admin_Car.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(CarList));
        }
    }
}
