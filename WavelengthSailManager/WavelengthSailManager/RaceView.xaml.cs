using System;
using System.Collections.Generic;
using WavelengthSailManager.Models;
using WavelengthSailManager.ViewModels;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class RaceView : ContentPage
    {
        //public List<int> competingBoatList = new List<int>();

        public RaceView(List<int> competingBoatList)
        {
            //this.competingBoatList = competingBoatList;
            InitializeComponent();
            BindingContext = new RaceViewModel(competingBoatList);
        }

        private void NextPage(object sender, EventArgs e)
        {
           // App.Current.MainPage = new RaceStart(CompetingBoatList);
        }
    }
}
