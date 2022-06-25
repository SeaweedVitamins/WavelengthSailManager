using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    class PYViewModel : INotifyPropertyChanged
    {
        ObservableCollection<PY> pyList;

        public event PropertyChangedEventHandler PropertyChanged;

        public PYViewModel()
        {
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                PYList = new ObservableCollection<PY>(await @interface.GetPYsAsync());
            });
        }

        public ObservableCollection<PY> PYList
        {
            set
            {
                if (pyList != value)
                {
                    pyList = value;

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