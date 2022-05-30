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
            var button = (Button)sender;
            BoatSailor ButtonContext = (BoatSailor)button.BindingContext;
            var boatID = ButtonContext.ID;
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
            App.Current.MainPage = new RaceStart(CompetingBoatList, selectedRace);
        }
    }
}
