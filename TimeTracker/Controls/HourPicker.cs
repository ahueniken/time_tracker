using Microsoft.Phone.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TimeTracker.Controls;

namespace TimeTracker.Controls
{
    public class HourPicker : LoopingSelector
    {
        public HourPicker()
        {
            this.Loaded += (obj, args) =>
                {
                    DataSource = new HoursDataSource(MinValue, MaxValue, Step, DefaultValue, StringFormat);
                    DataSource.SelectionChanged += (obj1, arg1) =>
                  {
                      this.SelectedItem = Convert.ToInt32(arg1.AddedItems[0]);
                  };
                };
        }

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public int Step
        {
            get { return (int)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        public int SelectedItem
        {
            get { return (int)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public int DefaultValue
        {
            get { return (int)GetValue(DefaultValueProperty); }
            set { SetValue(DefaultValueProperty, value); }
        }

        public string StringFormat
        {
            get{return GetValue(StringFormatProperty).ToString();}
            set{SetValue(StringFormatProperty, value);}
        }

        public static readonly DependencyProperty MaxValueProperty =
                DependencyProperty.Register("MaxValue", typeof(int), typeof(HourPicker), new PropertyMetadata(24, ValueChanged));

        public static readonly DependencyProperty MinValueProperty =
                DependencyProperty.Register("MinValue", typeof(int), typeof(HourPicker), new PropertyMetadata(0, ValueChanged));

        public static readonly DependencyProperty StepProperty =
                DependencyProperty.Register("Step", typeof(int), typeof(HourPicker), new PropertyMetadata(1, ValueChanged));

        public static readonly DependencyProperty SelectedItemProperty =
                DependencyProperty.Register("SelectedItem", typeof(int), typeof(HourPicker)
                , new PropertyMetadata(new PropertyChangedCallback((sender, e) =>
                {
                    HourPicker _this = (HourPicker)sender;
                    _this.DataSource.SelectedItem = e.NewValue;
                })));

        public static readonly DependencyProperty DefaultValueProperty =
                DependencyProperty.Register("DefaultValue", typeof(int), typeof(HourPicker), new PropertyMetadata(12, ValueChanged));

        public static readonly DependencyProperty StringFormatProperty =
                DependencyProperty.Register("StringFormat", typeof(string), typeof(HourPicker), new PropertyMetadata(string.Empty, ValueChanged));

        private static void ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            HourPicker picker = (HourPicker)obj;
            picker.DataSource = new HoursDataSource(picker.MinValue, picker.MaxValue, picker.Step, picker.DefaultValue, picker.StringFormat);
        }
    }
}
