using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class RaceStart : ContentPage
    {
        List<int> competingBoatList = new List<int>();
        public RaceStart(List<int> competingBoatList)
        {
            InitializeComponent();
            this.competingBoatList = competingBoatList;
        }

        private void NextPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new StartCountdown(competingBoatList);
        }
    }
}
