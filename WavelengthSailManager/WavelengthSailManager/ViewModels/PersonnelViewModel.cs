using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WavelengthSailManager.Models;

namespace WavelengthSailManager.ViewModels
{
    class PersonnelViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Sailor> personnelList;

        public event PropertyChangedEventHandler PropertyChanged;

        public PersonnelViewModel()
        {
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;

                // Keep as a list for sorting 
                List<Sailor> linqList = await @interface.GetSailorsAsync();

                // Set to observable collection for display.
                PersonnelList = new ObservableCollection<Sailor>(linqList.OrderBy(i => i.Sailor_Name));
            });
        }

        // Getter and setter for personnel list
        public ObservableCollection<Sailor> PersonnelList
        {
            set
            {
                if (personnelList != value)
                {
                    personnelList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("PersonnelList"));
                    }
                }
            }
            get
            {
                return personnelList;
            }
        }
    }
}