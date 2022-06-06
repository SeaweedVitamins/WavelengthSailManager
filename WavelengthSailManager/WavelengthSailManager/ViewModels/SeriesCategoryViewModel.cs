using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    class SeriesCategoryViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Series> seriesList;

        ObservableCollection<Category> categoryList;

        public event PropertyChangedEventHandler PropertyChanged;

        public SeriesCategoryViewModel()
        {
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                SeriesList = new ObservableCollection<Series>(await @interface.GetSeriesAsync());
                CategoryList = new ObservableCollection<Category>(await @interface.GetCategoriesAsync());
            });
        }

        public ObservableCollection<Series> SeriesList
        {
            set
            {
                if (seriesList != value)
                {
                    seriesList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SeriesList"));
                    }
                }
            }
            get
            {
                return seriesList;
            }
        }

        public ObservableCollection<Category> CategoryList
        {
            set
            {
                if (categoryList != value)
                {
                    categoryList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CategoryList"));
                    }
                }
            }
            get
            {
                return categoryList;
            }
        }
    }
}