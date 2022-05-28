using System;
using System.Collections.Generic;
using WavelengthSailManager.ViewModels;
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
            BindingContext = new CountdownViewModel(competingBoatList);
        }

        private void RestartSequence(object sender, EventArgs e)
        {
            App.Current.MainPage = new RaceStart(competingBoatList);
        }

        private void NextPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new RaceView(competingBoatList);
        }
    }
}
