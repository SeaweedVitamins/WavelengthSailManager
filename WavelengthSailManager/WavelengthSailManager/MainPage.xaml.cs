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
            DatabaseInterface @interface = await DatabaseInterface.Instance;
        }
    }
}
