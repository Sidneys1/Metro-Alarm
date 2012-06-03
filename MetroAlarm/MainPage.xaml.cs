using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

			Alarms.Add(new Classes.Alarm("Wake up my ChickPea!") { Enabled = true, alarmTime = new Classes.Time(7,30,00), Desc = "She is wonderful and deserves a loving wakeup call... :)" });
			Alarms.Add(new Classes.Alarm("Arise! Seize the Day! Carpe Diem!!"));

			AlarmFrame.DataContext = Alarms;
		}

		#region Titlebar

		private void minimizeImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}

		private void closeImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.Close();
		}

		private void Dragger_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Application.Current.MainWindow.DragMove();
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

		private void AlarmsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (page == 1 && sender == AlarmsButton)
			{
				AlarmButtonFadeIn.Begin();
				SettingsButtonFadeOut.Begin();
				SettingsFadeOut.Begin();
				page = 0;
			}
			else if (page == 0 && sender == SettingsButton)
			{
				AlarmButtonFadeOut.Begin();
				SettingsButtonFadeIn.Begin();
				AlarmFadeOut.Begin();
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

		private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
		{
			Alarms.Add(new Classes.Alarm("New Alarm"));
		}

		#endregion

		#region Fades

		private void AddBtn_MouseEnter(object sender, MouseEventArgs e)
		{
			AddFadeIn.Begin();
		}

		private void AddBtn_MouseLeave(object sender, MouseEventArgs e)
		{
			AddFadeOut.Begin();
		}

		private void AlarmFadeOut_Completed(object sender, EventArgs e)
		{
			SettingsFadeIn.Begin();
		}

		private void SettingsFadeOut_Completed(object sender, EventArgs e)
		{
			AlarmFadeIn.Begin();
		}

		private void closeImage_MouseEnter(object sender, MouseEventArgs e)
		{
			CloseFadeIn.Begin();
		}

		private void closeImage_MouseLeave(object sender, MouseEventArgs e)
		{
			CloseFadeOut.Begin();
		}

		private void minimizeImage_MouseEnter(object sender, MouseEventArgs e)
		{
			MinimizeFadeIn.Begin();
		}

		private void minimizeImage_MouseLeave(object sender, MouseEventArgs e)
		{
			MinimizeFadeOut.Begin();
		}

		#endregion
	}
}