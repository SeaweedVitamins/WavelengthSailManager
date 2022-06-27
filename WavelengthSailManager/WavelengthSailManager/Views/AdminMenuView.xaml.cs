using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class AdminMenuView : ContentPage
    {
        public AdminMenuView()
        {
            InitializeComponent();
        }

        private void NavigateToHome(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

        private void NavigateRaceManagement(object sender, EventArgs e)
        {
            App.Current.MainPage = new RaceManagement();
        }

        private void NavigatePYSettings(object sender, EventArgs e)
        {
            App.Current.MainPage = new PYSettings();
        }

        private void NavigateBoatPark(object sender, EventArgs e)
        {
            App.Current.MainPage = new BoatPark();
        }
    }
}
