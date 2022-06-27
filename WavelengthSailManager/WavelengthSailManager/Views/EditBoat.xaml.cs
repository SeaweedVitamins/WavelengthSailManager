using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.Views
{
    public partial class EditBoat : ContentPage
    {
        public EditBoat(Boat BoatSelected)
        {
            InitializeComponent();
            //not sure this is the right id
            SailorPicker.SelectedItem = BoatSelected.Sailor_ID;
            EntrySailNumber.Text = Convert.ToString(BoatSelected.Sail_Number);
        }
    }
}
