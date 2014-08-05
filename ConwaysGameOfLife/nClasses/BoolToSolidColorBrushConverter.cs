using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ConwaysGameOfLife.nClasses
{
    class BoolToBrushConverter : IValueConverter
    {

        private static Color _liveColor = Color.FromArgb(255,0, 220, 255);
        private static Color _deadColor = Color.FromArgb(230, 0, 0, 0);
        private static Color[] roygbv = new Color[8] {Colors.Red, Colors.Orange, Colors.Yellow, Colors.Lime, Colors.DeepSkyBlue, Colors.DarkViolet, Colors.DeepPink, Colors.DodgerBlue};
        private static SolidColorBrush[] roygbvBrush = new SolidColorBrush[8];
        private static SolidColorBrush _liveBrush = new SolidColorBrush(_liveColor);
        private static SolidColorBrush _deadBrush = new SolidColorBrush(_deadColor);
        private static Random gen = new Random();
        private static bool _raveMode = false;

        public BoolToBrushConverter()
        {
            for (int i = 0; i < roygbv.Count(); i++)
            {
                roygbvBrush[i] = new SolidColorBrush(roygbv[i]);
            }
        }

        public bool RaveMode
        {
            get
            {
                return _raveMode;
            }
            set
            {
                _raveMode = value;
            }
        }
        public Color LiveColor
        {
            get
            {
                return _liveColor;
            }
            set
            {
                _liveColor = value;
                _liveBrush = new SolidColorBrush(_liveColor);
                _liveBrush.Freeze();
            }
        }
        public Color DeadColor
        {
            get
            {
                return _deadColor;
            }
            set
            {
                _deadColor = value;
                _deadBrush = new SolidColorBrush(_deadColor);
                _deadBrush.Freeze();
            }
        }

        public object Convert(object value, Type SolidColorBrush, object parameter, CultureInfo culture)
        {
            if (!_raveMode)
            {
                if ((bool)value)
                {
                    return _liveBrush;
                }
                else
                {
                    return _deadBrush;
                }
            }
            else
            {
                if ((bool)value)
                {
                    return roygbvBrush[gen.Next(6)];
                }
                else
                {
                    return _deadBrush;
                }
            }
        }

        public object ConvertBack(object value, Type SolidColorBrush, object parameter, CultureInfo culture)
        {
            SolidColorBrush b = (SolidColorBrush)value;
            Color c = b.Color;
            if (c == _liveColor)
            {
                return true;
            }
            return false;
        }

    }
}
