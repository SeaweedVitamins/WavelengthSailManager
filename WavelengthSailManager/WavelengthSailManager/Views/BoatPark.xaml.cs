﻿using System;
using System.Collections.Generic;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class BoatPark : ContentPage
    {
        public BoatPark()
        {
            InitializeComponent();
        }

        private async void NewSailorAsync(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Sailor Name", "Enter name below:");
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            Sailor sailor = new Sailor();
            sailor.Sailor_Name = result;
            await @interface.SaveSailorAsync(sailor);
        }
    }
}