using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    public class CountdownViewModel : INotifyPropertyChanged
    {
        int timeToGo;
        bool warningFlag;
        bool preparatoryFlag;

        public event PropertyChangedEventHandler PropertyChanged;

        public CountdownViewModel()
        {
            this.TimeToGo = 300;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                WarningFlag = this.TimeToGo == 300 ? true : false;
                WarningFlag = this.TimeToGo == 0 ? false : true;
                PreparatoryFlag = this.TimeToGo == 240 ? true : false;
                PreparatoryFlag = this.TimeToGo == 60 ? false : true;

                this.TimeToGo--;
                return true;
            });
        }

        public int TimeToGo
        {
            set
            {
                if (timeToGo != value)
                {
                    timeToGo = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TimeToStart"));
                    }
                }
            }
            get
            {
                return timeToGo;
            }
        }

        public string TimeToStart
        {
            get
            {
                TimeSpan time = TimeSpan.FromSeconds(timeToGo);
                return time.ToString(@"mm\:ss");
            }
        }

        public bool WarningFlag
        {
            set
            {
                if (warningFlag != value)
                {
                    warningFlag = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("WarningFlag"));
                    }
                }
            }
            get
            {
                return warningFlag;
            }
        }

        public bool PreparatoryFlag
        {
            set
            {
                if (preparatoryFlag != value)
                {
                    preparatoryFlag = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("PreparatoryFlag"));
                    }
                }
            }
            get
            {
                return preparatoryFlag;
            }
        }
    }
}
