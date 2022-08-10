using System;
using System.Collections.Generic;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class RaceStart : ContentPage
    {
        List<int> competingBoatList = new List<int>();
        Race selectedRace;

        public RaceStart(List<int> competingBoatList, Race selectedRace)
        {
            InitializeComponent();

            // Competing boat list is just passed through
            this.competingBoatList = competingBoatList;
            this.selectedRace = selectedRace;
        }

        private void NextPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new StartCountdown(competingBoatList, selectedRace);
        }
    }
}
