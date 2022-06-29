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
                Boat boat = (Boat)ClassPicker.SelectedItem;
                Boat newBoat = new Boat();
                newBoat.Sailor_ID = SailorPicker.SelectedIndex;
                newBoat.Sail_Number = Convert.ToInt32(EntrySailNumber.Text);
                newBoat.Class_Name = boat.Class_Name;
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                await @interface.SaveBoatAsync(newBoat);
            });
        }
    }
}
