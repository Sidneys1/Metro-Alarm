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
		public TimeEditor()
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

        private void CheckLabel_CheckChanged_1(object sender, EventArgs e)
        {
            if (sender == AM)
                PM.SilentCheckState(false);
            else if (sender == PM)
                AM.SilentCheckState(false);
        }
	}
}

