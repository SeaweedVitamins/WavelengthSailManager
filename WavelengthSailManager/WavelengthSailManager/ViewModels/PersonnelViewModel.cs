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
    class PersonnelViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Sailor> personnelList;

        public event PropertyChangedEventHandler PropertyChanged;

        public PersonnelViewModel()
        {
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                List<Sailor> linqList = await @interface.GetSailorsAsync();
                PersonnelList = new ObservableCollection<Sailor>(linqList.OrderBy(i => i.Sailor_Name));
            });
        }

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