using Catalog.Services;
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
    public sealed partial class AdminLK : Page
    {
        private int? id;
        public AdminLK()
        {
            this.InitializeComponent();

            Admin admin = Auth.UserAdmin;
            Fio.Text = admin.fio;
            Username.Text = admin.username;
            Phone.Text = admin.phone;
            Passport.Text = admin.passport;
        }

        private async void BackAdminBossAdminPage(object sender, RoutedEventArgs e)
        {
            //var password = Password.Password;
            var frm = Window.Current.Content as Frame;

            //var profileStatus = Auth.UserAdmin;

            //if (profileStatus)
            //{
            //    if (Auth.UserAdmin.id == 1)
            //    {
            //        frm.Navigate(typeof(NavPageBossAdmin));
            //    }
            //    else
            //    {
            //        frm.Navigate(typeof(NavPage));
            //    }
            //}

            if (Auth.UserAdmin.id == 1)
            {
                frm.Navigate(typeof(NavPageBossAdmin));
            }
            else
            {
                frm.Navigate(typeof(NavPage));
            }
            //frm.Navigate(typeof(NavPage)); 
        }
    }
}
