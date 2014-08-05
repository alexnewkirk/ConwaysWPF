using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for GeneratorControl.xaml
    /// </summary>
    public partial class GeneratorControl : UserControl
    {
        private static ObservableCollection<string> patterns = new ObservableCollection<string>(new string[] { "r-pentomino", "glider" });
        public GeneratorControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.GenerateGlider();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.GenerateRPentomino();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.GenerateRPentomino();
            mw.Start(1000/(int)mw.PControl.SpeedSlider.Value);
            mw.StartNoiseGen(2500);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.StopNoiseGen();
            mw.Stop();
        }
    }
}
