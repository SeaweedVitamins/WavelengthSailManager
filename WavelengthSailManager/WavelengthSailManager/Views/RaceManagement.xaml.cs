using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using WavelengthSailManager.ViewModels;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class RaceManagement : ContentPage
    {
        public RaceManagement()
        {
            InitializeComponent();
            BindingContext = new RaceManagementViewModel();
        }

        private async void NewSeriesAsync(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Series Name", "Enter name below:");
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            Series series = new Series();
            series.Series_Name = result;
            await @interface.SaveSeriesAsync(series);

        }

        private async void NewCategoryAsync(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Category Name", "Enter name below:");
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            Category category = new Category();
            category.Category_Name = result;
            await @interface.SaveCategoryAsync(category);
        }
    }
}
