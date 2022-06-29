using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    class ResultsViewerViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Series> seriesList;

        ObservableCollection<Race> raceList;

        public event PropertyChangedEventHandler PropertyChanged;

        public ResultsViewerViewModel()
        {
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                SeriesList = new ObservableCollection<Series>(await @interface.GetSeriesAsync());
                RaceList = new ObservableCollection<Race>(await @interface.GetRaceAsync());
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