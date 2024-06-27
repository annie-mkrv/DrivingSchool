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

//Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace Catalog
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class BossAdminEditor : Page
    {
        private int? id;
        public BossAdminEditor()
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


                ChangePasswordCheck.Visibility = Visibility.Collapsed;
                ChangePasswordCheck.IsChecked = true;
            }
            else
            {
                var admin = e.Parameter as Admin;

                AddButton.Visibility = Visibility.Collapsed;
                AddHeader.Visibility = Visibility.Collapsed;
                BackArrowAdd.Visibility = Visibility.Collapsed;

                UpdateHeader.Text = $"Редактирование администратора №{admin.id}";

                id = admin.id;

                Fio.Text = admin.fio;
                Username.Text = admin.username;
                Phone.Text = admin.phone;
                Passport.Text = admin.passport;
            }
        }

        // Проверка валидации
        public bool Validate()
        {
            if (String.IsNullOrEmpty(Fio.Text) || String.IsNullOrEmpty(Username.Text) || String.IsNullOrEmpty(Phone.Text) || String.IsNullOrEmpty(Passport.Text))
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

            if (!ChangePasswordCheck.IsChecked ?? true)
            {
                return true;
            }

            if (Password.Password != PasswordRepeat.Password)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Пароли не совпадают",
                    IsPrimaryButtonEnabled = false
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
                var admin = new Admin
                {
                    fio = Fio.Text,
                    username = Username.Text,
                    phone = Phone.Text,
                    passport = Passport.Text,
                    password = Password.Password
                };
                await AdminService.Add(admin);
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();
            }
        }


        //Кнопка "Изменить"
        private async void Update(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                var admin = new Admin
                {
                    id = id,
                    fio = Fio.Text,
                    username = Username.Text,
                    phone = Phone.Text,
                    passport = Passport.Text,
                };
                if (ChangePasswordCheck.IsChecked ?? true)
                {
                    admin.password = Password.Password;
                }
                await AdminService.Update(admin);

                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();

            }
        }


        //Кнопка "Отменить"
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }

        private void ChangePasswordCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Password_Grid.Visibility = Visibility.Collapsed;
        }

        private void ChangePasswordCheck_Checked(object sender, RoutedEventArgs e)
        {
            Password_Grid.Visibility = Visibility.Visible;

        }

        private void BackBossAdminList(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(BossAdminList));
        }

    }
}

