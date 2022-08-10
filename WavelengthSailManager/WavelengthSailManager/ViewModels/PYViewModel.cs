using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WavelengthSailManager.Models;

namespace WavelengthSailManager.ViewModels
{
    class PYViewModel : INotifyPropertyChanged
    {
        ObservableCollection<PY> pyList;

        public event PropertyChangedEventHandler PropertyChanged;

        public PYViewModel()
        {
            // Run async to avoid blocker
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;

                // On retrieval set to bindable object
                PYList = new ObservableCollection<PY>(await @interface.GetPYsAsync());
            });
        }

        // Getter and setter for the bindable object
        public ObservableCollection<PY> PYList
        {
            set
            {
                if (pyList != value)
                {
                    pyList = value;

                    // Update property changed notifier if new value set
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("PYList"));
                    }
                }
            }
            get
            {
                return pyList;
            }
        }
    }
}