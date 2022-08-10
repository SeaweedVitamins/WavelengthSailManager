using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using WavelengthSailManager.ViewModels;
using WavelengthSailManager.Views;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class PYSettings : ContentPage
    {
        public PYSettings()
        {
            InitializeComponent();
            setBinding();
        }

        public void setBinding()
        {
            // Set the view binding
            BindingContext = new PYViewModel();
        }

        // Get the py calue for a class
        private void GetCorrespondingPyValue(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            PY context = (PY)picker.ItemsSource[selectedIndex];

            // Set to text entry
            PYEntry.Text = Convert.ToString(context.Value);
        }

        // Save the new py value assigned
        private void SaveModifiedPYValue(object sender, EventArgs e)
        {
            PY context = (PY)ClassPicker.ItemsSource[ClassPicker.SelectedIndex];
            context.Value = Convert.ToInt32(PYEntry.Text);
            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                await @interface.SavePYAsync(context);
            });
        }

        // Save a new py value with the class
        private void SaveNewPYValue(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                PY py = new PY();
                py.Class_Name = EntryClassName.Text;
                py.Value = Convert.ToInt32(EntryPYValue.Text);
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                await @interface.SavePYAsync(py);
            });
        }

        private void NavigateToHome(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}
