using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.Views
{
    public partial class NewBoat : ContentPage
    {
        
        public NewBoat()
        {
            InitializeComponent();
        }

        private void SaveBoat(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                // Assign selected picker to boat
                Boat boat = (Boat)ClassPicker.SelectedItem;
                Boat newBoat = new Boat();

                // Retrieve the sailor ID sail number and class
                newBoat.Sailor_ID = SailorPicker.SelectedIndex;
                newBoat.Sail_Number = Convert.ToInt32(EntrySailNumber.Text);
                newBoat.Class_Name = boat.Class_Name;

                // Save new model to the database
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                await @interface.SaveBoatAsync(newBoat);
            });
        }
    }
}
