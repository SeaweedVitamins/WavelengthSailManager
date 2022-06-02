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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
                
            //if (Settings.FirstRun)
            //{
                Task.Run(() => this.loadDatabase()).Wait();
              //  Settings.FirstRun = false;
           // }
        }

        public async Task loadDatabase()
        {
            DatabaseInterface @interface = await DatabaseInterface.Instance;
        }

        private void NavigateToTodaysRaces(object sender, EventArgs e)
        {
            App.Current.MainPage = new TodaysRaces();
        }

        private void NavigateToAdminMenu(object sender, EventArgs e)
        {
            App.Current.MainPage = new AdminMenuView();
        }
    }
}
