using System;
using System.Collections.Generic;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class RaceView : ContentPage
    {
        public List<int> CompetingBoatList = new List<int>();

        public RaceView()
        {
            InitializeComponent();
        }

        void ToggleBoat(object sender, EventArgs args)
        {
            
        }

        private void NextPage(object sender, EventArgs e)
        {
           // App.Current.MainPage = new RaceStart(CompetingBoatList);
        }
    }
}
