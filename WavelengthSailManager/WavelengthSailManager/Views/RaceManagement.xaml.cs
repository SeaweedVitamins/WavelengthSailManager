using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using WavelengthSailManager.ViewModels;
using WavelengthSailManager.Views;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class RaceManagement : ContentPage
    {
        public RaceManagement()
        {
            InitializeComponent();
            setBinding();
        }

        public void setBinding()
        {
            BindingContext = new RaceManagementViewModel();
        }

        private async void NewSeriesAsync(object sender, EventArgs e)
        {
            // Display the prompt to request the series name
            string result = await DisplayPromptAsync("Series Name", "Enter name below:");

            // Save new series to db
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            Series series = new Series();
            series.Series_Name = result;
            await @interface.SaveSeriesAsync(series);

        }

        private async void NewCategoryAsync(object sender, EventArgs e)
        {
            // Display the prompt to request the category name
            string result = await DisplayPromptAsync("Category Name", "Enter name below:");

            // Save category to the db
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            Category category = new Category();
            category.Category_Name = result;
            await @interface.SaveCategoryAsync(category);
        }

        async void Delete(object sender, EventArgs args)
        {
            // Get context from buttion
            var button = (Button)sender;
            RaceManagementModel ButtonContext = (RaceManagementModel)button.BindingContext;

            // Get race context
            var raceID = ButtonContext.ID;
            DatabaseInterface @interface = await DatabaseInterface.Instance;

            // Get the race and remove from db
            Race race = await @interface.GetRaceAsync(raceID);
            await @interface.DeleteRaceAsync(race);
            setBinding();
        }

        private void NavigateNewRace(object sender, EventArgs e)
        {
            App.Current.MainPage = new NewRace();
        }

        private void NavigateToHome(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}
