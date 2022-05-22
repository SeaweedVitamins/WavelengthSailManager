using System;
using System.Collections.Generic;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class BoatList : ContentPage
    {
        public List<int> CompetingBoatList = new List<int>();

        public BoatList()
        {
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
                button.BackgroundColor = Color.OrangeRed;
            } else
            {
                CompetingBoatList.Add(boatID);
                button.BackgroundColor = Color.ForestGreen;
            }
        }
    }
}
