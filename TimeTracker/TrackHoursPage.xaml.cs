using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TimeTracker.Models;
using TimeTracker.ViewModels;

namespace TimeTracker
{
    public partial class TrackHoursPage : PhoneApplicationPage
    {
        Category _category;

        public TrackHoursPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        private void appBarOkButton_Click(object sender, EventArgs e)
        {
            TrackedHourItem newTrackedHour = new TrackedHourItem
            {
                Category = (Category)categoryListPicker.SelectedItem,
                Hour = ((DateTime)datePicker.Value).AddHours((Double)HoursPicker.SelectedItem)
            };
            App.ViewModel.AddTrackedHourItem(newTrackedHour);
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
           
        }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}