using ConwaysGameOfLife.nClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConwaysGameOfLife.nUserControls
{
    /// <summary>
    /// Interaction logic for ColorControl.xaml
    /// </summary>
    public partial class ColorControl : UserControl
    {
        private BoolToBrushConverter bc = (BoolToBrushConverter)Application.Current.FindResource("boolConv");
        private PreviewWindow _P = new PreviewWindow();
        public PreviewWindow P
        {
            get
            {
                return _P;
            }
            set
            {
                _P = value;
            }
        }

        public ColorControl()
        {
            InitializeComponent();
            this.DataContext = P;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.SetLiveCellColor(P.PreviewColor);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.SetDeadCellColor(P.PreviewColor);
        }

        private void RaveModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.Clear_Button_Click(null,null);
            bc.RaveMode = true;
        }

        private void RaveModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.Clear_Button_Click(null,null);
            bc.RaveMode = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.UniGrid.Background = P.PreviewColorBrush;
        }

    }
}
