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

            this.BoatSelected = BoatSelected;
            // Setting Sailor id is unreliable
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                Boat selected = await @interface.GetBoatAsync(BoatSelected.ID);
                SailorPicker.SelectedIndex = selected.Sailor_ID;
                EntrySailNumber.Text = Convert.ToString(BoatSelected.Sail_Number);
            });
        }

        private void SaveBoat(object sender, EventArgs e)
        {
            // Setting Sailor id is unreliable
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                Boat selected = await @interface.GetBoatAsync(BoatSelected.ID);
                selected.Sailor_ID = SailorPicker.SelectedIndex;
                selected.Sail_Number = Convert.ToInt32(EntrySailNumber.Text);
                await @interface.SaveBoatAsync(selected);
            });
        }
    }
}
