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
    class RaceResultsViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Results> resultList;

        int placeCount = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public RaceResultsViewModel(ObservableCollection<Timing> timingList)
        {
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                ObservableCollection<PY> pyList = new ObservableCollection<PY>(await @interface.GetPYsAsync());
                ObservableCollection<BoatSailor> sailorList = new ObservableCollection<BoatSailor>(await @interface.GetBoatListAsync());
                ResultList = calculateResults(timingList, pyList, sailorList);
            });
        }

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

        public ObservableCollection<Results> calculateResults(ObservableCollection<Timing> timingList, ObservableCollection<PY> pyList, ObservableCollection<BoatSailor> sailorList)
        {
            int maxNumberOfLaps = 1;
            foreach(var x in timingList)
            {
                if(x.Lap_Time_List.Count > maxNumberOfLaps)
                {
                    maxNumberOfLaps = x.Lap_Time_List.Count;
                }
            }
            foreach(var x in timingList)
            {
                int pyValue = 0;
                for(int i=0; i<pyList.Count; i++)
                {
                    if(pyList[i].Class_Name == x.Class_Name)
                    {
                        pyValue = pyList[i].Value;
                        break;
                    }
                }

                var numberOfLaps = 1;
                if(x.Lap_Time_List.Count > numberOfLaps) { numberOfLaps = x.Lap_Time_List.Count; }
                x.Corrected_Time = (x.Finish_Time * maxNumberOfLaps * 1000) / (pyValue * numberOfLaps);

            }

            var list = timingList.ToList<Timing>();
            list = timingList.OrderBy(t => t.Corrected_Time).ToList();

            
            var resultsLinqList = list.ConvertAll(x => new Results { Place = getPlace(x.Special_Classification_Assigned), Sail_Number = x.Sail_Number,
                Sailor_Name = getSailorName(sailorList, x.ID), Class_Name = x.Class_Name, Number_Of_Laps = getNumberOfLaps(x.Lap_Time_List.Count),
                Finish_Time = x.Finish_Time, Corrected_Time = ConvertTimeToString(x.Corrected_Time)});

            ObservableCollection<Results> results = new ObservableCollection<Results>(resultsLinqList);
            return results;
        }

        public int getNumberOfLaps(int lapCount)
        {
            if(lapCount == 0)
            {
                return 1;
            }
            else
            {
                return lapCount;
            }
        }

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

        public string ConvertTimeToString(int correctedTime)
        {
            TimeSpan time = TimeSpan.FromSeconds(correctedTime);
            return time.ToString(@"mm\:ss");
        }
    }
}
