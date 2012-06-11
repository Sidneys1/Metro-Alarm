using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MetroAlarm.Controls
{
    public partial class AlarmSound : ChildWindow
    {
        public AlarmSound()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void SnoozeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChildWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            Classes.Alarm alarm = this.DataContext as Classes.Alarm;
            switch (alarm.AlarmTrack)
            {
                case MetroAlarm.Classes.AudioTrack.Beeper:
                    MediaPlayer.Source = new Uri("/Resources/Audio/beeper.wav", UriKind.Relative);
                    break;
                case MetroAlarm.Classes.AudioTrack.Alarm:
                    MediaPlayer.Source = new Uri("/Resources/Audio/alarm.wav", UriKind.Relative);
                    break;
            }
            MediaPlayer.Play();
        }

        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Play();
        }
    }
}

