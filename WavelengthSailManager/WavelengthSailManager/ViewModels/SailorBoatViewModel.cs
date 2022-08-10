using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    class SailorBoatViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Sailor> sailorList;

        ObservableCollection<Boat> boatList;

        public event PropertyChangedEventHandler PropertyChanged;

        public SailorBoatViewModel()
        {
            Task.Run(async () =>
            {
                // The database interface pulls sailor and boat list
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                SailorList = new ObservableCollection<Sailor>(await @interface.GetSailorsAsync());
                BoatList = new ObservableCollection<Boat>(await @interface.GetBoatsAsync());
            });
        }

        /*
         * Getters and Setters for properties
         */
        public ObservableCollection<Sailor> SailorList
        {
            set
            {
                if (sailorList != value)
                {
                    sailorList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SailorList"));
                    }
                }
            }
            get
            {
                return sailorList;
            }
        }

        public ObservableCollection<Boat> BoatList
        {
            set
            {
                if (boatList != value)
                {
                    boatList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("BoatList"));
                    }
                }
            }
            get
            {
                return boatList;
            }
        }
    }
}