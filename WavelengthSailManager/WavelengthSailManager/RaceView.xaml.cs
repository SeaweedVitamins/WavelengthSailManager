using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private void BoatFinished(object sender, EventArgs e)
        {
            var fb = (Button)sender;
            var mainGrid = (Grid)fb.Parent;
            foreach(var x in mainGrid.Children)
            {
                if(x.GetType() != typeof(Label))
                {
                    x.IsEnabled = false;
                    x.BackgroundColor = Color.LightGray;
                }
            }
        }

        private void BoatLapped(object sender, EventArgs e)
        {
            var lb = (Button)sender;
            lb.BackgroundColor = Color.Orange;
            int duration = 2;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                duration--;
                if (duration == 0) { lb.BackgroundColor = Color.Green; return false; }

                return true;
            });
        }
    }
}
