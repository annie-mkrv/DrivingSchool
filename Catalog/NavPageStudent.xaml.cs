using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace Catalog
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NavPageStudent : Page
    {
        public NavPageStudent()
        {
            this.InitializeComponent();
            Student_MainPage.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(MainPage));
        }


        private void Student_MainPage_Click(object sender, RoutedEventArgs e)
        {
            Student_SignUp.Style = null;
            Student_MyClasses.Style = null;
            Student_MainPage.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(MainPage));
        }

        private void Student_SignUp_Click(object sender, RoutedEventArgs e)
        {
            Student_MyClasses.Style = null;
            Student_MainPage.Style = null;
            Student_SignUp.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(UserClasses));
        }

        private void Student_MyClasses_Click(object sender, RoutedEventArgs e)
        {
            Student_MainPage.Style = null;
            Student_SignUp.Style = null;
            Student_MyClasses.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(UserMyClasses));

        }
    }
}

