using System;

namespace MetroAlarm.Classes
{
	public delegate void AlarmEvent(AlarmArgs e);

	public class Alarm
	{
		public Time alarmTime { get; set; }
		public string Name { get; set; }
		public string Desc { get; set; }
		public bool Enabled { get; set; }

		public Alarm(string n) 
		{ 
			Name = n;
			Desc = "";
			alarmTime = new Time();
		}
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
