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
        BlurEffect blur = (BlurEffect)Application.Current.Resources["BlurEffect"];
		public AlarmView()
		{
			InitializeComponent();
			Storyboard.SetTarget(HeightMod, this);
            Storyboard.SetTarget(BlurAnimIn, blur);
            Storyboard.SetTarget(BlurAnimOut, blur);
			FadeIn.Begin();
		}

		private void FadeOut_Completed_1(object sender, EventArgs e)
		{
			if (RemoveAlarm != null)
				RemoveAlarm(this, new Classes.AlarmArgs(this.DataContext as Classes.Alarm));
		}

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
			ConfirmationDialog d = new ConfirmationDialog();

            FadeOutBlur.Stop();
            FadeInBlur.Stop();

            d.Closed += (send, args) =>
				{
                    FadeOutBlur.Begin();

                    if (d.DialogResult == true)
                    {       
                        FadeOut.Begin();
                    }
				};

            FadeInBlur.Begin();
			d.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TimeEditor editor = new TimeEditor((this.DataContext as Classes.Alarm).AlarmTime);

            FadeOutBlur.Stop();
            FadeInBlur.Stop();

            editor.Closed += (send, args) =>
            {
                FadeOutBlur.Begin();
                if (editor.DialogResult == true)
                {
                    (this.DataContext as Classes.Alarm).AlarmTime = editor.ReturnTime;
                }
            };
            FadeInBlur.Begin();
            editor.Show();
        }
	}
}
