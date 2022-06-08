using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.Views
{
    public partial class NewRace : ContentPage
    {
        public NewRace()
        {
            InitializeComponent();
        }

        private void SaveRace(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;

                Race race = new Race();
                Category category = (Category)CategoryPicker.SelectedItem;
                Series series = (Series)SeriesPicker.SelectedItem;

                var raceNumber = await @interface.GetTopSeriesRaceNumberAsync(series.ID);
                race.Race_Number = raceNumber[0]+1;
                race.Category_ID = category.ID;
                race.Series_ID = series.ID;
                race.Special_Event = SpecialEventEntry.Text;
                race.DateTime = RaceDatePicker.Date + RaceTimePicker.Time;
                
                await @interface.SaveRaceAsync(race);
            });
            App.Current.MainPage = new RaceManagement();
        }
    }
}
