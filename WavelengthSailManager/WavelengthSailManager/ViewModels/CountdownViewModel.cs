using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager.ViewModels
{
    public class CountdownViewModel : INotifyPropertyChanged
    {
        int timeToGo;
        bool warningFlag;
        bool preparatoryFlag;

        public event PropertyChangedEventHandler PropertyChanged;

        public CountdownViewModel(List<int> competingBoatList, Race selectedRace)
        {
            // Set to 5 minutes
            this.TimeToGo = 300;

            // Create device timer
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                // Decrement the timer
                this.TimeToGo--;

                // Triggers for each interval where a change should occur
                if (this.TimeToGo == 299) { WarningFlag = true; soundSignal(); }
                if (this.TimeToGo == 240) { PreparatoryFlag = true; soundSignal(); }
                if (this.TimeToGo == 60) { WarningFlag = false; soundSignal(); }
                if (this.TimeToGo == 0) {

                    // Send sound signal
                    PreparatoryFlag = false; soundSignal();

                    // Switch to race view on end of timer
                    App.Current.MainPage = new RaceView(competingBoatList, selectedRace);
                    return false;
                }
                
                return true;
            });
        }

        // Getter and setter for time to go
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

        // Getter for time to start, formatting included
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

        // Sound signal function for each interval
        public void soundSignal()
        {
            // Set the audio player and source
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioSource = assembly.GetManifestResourceStream(
                "WavelengthSailManager.Resources.Audio.SoundSignal.mp3");

            // Play the sound
            player.Load(audioSource);
            player.Play();
        }
    }
}
