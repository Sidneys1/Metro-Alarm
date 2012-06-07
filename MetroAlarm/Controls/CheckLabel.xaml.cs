using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MetroAlarm
{
	public partial class CheckLabel : UserControl
	{
        public string Text { get; set; }


        public event EventHandler<EventArgs> CheckChanged;
        bool ischecked = false;
        public bool isChecked
        {
            get { return ischecked; }
            set
            {
                if (value != ischecked)
                {
                    if (value)
                        VisualStateManager.GoToState(this, "Checked", true);
                    else
                        VisualStateManager.GoToState(this, "Unchecked", true);
                    if (CheckChanged != null)
                        CheckChanged(this, new EventArgs());
                    ischecked = value;
                }
            }
        }

		public CheckLabel()
		{
			// Required to initialize variables
			InitializeComponent();
            DataContext = this;
		}

        private void userControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!ischecked)
                VisualStateManager.GoToState(this, "MouseOver", true);
        }

        private void userControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!ischecked)
                VisualStateManager.GoToState(this, "Unchecked", true);
        }

        private void userControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!ischecked)
            {
                VisualStateManager.GoToState(this, "MouseDown", true);
                this.CaptureMouse();
            }
        }

        private void userControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
            isChecked = true;
        }

        public void SilentCheckState(bool check)
        {
            ischecked = check;

            if (ischecked)
                VisualStateManager.GoToState(this, "Checked", true);
            else
                VisualStateManager.GoToState(this, "Unchecked", true);
        }
	}
}