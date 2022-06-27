using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WavelengthSailManager.Models;
using WavelengthSailManager.Views;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    class BoatParkViewModel : INotifyPropertyChanged
    {
        ObservableCollection<BoatSailor> boatList;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand DeleteCommand { get; }

        public ICommand EditCommand { get; }

        public BoatParkViewModel()
        {
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                BoatList = new ObservableCollection<BoatSailor>(await @interface.GetBoatListAsync());
            });

            DeleteCommand = new Command((object context) => {
                BoatSailor BoatSelected = (BoatSailor)context;
                Task.Run(async () =>
                {
                    DatabaseInterface @interface = await DatabaseInterface.Instance;
                    Boat selected = await @interface.GetBoatAsync(BoatSelected.ID);
                    await @interface.DeleteBoatAsync(selected);
                });
            });

            EditCommand = new Command((object context) => {
                BoatSailor BoatSelected = (BoatSailor)context;
                App.Current.MainPage = new EditBoat(BoatSelected);
            });
        }

        public ObservableCollection<BoatSailor> BoatList
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
