using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    public class ClasessDateTimeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ClassTemplate { get; set; }
        public DataTemplate DateTimeTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is Classes)
            {
                return ClassTemplate;
            }
            else if (item is DateTime)
            {
                return DateTimeTemplate;
            }
            return base.SelectTemplateCore(item, container);
        }
    }

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class UserClasses : Page
    {
        private int? id;

        public UserClasses()
        {
            this.InitializeComponent();
        }

        private void BackMainPage(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(NavPageStudent));
        }

        private DateTime date = DateTime.Now;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var month = DateTimeFormatInfo.GetInstance(CultureInfo.CurrentCulture).GetMonthName(date.Month);
            HeaderMonthUC.Text = month;
            GenerateWeekButtons();
            //this.ClassesListBox.ItemsSource = await ClassesService.GetAll(date);
        }

        private void GenerateWeekButtons()
        {
            WeekDays.Children.Clear();
            // Get the current date and time
            DateTime now = date;
            // Get the start of the week (assuming Monday as the first day of the week)
            DateTime startOfWeek = now.Date.AddDays(-((int)now.DayOfWeek == 0 ? 6 : (int)now.DayOfWeek - 1));


            // Generate day panels for each day of the week
            for (int i = 0; i < 7; i++)
            {

                // Create a StackPanel to hold the day panels
                StackPanel weekPanel = new StackPanel();
                weekPanel.Orientation = Orientation.Vertical;

                DateTime day = startOfWeek.AddDays(i);

                // Create a StackPanel for the day
                StackPanel dayPanel = new StackPanel();
                dayPanel.Orientation = Orientation.Vertical;
                dayPanel.Margin = new Thickness(10);

                // Create a Button for the day
                Button button = new Button
                {
                    Content = day.ToString("dd"),
                    Margin = new Thickness(0, 0, 0, 5)
                };
                button.Click += SelectDate;
                button.CommandParameter = day;

                // Create a TextBlock for the day of the week name
                TextBlock textBlock = new TextBlock
                {
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(day.DayOfWeek),
                    TextAlignment = TextAlignment.Center
                };

                // Add Button and TextBlock to the day panel
                dayPanel.Children.Add(button);
                dayPanel.Children.Add(textBlock);

                // Add the day panel to the week panel
                weekPanel.Children.Add(dayPanel);

                // Add the week panel to the root layout panel (e.g., a Grid)
                WeekDays.Children.Add(weekPanel);
            }

        }

        private async void SelectDate(object sender, RoutedEventArgs e)
        {
            date = (DateTime)(sender as Button).CommandParameter;
            var classes = await ClassesService.GetAll(date);

            // Create an ObservableCollection to hold both Classes and DateTime objects
            var collection = new ObservableCollection<object>();

            // Define the start and end times of the workday
            DateTime startTime = date.Date.AddHours(10); // 8:00 AM
            DateTime endTime = date.Date.AddHours(20); // 6:00 PM

            // Iterate over the workday in 2-hour intervals
            for (DateTime current = startTime; current < endTime; current = current.AddHours(2))
            {
                // Check if there is a class at the current time slot
                var existingClass = classes.FirstOrDefault(c =>
                {
                    var datee = DateTime.Parse(c.date, null, DateTimeStyles.RoundtripKind).ToLocalTime().TimeOfDay;
                    var currentDatee = current.TimeOfDay;
                    return datee == currentDatee;
                });

                if (existingClass != null)
                {
                    // Add the existing class to the collection
                    collection.Add(existingClass);
                }
                else
                {
                    // Add a DateTime object to the collection indicating an empty slot
                    collection.Add(current);
                }
            }

            // Set the ItemsSource of the ListBox to the created collection
            ClassesListBox.ItemsSource = collection;
        }

        private void NextWeek(object sender, RoutedEventArgs e)
        {
            date = GetNextWeekMonday(date);
            var month = DateTimeFormatInfo.GetInstance(CultureInfo.CurrentCulture).GetMonthName(date.Month);
            HeaderMonthUC.Text = month;
            GenerateWeekButtons();
        }

        private void PrevWeek(object sender, RoutedEventArgs e)
        {
            date = GetPreviousWeekMonday(date);
            var month = DateTimeFormatInfo.GetInstance(CultureInfo.CurrentCulture).GetMonthName(date.Month);
            HeaderMonthUC.Text = month;
            GenerateWeekButtons();
        }

        public static DateTime GetPreviousWeekMonday(DateTime date)
        {
            int daysToSubtract = (int)date.DayOfWeek - (int)DayOfWeek.Monday;
            if (daysToSubtract < 0)
            {
                daysToSubtract += 7;
            }
            DateTime previousWeek = date.AddDays(-daysToSubtract - 7);
            return previousWeek;
        }

        public static DateTime GetNextWeekMonday(DateTime date)
        {
            int daysToAdd = (int)DayOfWeek.Monday - (int)date.DayOfWeek;
            if (daysToAdd <= 0)
            {
                daysToAdd += 7;
            }
            DateTime nextWeek = date.AddDays(daysToAdd);
            return nextWeek;
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            var frm = Window.Current.Content as Frame;
            frm.Navigate(typeof(UserSignUp), (sender as Button).DataContext/*ПЕРЕДАВАТЬ СТУДЕНТА*/);
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            var classes = (sender as Button).DataContext as Classes;
            await ClassesService.Delete(classes);

            this.ClassesListBox.ItemsSource = await ClassesService.GetAll(date);
        }
    }
}
