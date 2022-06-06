using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    class RaceViewModel : INotifyPropertyChanged
    {
        int elapsedTime;

        ObservableCollection<Timing> timingList;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LapCommand { get; }

        public ICommand FinishCommand { get; }

        public RaceViewModel(List<int> competingBoatList, Race selectedRace)
        {
            this.ElapsedTime = 0;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                this.ElapsedTime++;
                
                return true;
            });

            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                var SpecialCollection = new ObservableCollection<SpecialValues>(await @interface.GetSpecialAsync());

                List<string> SpecialList = new List<string>();
                foreach(var x in SpecialCollection) { SpecialList.Add(x.Name); }

                List<Boat> tList = new List<Boat>(await @interface.GetTimingListAsync(competingBoatList));
                var timingLinqList = tList.ConvertAll(x => new Timing { ID = x.ID, Class_Name = x.Class_Name, Sail_Number = x.Sail_Number, Special_List = SpecialList, Lap_Time_List = new List<int>() });
                TimingList = new ObservableCollection<Timing>(timingLinqList);
            });

            LapCommand = new Command((object context) => {
                var BoatRacing = (Timing)context;
                BoatRacing.Lap_Time_List.Add(elapsedTime);
            });

            FinishCommand = new Command((object context) => {
                var BoatRacing = (Timing)context;
                BoatRacing.Finish_Time = elapsedTime;
                checkIfRaceOver(selectedRace);
            });
        }

        public void checkIfRaceOver(Race selectedRace)
        {
            bool finished = true;
            foreach(var x in timingList)
            {
                if (x.Finish_Time == 0 && x.Special_Classification_Assigned == null)
                {
                    finished = false;
                }
            }
            if(finished == true)
            {
                App.Current.MainPage = new RaceResults(timingList, selectedRace);
            }
        }

        public ObservableCollection<Timing> TimingList
        {
            set
            {
                if (timingList != value)
                {
                    timingList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TimingList"));
                    }
                }
            }
            get
            {
                return timingList;
            }
        }

        public int ElapsedTime
        {
            set
            {
                if (elapsedTime != value)
                {
                    elapsedTime = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TimeElapsed"));
                    }
                }
            }
            get
            {
                return elapsedTime;
            }
        }

        public string TimeElapsed
        {
            get
            {
                TimeSpan time = TimeSpan.FromSeconds(elapsedTime);
                return time.ToString(@"mm\:ss");
            }
        }
    }
}
