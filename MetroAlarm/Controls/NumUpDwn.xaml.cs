using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MetroAlarm.Controls
{
    public partial class NumUpDwn : UserControl
    {
        int _maxValue = 12;
        public int MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                if (_value > _maxValue)
                    Value = _maxValue;
            }
        }

        int _minValue = 0;
        public int MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                if (_value < _minValue)
                    Value = _minValue;
            }
        }

        public event EventHandler<EventArgs> ValueChanged;

        int _value = 1;
        public int Value 
        {
            get { return _value; }
            set 
            {
                if (value != _value)
                {
                    bool dir = value > _value;
                    _value = value;

                    if (ValueChanged != null)
                        ValueChanged(this, new EventArgs());

                    if (dir)
                    {
                        if (DownOut.GetCurrentState() == ClockState.Active)
                        {
                            DownOut.SkipToFill();
                            DownOut_Completed(this, null);
                        }
                        else
                            DownOut.Begin();
                    }
                    else
                    {
                        if (UpOut.GetCurrentState() == ClockState.Active)
                        {
                            UpOut.SkipToFill();
                            UpOut_Completed(this, null);
                        }
                        else
                            UpOut.Begin();
                    }
                }
            }
        }

        public NumUpDwn()
        {
            InitializeComponent();
        }

        private void DownOut_Completed(object sender, EventArgs e)
        {
            label.Content = Value.ToString();
            UpIn.Stop();
            UpIn.Begin();
        }

        private void UpOut_Completed(object sender, EventArgs e)
        {
            label.Content = Value.ToString();
            DownIn.Stop();
            DownIn.Begin();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Value < MaxValue)
                Value++;
            else
                Value = MinValue;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Value > MinValue)
                Value--;
            else
                Value = MaxValue;
        }
    }
}
