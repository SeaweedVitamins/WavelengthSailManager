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
    class ResultsViewerViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Series> seriesList;

        ObservableCollection<Race> raceList;

        ObservableCollection<Race> RaceCollection;

        ObservableCollection<ResultsViewModel> resultsDisplay;

        Series selectedSeries;

        public Series SelectedSeries
        {
            get { return selectedSeries; }
            set { OnSeriesChanged(value); selectedSeries = value; }
        }

        public Race SelectedRace {
            set {
                    displayRace(value, 0);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ResultsViewerViewModel()
        {
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                SeriesList = new ObservableCollection<Series>(await @interface.GetSeriesAsync());
                RaceCollection = new ObservableCollection<Race>(await @interface.GetRaceAsync());
            });
        }

        private void OnSeriesChanged(Series SelectedSeries)
        {
            displayRace(null, SelectedSeries.ID);
            var races = RaceCollection.Where(a => a.Series_ID == SelectedSeries.ID).ToList();
            RaceList = new ObservableCollection<Race>(races);
        }

        public void displayRace(Race SelectedRace, int selectedSeries)
        {
            if(SelectedRace == null)
            {
                Task.Run(async () =>
                {
                    DatabaseInterface @interface = await DatabaseInterface.Instance;
                    List<Results> list = await @interface.GetSeriesResultsAsync(selectedSeries);

                    var resultsLinqList = list.ConvertAll(x => new ResultsViewModel
                    {
                        Boat_ID = x.Boat_ID,
                        Position = x.Place,
                        Sail_Number = Convert.ToString(x.Sail_Number),
                        Name = x.Sailor_Name,
                        Class = x.Class_Name,
                        Points = calculatePoints(x.Place, list.Count, false)
                    });

                    var groupedResults = from boat in resultsLinqList
                                         group boat by boat.Boat_ID into r
                                         select new ResultsViewModel
                                         {
                                             Boat_ID = r.First().Boat_ID,
                                             Sail_Number = Convert.ToString(r.First().Sail_Number),
                                             Name = r.First().Name,
                                             Class = r.First().Class,
                                             Points = Convert.ToString(r.Sum(p => Convert.ToInt16(calculatePoints(p.Points, 10, true))))
                                         };

                    List<ResultsViewModel> orderedResults = groupedResults.OrderBy(t => Convert.ToInt16(t.Points)).ToList();
                    int count = 0;
                    foreach (var x in orderedResults)
                    {
                        x.Position = Convert.ToString(count+1);
                        count++;
                    }

                    ResultsDisplay = new ObservableCollection<ResultsViewModel>(orderedResults);
                });
            }
            else
            {
                Task.Run(async () =>
                {
                    DatabaseInterface @interface = await DatabaseInterface.Instance;
                    List<Results> list = await @interface.GetResultsAsync(SelectedRace.ID);

                    var resultsLinqList = list.ConvertAll(x => new ResultsViewModel
                    {
                        Position = x.Place,
                        Sail_Number = Convert.ToString(x.Sail_Number),
                        Name = x.Sailor_Name,
                        Class = x.Class_Name,
                        Points = calculatePoints(x.Place, list.Count, false)
                    });

                    ResultsDisplay = new ObservableCollection<ResultsViewModel>(resultsLinqList);
                });
            }
        }

        public string calculatePoints(string recorded, int max, bool returnNumber)
        {
            if(int.TryParse(recorded, out int value))
            {
                return recorded;
            }
            else if(returnNumber == true)
            {
                return Convert.ToString(max);
            }
            else
            {
                return recorded + " (" + max++ + ") ";
            }
        }

        public ObservableCollection<Series> SeriesList
        {
            set
            {
                if (seriesList != value)
                {
                    seriesList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SeriesList"));
                    }
                }
            }
            get
            {
                return seriesList;
            }
        }

        public ObservableCollection<Race> RaceList
        {
            set
            {
                if (raceList != value)
                {
                    raceList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("RaceList"));
                    }
                }
            }
            get
            {
                return raceList;
            }
        }

        public ObservableCollection<ResultsViewModel> ResultsDisplay
        {
            set
            {
                if (resultsDisplay != value)
                {
                    resultsDisplay = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ResultsDisplay"));
                    }
                }
            }
            get
            {
                return resultsDisplay;
            }
        }
    }
}