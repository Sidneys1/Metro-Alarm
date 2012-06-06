using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace MetroAlarm.Controls
{
	public partial class AlarmView : UserControl
	{
		public event EventHandler<Classes.AlarmArgs> RemoveAlarm;

		public AlarmView()
		{
			InitializeComponent();
			Storyboard.SetTarget(HeightMod, this);
			FadeIn.Begin();
		}

		#region Edit Name

		private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
		{
			NameEditBox.Visibility = System.Windows.Visibility.Visible;
			NameEditBox.Focus();
		}

		#endregion

		#region Delete Button

		private void DeleteBtn_MouseEnter(object sender, MouseEventArgs e)
		{
			DeleteFadeIn.Begin();
		}

		private void DeleteBtn_MouseLeave(object sender, MouseEventArgs e)
		{
			DeleteFadeOut.Begin();
		}

		#endregion

		private void DeleteBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ConfirmationDialog d = new ConfirmationDialog();

            BlurEffect blur = new BlurEffect();
            blur.Radius = 10;

            Application.Current.RootVisual.Effect = blur;

            d.Closed += (send, args) =>
				{                
                    Application.Current.RootVisual.Effect = null;

                    if (d.DialogResult == true)
                    {       
                        FadeOut.Begin();
                    }
				};

			d.Show();
		}

		private void FadeOut_Completed_1(object sender, EventArgs e)
		{
			if (RemoveAlarm != null)
				RemoveAlarm(this, new Classes.AlarmArgs(this.DataContext as Classes.Alarm));

		}
	}
}
