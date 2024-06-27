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
    public sealed partial class BossAdminList : Page
    {
        private int? id;
        public BossAdminList()
        {
            this.InitializeComponent();
            Admin admin = Auth.UserAdmin;
        }

        private void BackBossAdminPage(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(NavPageBossAdmin));
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.AdminListBox.ItemsSource = await AdminService.getAll();
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;

            frm.Navigate(typeof(BossAdminEditor));
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(BossAdminEditor), (sender as Button).DataContext/*ПЕРЕДАВАТЬ АДМИНА*/);
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            var admin = (sender as Button).DataContext as Admin;
            await AdminService.Delete(admin);

            this.AdminListBox.ItemsSource = await AdminService.getAll();
        }
    }
}
