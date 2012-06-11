using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace MetroAlarm.Classes
{
    public class ColorResourceWrapper : DependencyObject, INotifyPropertyChanged
    {
        private Color color = Color.FromArgb(255, 00, 174, 255);
        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public static readonly DependencyProperty BouyancyProperty = DependencyProperty.RegisterAttached(
            "Color",
            typeof(Color),
            typeof(ColorResourceWrapper),
            new PropertyMetadata(Color.FromArgb(255, 00, 174, 255))
        );

    }
}
