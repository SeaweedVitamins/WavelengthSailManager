using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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

        private async void NavigateToAdminMenuAsync(object sender, EventArgs e)
        {
            // Retrieve user entered password
            string result = await DisplayPromptAsync("Password", "Enter password below:");

            // Get set password
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            List<Configuration> config = await @interface.GetConfigurationAsync();
            Configuration configuration = config.Where(n => n.Name == "Admin_Password").FirstOrDefault();

            // Generate salt and calculate hash
            byte[] hashBytes = Convert.FromBase64String(configuration.Value);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(configuration.Value, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Verify if hash is the same as stored
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    //return;
                }
            }
            App.Current.MainPage = new AdminMenuView();
        }

        private void NavigateToResultsViewer(object sender, EventArgs e)
        {
            App.Current.MainPage = new ResultsViewer();
        }
    }
}
