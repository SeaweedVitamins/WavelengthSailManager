using System;
using System.Collections.Generic;
using WavelengthSailManager.Models;
using WavelengthSailManager.ViewModels;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class RaceView : ContentPage
    {

        public RaceView(List<int> competingBoatList, Race selectedRace)
        {
            InitializeComponent();

            // Set the grid binding context to race view model
            BindingContext = new RaceViewModel(competingBoatList, selectedRace);
        }

        private void NavigateToHome(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

        private void BoatFinished(object sender, EventArgs e)
        {
            // Get button context
            var fb = (Button)sender;
            var mainGrid = (Grid)fb.Parent;

            // Disable children of grid when finished
            foreach(var x in mainGrid.Children)
            {
                if(x.GetType() != typeof(Label))
                {
                    x.IsEnabled = false;
                    x.BackgroundColor = Color.LightGray;
                }
            }
        }

        private void BoatLapped(object sender, EventArgs e)
        {
            // Get button context
            var lb = (Button)sender;
            lb.BackgroundColor = Color.Orange;
            int duration = 2;

            // Display yellow for 2 seconds
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                duration--;
                if (duration == 0) { lb.BackgroundColor = Color.Green; return false; }

                return true;
            });
        }
    }
}
