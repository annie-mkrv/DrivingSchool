using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Catalog.Services;
using Windows.UI.Xaml.Navigation;
using System.Numerics;

namespace Catalog
{
    public sealed partial class UserSignUp : Page
    {
        private DateTime date;
        public UserSignUp()
        {
            this.InitializeComponent();

            Student student = Auth.UserStudent;
            Fio.Text = student.fio;
        }

        private void BackUserClasses(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(UserClasses));
        }

        private void InstructorId_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InstructorId_ComboBox.SelectedValue != null)
            {
                InstructorId_ComboBox.Text = InstructorId_ComboBox.SelectedValue.ToString();
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.date = (DateTime)e.Parameter;
            this.DateTime.Text  =date.ToString("dd.MM.yyyy HH:mm");

            var instructors = await InstructorService.getAll();

            InstructorId_ComboBox.ItemsSource = instructors;
        }

        // Кнопка "Добавить"
        private async void Add(object sender, RoutedEventArgs e)
        {
            var classes = new Classes
            {
                studentId = Auth.UserStudent.id ?? 0,
                date = date.ToString("o"),
                //fio = Fio.Text,
                //phone = Phone.Text,
                instructorId = (InstructorId_ComboBox.SelectedItem as Instructor).id ?? 1
            };
            await ClassesService.Add(classes);
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }

    }
}
