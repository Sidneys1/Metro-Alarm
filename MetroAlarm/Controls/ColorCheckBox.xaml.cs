using System;
using System.Windows;
using System.Windows.Controls;

namespace MetroAlarm.Controls
{
    public partial class ColorCheckBox : UserControl
    {
        public event EventHandler<EventArgs> CheckChanged;
        bool _isChecked = true;
        public bool IsChecked
        {
            get { return _isChecked; }
            set 
            {
                if (value != _isChecked)
                {
                    _isChecked = value;
                    if (CheckChanged != null)
                        CheckChanged(this, new EventArgs());
                    if (_isChecked)
                        VisualStateManager.GoToState(this, "Checked", true);
                    else
                        VisualStateManager.GoToState(this, "UnChecked", true);
                }
            }
        }

        public ColorCheckBox()
        {
            InitializeComponent();
        }

        public void SetIsCheckedSilent(bool CheckState)
        {
            _isChecked = CheckState;

            if (_isChecked)
                VisualStateManager.GoToState(this, "Checked", true);
            else
                VisualStateManager.GoToState(this, "UnChecked", true);

        }

        private void Border_MouseLeftButtonUp_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsChecked = true;
        }
    }
}
