using System;
using System.ComponentModel;

namespace MetroAlarm.Classes
{
	public delegate void AlarmEvent(AlarmArgs e);

	public class Alarm : INotifyPropertyChanged
	{
        Time _alarmTime;
		public Time AlarmTime 
        {
            get { return _alarmTime; }
            set
            {
                _alarmTime = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("AlarmTime"));
            }
        }

		public string Name { get; set; }

        string _desc;
        public string Desc
        {
            get { return _desc; }
            set 
            {
                _desc = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Desc"));
            }
        }

        bool _enabled = false;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (value != _enabled)
                {
                    _enabled = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Enabled"));
                }
            }
        }

        AudioTrack _alarmTrack = AudioTrack.Beeper;
        public AudioTrack AlarmTrack
        {
            get { return _alarmTrack; }
            set
            {
                if (value != _alarmTrack)
                {
                    _alarmTrack = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("AlarmTrack"));
                }
            }
        }

		public Alarm(string n) 
		{ 
			Name = n;
			Desc = "";
			AlarmTime = new Time();
		}

        public event PropertyChangedEventHandler PropertyChanged;
    }

	public class AlarmArgs : EventArgs
	{
		public Alarm Alarm { get; set; }

		public AlarmArgs(Alarm a)
		{
			Alarm = a;
		}
	}
}
