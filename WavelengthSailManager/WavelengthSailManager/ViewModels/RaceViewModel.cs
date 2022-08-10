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
            // Set time to 0
            this.ElapsedTime = 0;

            // Timer that triggers evey second
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                // Increment elapsed time
                this.ElapsedTime++;
                
                return true;
            });

            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                var SpecialCollection = new ObservableCollection<SpecialValues>(
                    await @interface.GetSpecialAsync());

                // Create list for special classification
                List<string> SpecialList = new List<string>();

                // Add to picker
                foreach(var x in SpecialCollection) { SpecialList.Add(x.Name); }

                // Get list of competing boats
                List<Boat> tList = new List<Boat>(await @interface.GetTimingListAsync(
                    competingBoatList));

                // Convert to Timing object collection
                var timingLinqList = tList.ConvertAll(x => new Timing { ID = x.ID,
                    Class_Name = x.Class_Name, Sail_Number = x.Sail_Number,
                    Special_List = SpecialList, NumberOfLaps = "Lap - 0",
                    Lap_Time_List = new List<int>() });

                //Set to display property
                TimingList = new ObservableCollection<Timing>(timingLinqList);
            });

            // Lap command triggered by button
            LapCommand = new Command((object context) => {
                var BoatRacing = (Timing)context;

                // Adds lap time to list
                BoatRacing.Lap_Time_List.Add(elapsedTime);

                // Updates text on button
                BoatRacing.NumberOfLaps = "Lap - "+ BoatRacing.Lap_Time_List.Count;
            });

            // Finish command triggered by button
            FinishCommand = new Command((object context) => {
                var BoatRacing = (Timing)context;

                // Set finished boat to elapsed time
                BoatRacing.Finish_Time = elapsedTime;

                // Check if race is completed
                checkIfRaceOver(selectedRace);
            });
        }

        public void checkIfRaceOver(Race selectedRace)
        {
            bool finished = true;

            // For each boat racing
            foreach(var x in timingList)
            {
                // If they havent finished and have no classification assigned
                if (x.Finish_Time == 0 && x.Special_Classification_Assigned == null)
                {
                    finished = false;
                }
            }
            if(finished == true)
            {
                // Switch to new page
                App.Current.MainPage = new RaceResults(timingList, selectedRace);
            }
        }

        /* 
         * Property get and sets 
         */

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

        // Set time elapsed as a string
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
