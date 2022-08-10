using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    class RaceManagementViewModel : INotifyPropertyChanged
    {
        ObservableCollection<RaceManagementModel> raceManagementList;

        public event PropertyChangedEventHandler PropertyChanged;

        public RaceManagementViewModel()
        {
            Task.Run(async () =>
            {
                // Getting each of the races to display on management screen
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                RaceManagementList = new ObservableCollection<RaceManagementModel>(
                    await @interface.GetRaceManagementListAsync());
            });
        }

        // Getter and Setter for the management list
        public ObservableCollection<RaceManagementModel> RaceManagementList
        {
            set
            {
                if (raceManagementList != value)
                {
                    raceManagementList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("RaceManagementList"));
                    }
                }
            }
            get
            {
                return raceManagementList;
            }
        }
    }
}
