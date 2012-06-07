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
	public partial class TimeEditor : ChildWindow
	{
        public Classes.Time ReturnTime = null;

		public TimeEditor(Classes.Time args = null)
		{
			InitializeComponent();
            if (args != null)
            {
                if (args.Hour > 12)
                {
                    HrUpDwn.Value = args.Hour - 12;
                    PM.isChecked = true;
                }
                else if (args.Hour == 0)
                {
                    HrUpDwn.Value = 12;
                    PM.isChecked = true;
                }
                else
                    HrUpDwn.Value = args.Hour;
                Min10UpDwn.Value = args.Minute / 10;
                Min1UpDwn.Value = args.Minute % 10;
            }
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
            ReturnTime = new Classes.Time(HrUpDwn.Value + (PM.isChecked ? 12 : 0), Min10UpDwn.Value * 10 + Min1UpDwn.Value, 0);
			this.DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}

        private void CheckLabel_CheckChanged_1(object sender, EventArgs e)
        {
            if (sender == AM)
                PM.SilentCheckState(false);
            else if (sender == PM)
                AM.SilentCheckState(false);
        }
	}
}

