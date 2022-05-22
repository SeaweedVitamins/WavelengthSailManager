using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class RaceStart : ContentPage
    {
        public RaceStart(List<int> competingBoatList)
        {
            InitializeComponent();
        }

        private void NextPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new StartCountdown();
        }
    }
}
