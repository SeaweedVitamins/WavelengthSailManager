using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
                // Get data for the races
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                List<Race> LinqList = await @interface.GetTodaysRacesAsync();

                //Set to a collection
                ObservableCollection<Race> raceCollection = new ObservableCollection<Race>();

                // Checks if the race belongs to the current date
                foreach(Race r in LinqList)
                {
                    if( r.DateTime.ToString("dd:MM") == DateTime.Now.ToString("dd:MM"))
                    {
                        raceCollection.Add(r);
                    }
                }
                RaceList = new ObservableCollection<Race>(raceCollection.OrderBy(i => i.DateTime));
            });
        }

        // Get and set for the RaceList
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