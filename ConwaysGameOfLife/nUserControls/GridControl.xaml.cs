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
    /// Interaction logic for GridControl.xaml
    /// </summary>
    public partial class GridControl : UserControl
    {
        public GridControl()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.BuildGrid((int)RowSlider.Value, (int)ColumnSlider.Value, (bool)RoundCellBox.IsChecked);
            //mw.ResizeGrid((int)RowSlider.Value, (int)ColumnSlider.Value);
        }
    }
}
