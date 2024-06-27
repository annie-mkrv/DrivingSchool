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
    public sealed partial class LK : Page
    {
        public LK()
        {
            this.InitializeComponent();

            Student student = Auth.UserStudent;
            Fio.Text = student.fio;
            Username.Text = student.username;
            Phone.Text = student.phone;
        }

        private void BackMainPage(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(NavPageStudent));
        }
    }
}
