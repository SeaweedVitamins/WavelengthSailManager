using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            onSaveClick();
        }

        async void onSaveClick()
        {
            Race insertRace = new Race();
            insertRace.DateTime = DateTime.Now;
            insertRace.Category_ID = "LOL";
            insertRace.Series_ID = "LOL";
            insertRace.Race_Number = 1;
            insertRace.ID = 2;
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            await @interface.SaveItemAsync(insertRace);
        }
    }
}
