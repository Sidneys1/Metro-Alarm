using System;

namespace MetroAlarm.Classes
{
	public class Time
	{
		#region Public constants

		public const char TIME_SEPERATOR = ':';

		#endregion

		#region Declarations

		public int Hour;
		public int Minute;
		public int Second;

		#endregion

		#region Constructors

		/// <summary>
		/// Create time object from current system time.
		/// </summary>
		public Time()
		{
			Hour = DateTime.Now.Hour;
			Minute = DateTime.Now.Minute;
			Second = DateTime.Now.Second;
		}

		/// <summary>
		/// Create time object from string value must be seperated as TIME_SEPERATOR constant.
		/// </summary>
		/// <param name="value"></param>
		public Time(string value)
		{
			string[] vals = value.Split(TIME_SEPERATOR); //new char[] { ':' });
			Hour = int.Parse(vals[0]);
			Minute = int.Parse(vals[1]);

			if (vals.Length > 2)
				Second = int.Parse(vals[2]);

			new Time(this.ToSeconds());
		}

		/// <summary>
		/// Create time object from parameters hour, minute and seconds.
		/// </summary>
		/// <param name="hour"></param>
		/// <param name="minute"></param>
		/// <param name="second"></param>
		public Time(int hour, int minute, int second)
		{
			Hour = hour;
			Minute = minute;
			Second = second;
			new Time(this.ToSeconds());
		}

		/// <summary>
		/// Create time object from seconds.
		/// </summary>
		/// <param name="seconds"></param>
		public Time(int seconds)
		{
			Minute = seconds / 60;
			Second = seconds % 60;

			Hour = Minute / 60;
			Minute = Minute % 60;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Add new time object and addition (+) it to previus time object.
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public Time Add(Time time)
		{
			this.Hour += time.Hour;
			this.Minute += time.Minute;
			this.Second += time.Second;

			return new Time(GetStringTime(this.ToSeconds()));
		}

		/// <summary>
		/// Add new string value and addition (+) it to previus time object.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public Time Add(string value)
		{
			return Add(new Time(value));
		}

		#endregion

		#region Public static methods

		/// <summary>
		/// Get current system time.
		/// </summary>
		/// <returns></returns>
		public static Time Now()
		{
			DateTime dt = DateTime.Now;
			return GetTimeFromSeconds(ToSeconds(dt));
		}

		/// <summary>
		/// Calculate time difference between two time objects.
		/// </summary>
		/// <param name="time1"></param>
		/// <param name="time2"></param>
		/// <returns></returns>
		public static Time TimeDiff(Time time1, Time time2)
		{
			try
			{
				int _secs1 = time1.ToSeconds();
				int _secs2 = time2.ToSeconds();

				int _secs = _secs1 - _secs2;

				return GetTimeFromSeconds(_secs);
			}
			catch
			{
				return new Time(0, 0, 0);
			}

		}

		/// <summary>
		/// Calculate time difference between two string values.
		/// </summary>
		/// <param name="time1"></param>
		/// <param name="time2"></param>
		/// <returns></returns>
		public static Time TimeDiff(string time1, string time2)
		{
			try
			{
				Time t1 = new Time(time1);
				Time t2 = new Time(time2);
				return TimeDiff(t1, t2);
			}
			catch
			{
				return new Time(0, 0, 0);
			}
		}

		/// <summary>
		/// Calculate time difference between two DateTime objects.
		/// </summary>
		/// <param name="dateTime1"></param>
		/// <param name="dateTime2"></param>
		/// <returns></returns>
		public static Time TimeDiff(DateTime dateTime1, DateTime dateTime2)
		{
			try
			{
				TimeSpan span = dateTime1 - dateTime2;
				return new Time(span.Seconds);
			}
			catch
			{
				return new Time(0, 0, 0);
			}
		}

		/// <summary>
		/// Calculate time difference between two second values.
		/// </summary>
		/// <param name="seconds1"></param>
		/// <param name="seconds2"></param>
		/// <returns></returns>
		public static Time TimeDiff(int seconds1, int seconds2)
		{
			try
			{
				Time t1 = new Time(seconds1);
				Time t2 = new Time(seconds2);
				return TimeDiff(t1, t2);
			}
			catch
			{
				return new Time(0, 0, 0);
			}
		}

		#endregion

		#region Convert methods

		/// <summary>
		/// Convert current time object to seconds.
		/// </summary>
		/// <returns></returns>
		public int ToSeconds()
		{
			return this.Hour * 3600 + this.Minute * 60 + this.Second;
		}

		/// <summary>
		/// Convert DateTime object to seconds.
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static int ToSeconds(DateTime dateTime)
		{
			return dateTime.Hour * 3600 + dateTime.Minute * 60 + dateTime.Second;
		}

		/// <summary>
		/// Convert current time object to string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{		
			return String.Format("{0:0}:{1:00}", Hour, Minute);
		}

		/// <summary>
		/// Convert seconds to time object.
		/// </summary>
		/// <param name="seconds"></param>
		/// <returns></returns>
		public static Time GetTimeFromSeconds(int seconds)
		{
			int _mins = seconds / 60;
			seconds = seconds % 60;

			int _hours = _mins / 60;
			_mins = _mins % 60;

			return new Time(_hours, _mins, seconds);
		}


		/// <summary>
		/// Convert seconds to string time.
		/// </summary>
		/// <param name="seconds"></param>
		/// <returns></returns>
		private string GetStringTime(int seconds)
		{
			int _mins = seconds / 60;
			seconds = seconds % 60;

			int _hours = _mins / 60;
			_mins = _mins % 60;

			this.Hour = _hours;
			this.Minute = _mins;
			this.Second = seconds;

			return String.Format("{0:00}:{1:00}:{2:00}", _hours, _mins, seconds); ;
		}

		/// <summary>
		/// Parse string to time.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Time Parse(string value)
		{
			try
			{
				return new Time(value);
			}
			catch
			{
				throw new Exception("Error parsing time!");
			}
		}

		#endregion

		#region Subtract time objects

		public static Time operator +(Time t1, Time t2)
		{
			Time t3 = new Time(t1.Hour, t1.Minute, t1.Second);
			t3.Add(t2);
			return t3;
		}

		public static Time operator -(Time t1, Time t2)
		{
			return TimeDiff(t1, t2);
		}

		#endregion
	}

}
