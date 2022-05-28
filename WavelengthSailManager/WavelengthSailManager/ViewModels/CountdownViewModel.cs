using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    public class CountdownViewModel : INotifyPropertyChanged
    {
        int timeToGo;
        bool warningFlag;
        bool preparatoryFlag;

        public event PropertyChangedEventHandler PropertyChanged;

        public CountdownViewModel(List<int> competingBoatList)
        {
            this.TimeToGo = 300;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                this.TimeToGo--;
                if (this.TimeToGo == 299) { WarningFlag = true; soundSignal(); }
                if (this.TimeToGo == 240) { PreparatoryFlag = true; soundSignal(); }
                if (this.TimeToGo == 60) { WarningFlag = false; soundSignal(); }
                if (this.TimeToGo == 0) {
                    PreparatoryFlag = false; soundSignal();
                    App.Current.MainPage = new RaceView(competingBoatList);
                    return false;
                }
                
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

        public void soundSignal()
        {
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioSource = assembly.GetManifestResourceStream("WavelengthSailManager.Resources.Audio.SoundSignal.mp3");
            player.Load(audioSource);
            player.Play();
        }
    }
}
