using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            var password = Password.Password;
            var frm = Window.Current.Content as Frame;

            if (string.IsNullOrEmpty(Username.Text))
            {
                Error.Text = "Введите имя пользователя";
                return;
            }
            else if (string.IsNullOrEmpty(Password.Password))
            {
                Error.Text = "Введите пароль";
                return;
            }
            else
            {
                Error.Text = "";
            }

            try
            {
                var profileStatus = await Auth.SignIn(Username.Text, password);

                if (profileStatus)
                {
                    if (Auth.UserAdmin.id == 1)
                    {
                        frm.Navigate(typeof(NavPageBossAdmin));
                    }
                    else
                    {
                        frm.Navigate(typeof(NavPage));
                    }
                }
                else
                {
                    frm.Navigate(typeof(NavPageStudent));
                }
            }
            catch (Exception)
            {
                Error.Text = "";
            }


        }

    }
}
