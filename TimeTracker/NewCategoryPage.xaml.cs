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
    public partial class NewCategoryPage : PhoneApplicationPage
    {
        public NewCategoryPage()
        {
            InitializeComponent();
        }

        private void appBarOkButton_Click(object sender, EventArgs e)
        {
            // Confirm there is some text in the text box.
            if (newCategoryName.Text.Length > 0)
            {
                // Create a new to-do item.
                Category newCategory = new Category
                {
                    Name = newCategoryName.Text,
                    Count = 0,
                };

                // Add the item to the ViewModel.
                App.ViewModel.AddCategory(newCategory);

                // Return to the main page.
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
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