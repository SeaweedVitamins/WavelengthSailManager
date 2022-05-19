using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    class BoatParkViewModel : INotifyPropertyChanged
    {
        ObservableCollection<BoatSailor> raceList;

        public event PropertyChangedEventHandler PropertyChanged;

        public BoatParkViewModel()
        {
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                BoatList = new ObservableCollection<BoatSailor>(await @interface.GetBoatListAsync());
            });
        }

        public ObservableCollection<BoatSailor> BoatList
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
