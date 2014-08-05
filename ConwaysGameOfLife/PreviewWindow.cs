using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ConwaysGameOfLife
{
    public class PreviewWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SolidColorBrush _previewColorBrush;// = new SolidColorBrush(Color.FromArgb(255,255,255,255));
        public SolidColorBrush PreviewColorBrush
        {
            get
            {
                return _previewColorBrush;
            }
            set
            {
                _previewColorBrush = value;
                FirePropertyChanged("PreviewColorBrush");
                //MessageBox.Show("R: " + PreviewColor.R);
                
            }
        }

        private Color _previewColor;
        public Color PreviewColor
        {
            get
            {
                return _previewColor;
            }
            set
            {
                _previewColor = value;
                PreviewColorBrush = new SolidColorBrush(_previewColor);

            }
        }

        private byte _red;
        public byte Red
        {
            get
            {
                return _red;
            }
            set
            {
                byte g = (byte)PreviewColor.G;
                byte b = (byte)PreviewColor.B;
                byte a = (byte)PreviewColor.A;
                _red = value;
                PreviewColor = Color.FromArgb(a, _red, g, b);
            }
        }

        private byte _green;
        public byte Green
        {
            get
            {
                return _green;
            }
            set
            {
                byte r = (byte)PreviewColor.R;
                byte b = (byte)PreviewColor.B;
                byte a = (byte)PreviewColor.A;
                _green = value;
                PreviewColor = Color.FromArgb(a, r, _green, b);
            }
        }

        private byte _blue;
        public byte Blue
        {
            get
            {
                return _blue;
            }
            set
            {
                byte r = (byte)PreviewColor.R;
                byte g = (byte)PreviewColor.G;
                byte a = (byte)PreviewColor.A;
                _blue = value;
                PreviewColor = Color.FromArgb(a, r, g, _blue);
            }
        }

        private byte _alpha;
        public byte Alpha
        {
            get
            {
                return _alpha;
            }
            set
            {
                byte r = (byte)PreviewColor.R;
                byte g = (byte)PreviewColor.G;
                byte b = (byte)PreviewColor.B;
                _alpha = value;
                PreviewColor = Color.FromArgb(_alpha, r, g, b);
            }
        }

        public void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                //MessageBox.Show(propertyName);
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
