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
    public sealed partial class NavPageBossAdmin : Page
    {
        public NavPageBossAdmin()
        {
            this.InitializeComponent();
            BossAdmin_MainPage.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(BossAdminPage));
        }


        private void BossAdmin_MainPage_Click(object sender, RoutedEventArgs e)
        {
            BossAdmin_Admins.Style = null;
            BossAdmin_MainPage.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(BossAdminPage));
        }

        private void BossAdmin_Admins_Click(object sender, RoutedEventArgs e)
        {
            BossAdmin_MainPage.Style = null;
            BossAdmin_Admins.Style = (Style)Application.Current.Resources["AccentButtonStyle"];
            frx.Navigate(typeof(BossAdminList));
        }
    }
}

