using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.Views
{
    public partial class EditBoat : ContentPage
    {
        
        public EditBoat(BoatSailor BoatSelected)
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                Boat selected = await @interface.GetBoatAsync(BoatSelected.ID);
                SailorPicker.SelectedIndex = selected.Sailor_ID;
                EntrySailNumber.Text = Convert.ToString(BoatSelected.Sail_Number);
            });
        }

        private void SaveNewBoat(object sender, EventArgs e)
        {
            App.Current.MainPage = new BoatPark();
        }
    }
}
