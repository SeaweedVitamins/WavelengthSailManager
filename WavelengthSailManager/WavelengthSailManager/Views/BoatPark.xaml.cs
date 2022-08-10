using System;
using System.Collections.Generic;
using WavelengthSailManager.Models;
using WavelengthSailManager.Views;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class BoatPark : ContentPage
    {
        public BoatPark()
        {
            InitializeComponent();
        }

        private void NavigateNewBoat(object sender, EventArgs e)
        {
            // Switches to new boat
            App.Current.MainPage = new NewBoat();
        }

        private async void NewSailorAsync(object sender, EventArgs e)
        {
            //Retrieve string result from prompt
            string result = await DisplayPromptAsync("Sailor Name", "Enter name below:");

            // Create interface and assign string to object
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            Sailor sailor = new Sailor();
            sailor.Sailor_Name = result;

            // Object can be written to the database
            await @interface.SaveSailorAsync(sailor);
        }

        private void NavigateToHome(object sender, EventArgs e)
        {
            // Switches current page to home
            App.Current.MainPage = new MainPage();
        }
    }
}
