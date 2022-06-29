using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WavelengthSailManager.Models;
using WavelengthSailManager.ViewModels;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class ResultsViewer : ContentPage
    {
        public ResultsViewer(ObservableCollection<Timing> timingList, Race selectedRace)
        {
            InitializeComponent();
        }
    }
}
