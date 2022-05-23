using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class StartCountdown : ContentPage
    {
        List<int> competingBoatList = new List<int>();
        public StartCountdown(List<int> competingBoatList)
        {
            this.competingBoatList = competingBoatList;
            InitializeComponent();
        }

        private void RestartSequence(object sender, EventArgs e)
        {
            App.Current.MainPage = new RaceStart(competingBoatList);
        }
    }
}
