using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WavelengthSailManager.Models;
using WavelengthSailManager.ViewModels;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class RaceResults : ContentPage
    {
        public RaceResults(ObservableCollection<Timing> timingList, Race selectedRace)
        {
            InitializeComponent();
            BindingContext = new RaceResultsViewModel(timingList, selectedRace);
        }
    }
}
