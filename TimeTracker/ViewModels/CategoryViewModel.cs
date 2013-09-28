using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Collections.ObjectModel;

namespace TimeTracker.ViewModels
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private string _category;
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                if (value != _category)
                {
                    _category = value;
                    NotifyPropertyChanged("Category");
                }
            }
        }

        private int _hours;
        public int Hours
        {
            get { return _hours; }
            set
            {
                if (value != _hours)
                {
                    _hours = value;
                    NotifyPropertyChanged("Hours");
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}