using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Animation;

// For Changing the hinting color:
//		http://forums.silverlight.net/p/229874/560110.aspx/1?Re+Change+Color+staticresource+at+runtime+
namespace MetroAlarm
{
	public partial class MainPage : UserControl
	{
		public int page = 0;
		private ObservableCollection<Classes.Alarm> Alarms;

		public MainPage()
		{
			InitializeComponent();
			SettingsFrame.Opacity = 0;

			if (!Application.Current.IsRunningOutOfBrowser)
			{
				closeImage.Visibility = System.Windows.Visibility.Collapsed;
				minimizeImage.Visibility = System.Windows.Visibility.Collapsed;
			}

			if (Application.Current.InstallState == InstallState.NotInstalled)
				Application.Current.InstallStateChanged += Current_InstallStateChanged;
			else
				InstallBtn.Visibility = System.Windows.Visibility.Collapsed;

			Alarms = new ObservableCollection<Classes.Alarm>();

			Alarms.Add(new Classes.Alarm("Wake up my ChickPea!") { Enabled = true, AlarmTime = new Classes.Time(7,30,00), Desc = "She is wonderful and deserves a loving wakeup call... :)" });
            Alarms.Add(new Classes.Alarm("Arise! Seize the Day! Carpe Diem!!") { Enabled = true });

			AlarmFrame.DataContext = Alarms;

            System.Windows.Threading.DispatcherTimer Checker = new System.Windows.Threading.DispatcherTimer();
            Checker.Interval = new TimeSpan(0, 0, 31);
            Checker.Tick += Checker_Tick;
            Checker.Start();
		}

        void Checker_Tick(object sender, EventArgs e)
        {
            foreach (Classes.Alarm alarm in Alarms)
            {
                if (alarm.Enabled && alarm.AlarmTime.Hour == DateTime.Now.Hour && alarm.AlarmTime.Minute == DateTime.Now.Minute)
                    alarm.Desc += "Alarm!";
            }
        }

		#region Titlebar

		private void Dragger_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.DragMove();
		}

        private void closeImage_Click(object sender, RoutedEventArgs e)
        {
			Application.Current.MainWindow.Close();
        }

        private void minimizeImage_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

		#endregion

		#region Resize Borders

		private void ResizeTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.DragResize(WindowResizeEdge.Top);
		}

		private void ResizeBottom_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.DragResize(WindowResizeEdge.Bottom);
		}

		private void ResizeRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.DragResize(WindowResizeEdge.Right);
		}

		private void ResizeLeft_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.DragResize(WindowResizeEdge.Left);
		}

		private void ResizeTopLeft_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.DragResize(WindowResizeEdge.TopLeft);
		}

		private void ResizeTopRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.DragResize(WindowResizeEdge.TopRight);
		}

		private void ResizeBottomLeft_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.DragResize(WindowResizeEdge.BottomLeft);
		}

		private void ResizeBottomRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.DragResize(WindowResizeEdge.BottomRight);
		}

		#endregion

		#region Page Switching

        private void SettingsButton_CheckChanged(object sender, EventArgs e)
        {
            if (page == 1 && sender == AlarmsButton)
			{
				SettingsFadeOut.Begin();
				SettingsButton.SilentCheckState(false);
				page = 0;
			}
			else if (page == 0 && sender == SettingsButton)
			{
				AlarmFadeOut.Begin();
				AlarmsButton.SilentCheckState(false);
				page = 1;
			}
        }

		#endregion

		#region Install

		void Current_InstallStateChanged(object sender, EventArgs e)
		{
			switch (Application.Current.InstallState)
			{
				case InstallState.InstallFailed:
				case InstallState.NotInstalled:
					InstallBtn.Visibility = System.Windows.Visibility.Visible;
					break;
				case InstallState.Installed:
				case InstallState.Installing:
					InstallBtn.Visibility = System.Windows.Visibility.Collapsed;
					break;
			}
		}

		private void InstallBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Application.Current.Install();
		}

		#endregion

		#region Add/Remove Alarms

		private void AlarmFrame_RemoveAlarm(object sender, Classes.AlarmArgs e)
		{
			Alarms.Remove(e.Alarm);
		}

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
			Alarms.Add(new Classes.Alarm("New Alarm"));
        }

		#endregion

		#region Fades

		private void AlarmFadeOut_Completed(object sender, EventArgs e)
		{
            AlarmFrame.Visibility = System.Windows.Visibility.Collapsed;
            SettingsFrame.Visibility = System.Windows.Visibility.Visible;
			SettingsFadeIn.Begin();
		}

		private void SettingsFadeOut_Completed(object sender, EventArgs e)
		{
            AlarmFrame.Visibility = System.Windows.Visibility.Visible;
            SettingsFrame.Visibility = System.Windows.Visibility.Collapsed;
			AlarmFadeIn.Begin();
		}

		#endregion

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ((Classes.ColorResourceWrapper)Application.Current.Resources["Hinting"]).Color = Colors.Gray;
            //ChangeColorStoryboard.Begin();
        }
	}
}