using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    class TodaysRacesViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Race> raceList;

        public event PropertyChangedEventHandler PropertyChanged;

        public TodaysRacesViewModel()
        {
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                RaceList = new ObservableCollection<Race>(await @interface.GetTodaysRacesAsync());
            });
        }

        public ObservableCollection<Race> RaceList
        {
            set
            {
                if (raceList != value)
                {
                    raceList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("RaceList"));
                    }
                }
            }
            get
            {
                return raceList;
            }
        }
    }
}