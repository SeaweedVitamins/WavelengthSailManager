using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.Views
{
    public partial class EditBoat : ContentPage
    {
        BoatSailor BoatSelected;
        
        public EditBoat(BoatSailor BoatSelected)
        {
            InitializeComponent();

            // Set local boat to passed in context
            this.BoatSelected = BoatSelected;
            Task.Run(async () =>
            {
                // Get selected boat details from the db
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                Boat selected = await @interface.GetBoatAsync(BoatSelected.ID);

                // Assign data to the inputs
                SailorPicker.SelectedIndex = selected.Sailor_ID;
                EntrySailNumber.Text = Convert.ToString(BoatSelected.Sail_Number);
            });
        }

        private void SaveBoat(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                // Retrieve boat being edited
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                Boat selected = await @interface.GetBoatAsync(BoatSelected.ID);

                // Assign updated information to model
                selected.Sailor_ID = SailorPicker.SelectedIndex;
                selected.Sail_Number = Convert.ToInt32(EntrySailNumber.Text);

                // Re-add data to db
                await @interface.SaveBoatAsync(selected);
            });
            App.Current.MainPage = new BoatPark();
        }
    }
}
