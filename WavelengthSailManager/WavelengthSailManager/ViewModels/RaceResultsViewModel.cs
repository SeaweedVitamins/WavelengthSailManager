using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WavelengthSailManager.Models;

namespace WavelengthSailManager.ViewModels
{
    class RaceResultsViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Results> resultList;

        int placeCount = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public RaceResultsViewModel(ObservableCollection<Timing> timingList, Race selectedRace)
        {
            Task.Run(async () =>
            {
                // Database interface and retrieve needed data
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                ObservableCollection<PY> pyList = new ObservableCollection<PY>(await @interface.GetPYsAsync());
                ObservableCollection<BoatSailor> sailorList = new ObservableCollection<BoatSailor>(await @interface.GetBoatListAsync());

                // Calculate the results for race completed
                ResultList = calculateResults(timingList, pyList, sailorList, selectedRace);
                foreach(var Result in ResultList)
                {
                    await @interface.SaveResultsAsync(Result);
                }
            });
        }

        // Getter and setter for the result list
        public ObservableCollection<Results> ResultList
        {
            set
            {
                if (resultList != value)
                {
                    resultList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ResultList"));
                    }
                }
            }
            get
            {
                return resultList;
            }
        }

        // Function to calculate the results for one race
        public ObservableCollection<Results> calculateResults(ObservableCollection<Timing> timingList,
            ObservableCollection<PY> pyList, ObservableCollection<BoatSailor> sailorList, Race selectedRace)
        {
            // Setting the number of laps default 1
            int maxNumberOfLaps = 1;

            // Each boat that is in the racing list
            foreach(var x in timingList)
            {
                if(x.Lap_Time_List.Count > maxNumberOfLaps)
                {
                    // Set number of laps completed
                    maxNumberOfLaps = x.Lap_Time_List.Count;
                }
            }

            // Each boat in the racing list
            foreach(var x in timingList)
            {
                int pyValue = 0;
                for(int i=0; i<pyList.Count; i++)
                {
                    if(pyList[i].Class_Name == x.Class_Name)
                    {
                        // Set the PY value
                        pyValue = pyList[i].Value;
                        break;
                    }
                }

                var numberOfLaps = 1;

                // Calculate the corrected time
                if(x.Lap_Time_List.Count > numberOfLaps) { numberOfLaps = x.Lap_Time_List.Count + 1; }
                x.Corrected_Time = (x.Finish_Time * maxNumberOfLaps * 1000) / (pyValue * numberOfLaps);

            }

            // Order by corrected time
            var list = timingList.ToList<Timing>();
            list = timingList.OrderBy(t => t.Corrected_Time).ToList();

            for(int i=0; i < list.Count; i++)
            {
                // If classification is set
                if (list[i].Special_Classification_Assigned != null)
                {
                    // Move item to end of result list
                    var item = list[i];
                    list.RemoveAt(i);
                    list.Add(item);
                }
            }

            // Convert to Results list
            var resultsLinqList = list.ConvertAll(x => new Results { Place = getPlace(x.Special_Classification_Assigned),
                Race_ID = selectedRace.ID, Sail_Number = x.Sail_Number, Sailor_Name = getSailorName(sailorList, x.ID),
                Class_Name = x.Class_Name, Number_Of_Laps = getNumberOfLaps(x.Lap_Time_List.Count),
                Finish_Time = ConvertTimeToString(x.Finish_Time), Corrected_Time = ConvertTimeToString(x.Corrected_Time),
                Boat_ID = x.ID});

            // Set observable collection to be displayed
            ObservableCollection<Results> results = new ObservableCollection<Results>(resultsLinqList);
            return results;
        }

        // Function to set number of laps completed by boat
        public int getNumberOfLaps(int lapCount)
        {
            if(lapCount == 0)
            {
                return 1;
            }
            else
            {
                return lapCount +1;
            }
        }

        // Get the place in the results
        public string getPlace(string special)
        {
            if(special != null)
            {
                return special;
            }
            else
            {
                placeCount++;
                return Convert.ToString(placeCount);
            }
        }

        // Get the sailor name from list
        public string getSailorName(ObservableCollection<BoatSailor> sailorList, int boatId)
        {
            for (int i = 0; i < sailorList.Count; i++)
            {
                if (sailorList[i].ID == boatId)
                {
                    return sailorList[i].Sailor_Name;
                }
            }

            return "";
        }

        // Set the time as a string for displaying
        public string ConvertTimeToString(int correctedTime)
        {
            TimeSpan time = TimeSpan.FromSeconds(correctedTime);
            return time.ToString(@"mm\:ss");
        }
    }
}
