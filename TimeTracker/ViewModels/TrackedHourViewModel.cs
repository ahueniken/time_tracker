using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TimeTracker.Models;

namespace TimeTracker.ViewModels
{
    public class TrackedHourViewModel : INotifyPropertyChanged
    {
        private TrackedHourDataContext trackedHourDB;

        public TrackedHourViewModel(string trackedHourDBConnectionString)
        {
            trackedHourDB = new TrackedHourDataContext(trackedHourDBConnectionString);
        }

        public void SaveChangesToDB()
        {
            trackedHourDB.SubmitChanges();
        }

        // All tracked hour items.
        private ObservableCollection<TrackedHourItem> _allTrackedHourItems;
        public ObservableCollection<TrackedHourItem> AllTrackedHourItems
        {
            get { return _allTrackedHourItems; }
            set
            {
                _allTrackedHourItems = value;
                NotifyPropertyChanged("_allTrackedHourItems");
            }
        }

        // A list of all categories, used by the add task page.
        private List<Category> _categoriesList;
        public List<Category> CategoriesList
        {
            get { return _categoriesList; }
            set
            {
                _categoriesList = value;
                NotifyPropertyChanged("CategoriesList");
            }
        }

        // Query database and load the collections and list used by the pivot pages.
        public void LoadCollectionsFromDatabase()
        {

            // Specify the query for all to-do items in the database.
            var trackedHoursInDB = from TrackedHourItem hour in trackedHourDB.Items
                                select hour;

            // Query the database and load all to-do items.
            AllTrackedHourItems = new ObservableCollection<TrackedHourItem>(trackedHoursInDB);

            // Specify the query for all categories in the database.
            var CategoriesInDB = from Category category in trackedHourDB.Categories
                                     select category;

            // Load a list of all categories.
            CategoriesList = trackedHourDB.Categories.ToList();
        }

        // Add a trackedHourItem to the database and collections.
        public void AddTrackedHourItem(TrackedHourItem newTrackedHourItem)
        {
            // Add a to-do item to the data context.
            trackedHourDB.Items.InsertOnSubmit(newTrackedHourItem);

            // Save changes to the database.
            trackedHourDB.SubmitChanges();

            // Add a to-do item to the "all" observable collection.
            AllTrackedHourItems.Add(newTrackedHourItem);
        }

        // Remove a trackedHourItem task item from the database and collections.
        public void DeleteToDoItem(TrackedHourItem trackedHourForDelete)
        {

            // Remove the to-do item from the "all" observable collection.
            AllTrackedHourItems.Remove(trackedHourForDelete);

            // Remove the to-do item from the data context.
            trackedHourDB.Items.DeleteOnSubmit(trackedHourForDelete);

            // Save changes to the database.
            trackedHourDB.SubmitChanges();
        }

        public void AddCategory(Category newCategory)
        {
            trackedHourDB.Categories.InsertOnSubmit(newCategory);
            trackedHourDB.SubmitChanges();
            CategoriesList.Add(newCategory);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
