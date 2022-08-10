using System;
using System.Collections.Generic;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class BoatList : ContentPage
    {
        public List<int> CompetingBoatList = new List<int>();

        Race selectedRace;

        public BoatList(Race selectedRace)
        {
            this.selectedRace = selectedRace;
            InitializeComponent();
        }

        void ToggleBoat(object sender, EventArgs args)
        {
            // Import the element and cast
            var button = (Button)sender;
            // Cast to model
            BoatSailor ButtonContext = (BoatSailor)button.BindingContext;

            var boatID = ButtonContext.ID;
            // Switcher for colour
            if (CompetingBoatList.Contains(boatID))
            {
                CompetingBoatList.Remove(boatID);
                button.BackgroundColor = Color.DeepSkyBlue;
            } else
            {
                CompetingBoatList.Add(boatID);
                button.BackgroundColor = Color.ForestGreen;
            }
        }

        private void NextPage(object sender, EventArgs e)
        {
            // Check if there are at least 3 boats
            if (CompetingBoatList.Count < 3)
            {
                DisplayAlert("Error", "You must select at least 3 boats", "OK");
                return;
            }
            App.Current.MainPage = new RaceStart(CompetingBoatList, selectedRace);
        }
    }
}
