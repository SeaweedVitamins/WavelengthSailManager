using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WavelengthSailManager.Models;
using WavelengthSailManager.Views;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    class BoatParkViewModel : INotifyPropertyChanged
    {
        // Creating collection of BoatSailors
        ObservableCollection<BoatSailor> boatList;

        public event PropertyChangedEventHandler PropertyChanged;

        // Set command properties
        public ICommand DeleteCommand { get; }

        public ICommand EditCommand { get; }

        public BoatParkViewModel()
        {
            Task.Run(async () =>
            {
                // Get boat list from database
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                var linqList = await @interface.GetBoatListAsync();

                // Order the boats by sail number
                BoatList = new ObservableCollection<BoatSailor>(linqList.OrderBy(i => i.Sail_Number));
            });

            // Set up delete command
            DeleteCommand = new Command((object context) => {

                // Set delete context
                BoatSailor BoatSelected = (BoatSailor)context;
                Task.Run(async () =>
                {
                    // Run database command to remove boat from db using boat ID
                    DatabaseInterface @interface = await DatabaseInterface.Instance;
                    Boat selected = await @interface.GetBoatAsync(BoatSelected.ID);
                    await @interface.DeleteBoatAsync(selected);
                    var linqList = await @interface.GetBoatListAsync();
                    BoatList = new ObservableCollection<BoatSailor>(linqList.OrderBy(i => i.Sail_Number));
                });
            });

            // Create new edit command
            EditCommand = new Command((object context) => {

                // Set context recieved to be Boat Sailor
                BoatSailor BoatSelected = (BoatSailor)context;

                // Switch to edit page
                App.Current.MainPage = new EditBoat(BoatSelected);
            });
        }

        // BoatList property setter
        public ObservableCollection<BoatSailor> BoatList
        {
            set
            {
                if (boatList != value)
                {
                    boatList = value;

                    if (PropertyChanged != null)
                    {
                        // The Send out the property notification
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
