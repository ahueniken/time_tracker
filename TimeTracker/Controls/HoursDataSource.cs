using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls.Primitives;
using System.Windows.Controls;

namespace TimeTracker.Controls
{
    internal class HoursDataSource : ILoopingSelectorDataSource
    {
        public HoursDataSource(int minValue, int maxValue, int step, int defaultValue, string stringFormat)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Step = step;
            SelectedItem = defaultValue;
            StringFormat = stringFormat;
        }

        public int MinValue
        {
            get;
            set;
        }

        public int MaxValue
        {
            get;
            set;
        }

        public int Step
        {
            get;
            set;
        }

        private string ApplyFormat(int digit)
        {
            return digit.ToString(StringFormat);
        }

        #region ILoopingSelectorDataSource Members

        public object GetNext(object relativeTo)
        {
            return Convert.ToInt32(relativeTo) + Step > MaxValue ? ApplyFormat(MinValue) : ApplyFormat(Convert.ToInt32(relativeTo) + Step);
        }

        public object GetPrevious(object relativeTo)
        {
            return Convert.ToInt32(relativeTo) - Step < MinValue ? ApplyFormat(MaxValue) : ApplyFormat(Convert.ToInt32(relativeTo) - Step);
        }

        public string StringFormat
        {
            get;
            private set;
        }

        public int selectedItem;
        public object SelectedItem
        {
            get
            {
                return ApplyFormat(selectedItem);
            }
            set
            {
                int newValue = Convert.ToInt32(value);
                if (selectedItem != newValue)
                {
                    int previousSelectedItem = selectedItem;
                    selectedItem = newValue;
                    EventHandler<SelectionChangedEventArgs> handler = SelectionChanged;
                    if (handler != null)
                        handler(this, new SelectionChangedEventArgs(new object[] { ApplyFormat(previousSelectedItem) }, new object[] { ApplyFormat(selectedItem) }));
                }
            }
        }

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        #endregion
    }
}

