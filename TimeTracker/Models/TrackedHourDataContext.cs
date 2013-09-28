using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;

namespace TimeTracker.Models
{
    class TrackedHourDataContext : DataContext
    {
        // Pass the connection string to the base class.
        public TrackedHourDataContext(string connectionString) : base(connectionString)
        { }

        // Specify a table for the to-do items.
        public Table<TrackedHourItem> Items;

        // Specify a table for the categories.
        public Table<Category> Categories;
    }

    [Table]
    public class TrackedHourItem : INotifyPropertyChanged, INotifyPropertyChanging
    {

       // Define ID: private field, public property, and database column.
        private int _trackedHourId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int TrackedHourId
        {
            get {return _trackedHourId;}
            set
            {
                if (_trackedHourId != value)
                {
                    NotifyPropertyChanging("TrackedHourId");
                    _trackedHourId = value;
                    NotifyPropertyChanged("TrackedHourId");
                }
            }
        }

        private DateTime _hour;
        [Column]
        public DateTime Hour {
            get {return _hour;}
            set {
                if (_hour != value)
                {   NotifyPropertyChanging("Hour");
                    _hour = value;
                    NotifyPropertyChanged("Hour");
                }
            }
        }

         // Internal column for the associated ToDoCategory ID value
        [Column]
        internal int _categoryId;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<Category> _category;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "_category", ThisKey = "_categoryId", OtherKey = "Id", IsForeignKey = true)]
        public Category Category
        {
            get {return _category.Entity;}
            set
            {
                NotifyPropertyChanging("Category");
                _category.Entity = value;

                if (value != null)
                {
                    _categoryId = value.Id;
                }

                NotifyPropertyChanging("Category");
            }
        }



         // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    [Table]
    public class Category : INotifyPropertyChanged, INotifyPropertyChanging
    {

         // Define ID: private field, public property, and database column.
        private int _id;

        [Column(DbType = "INT NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id
        {
            get {return _id;}
            set
            {
                NotifyPropertyChanging("Id");
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        // Define category name: private field, public property, and database column.
        private string _name;

        [Column]
        public string Name
        {
            get {return _name;}
            set
            {
                NotifyPropertyChanging("Name");
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private int _count;
        [Column]
        public int Count
        { get {return _count;}
            set
            {
                NotifyPropertyChanging("Count");
                _count = value;
                NotifyPropertyChanged("Count");
            }
        }
         
        // Define the entity set for the collection side of the relationship.
        private EntitySet<TrackedHourItem> _trackedHours;

        [Association(Storage = "_trackedHours", OtherKey = "_categoryId", ThisKey = "Id")]
        public EntitySet<TrackedHourItem> TrackedHours
        {
            get { return this._trackedHours; }
            set { this._trackedHours.Assign(value); }
        }

         // Assign handlers for the add and remove operations, respectively.
        public Category()
        {
            _trackedHours = new EntitySet<TrackedHourItem>(
                new Action<TrackedHourItem>(this.attach_TrackedHour), 
                new Action<TrackedHourItem>(this.detach_TrackedHour)
                );
        }

        // Called during an add operation
        private void attach_TrackedHour(TrackedHourItem trackedHour)
        {
            NotifyPropertyChanging("TrackedHourItem");
            trackedHour.Category = this;
        }

        // Called during a remove operation
        private void detach_TrackedHour(TrackedHourItem trackedHour)
        {
            NotifyPropertyChanging("TrackedHourItem");
            trackedHour.Category = null;
        }


        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

}
