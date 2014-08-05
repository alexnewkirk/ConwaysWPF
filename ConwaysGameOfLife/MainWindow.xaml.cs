using ConwaysGameOfLife.nClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConwaysGameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Timer _timer = new Timer();
        private Timer _noisegenTimer = new Timer();
        private bool[,] _tempCopy = new bool[100,100];
        private Random gen = new Random();

        public Random Gen
        {
            get { return gen; }
            set { gen = value; }
        }
        private int _rowMax, _colMax;
        private BoolToBrushConverter bc = (BoolToBrushConverter)Application.Current.Resources["boolConv"];
        private List<List<Cell>> _grid = new List<List<Cell>>();
        private bool _highLifeMode = false;
        public bool HighLifeMode
        {
            get
            {
                return _highLifeMode;
            }
            set
            {
                _highLifeMode = value;
            }
        }
        public List<List<Cell>> Grid
        {
            get
            {
                return _grid;
            }
            set
            {
                _grid = value;
            }
        }
        private List<List<Shape>> _shapeGrid = new List<List<Shape>>();
        public List<List<Shape>> ShapeGrid
        {
            get
            {
                return _shapeGrid;
            }
            set
            {
                _shapeGrid = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            GControl.DataContext = this;
            PControl.DataContext = this;
            RControl.DataContext = this;
            UniGrid.Background = new SolidColorBrush(Colors.Black);
            BuildGrid(50, 70, true);
        }

        public void ResizeGrid(int rows, int columns)
        {
            UniGrid.Children.Clear();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    UniGrid.Children.Add(ShapeGrid.ElementAt(i).ElementAt(j));
                }
            }
        }

        public void BuildGrid(int rows, int columns, bool roundCells)
        {
            _rowMax = rows;
            _colMax = columns;
            Stop();
            Clear_Button_Click(null, null);
            Grid = new List<List<Cell>>();
            _tempCopy = new bool[100, 100];
            ShapeGrid = new List<List<Shape>>();
            UniGrid.Rows = rows;
            UniGrid.Columns = columns;
            for (int i = 0; i < rows; i++)
            {
                List<Cell> currentRow = new List<Cell>();
                Grid.Add(currentRow);
                List<Shape> currentRecRow = new List<Shape>();
                ShapeGrid.Add(currentRecRow);
                SolidColorBrush outline = new SolidColorBrush(Colors.Black);
                for (int j = 0; j < columns; j++)
                {
                    Cell c = new Cell();
                    currentRow.Add(c);
                    c.X = i;
                    c.Y = j;
                    _tempCopy[i, j] = false;
                    Shape r = roundCells ? (Shape)new Ellipse() : new Rectangle();
                    int recWidth = roundCells ? 20 : 200;
                    int recHeight = roundCells ? 20 : 200;
                    r.Margin = new Thickness(0.02);
                    r.Width = recWidth;
                    r.Height = recHeight;
                    r.MouseLeftButtonDown += r_MouseLeftButtonDown;
                    r.Stroke = outline;
                    Binding currentBinding = new Binding();
                    currentBinding.Source = Grid.ElementAt(i).ElementAt(j);
                    currentBinding.Path = new PropertyPath("IsAlive");
                    currentBinding.Mode = BindingMode.TwoWay;
                    currentBinding.Converter = (BoolToBrushConverter)Application.Current.FindResource("boolConv");
                    r.SetBinding(Shape.FillProperty, currentBinding);
                    currentRecRow.Add(r);
                }
                
            }
            for (int i = 0; i < Grid.Count; i++)
            {
                for (int j = 0; j < Grid.ElementAt(0).Count; j++)
                {
                    Cell c = Grid.ElementAt(i).ElementAt(j);
                    BuildNeighborList(c, i, j);
                }
            }
            ResizeGrid(rows, columns);
        }

        private void r_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shape r = (Shape)sender;
            SolidColorBrush b = (SolidColorBrush)r.Fill;
            SolidColorBrush newBrush;
            if (b.Color != bc.DeadColor)
            {
                newBrush = new SolidColorBrush(bc.DeadColor);
            }
            else
            {
                newBrush = new SolidColorBrush(bc.LiveColor);
            }
            newBrush.Freeze();
           r.Fill = newBrush;
        }

        public void SetLiveCellColor(Color b)
        {
            Clear_Button_Click(null, null);
            bc.LiveColor = b;
            BuildGrid(_rowMax, _colMax, (bool)GControl.RoundCellBox.IsChecked);
            UniGrid.Children.Clear();
            BuildGrid(_rowMax, _colMax, (bool)GControl.RoundCellBox.IsChecked);
        }

        public void SetDeadCellColor(Color b)
        {
            Clear_Button_Click(null, null);
            bc.DeadColor = b;
            UniGrid.Children.Clear();
            BuildGrid(_rowMax, _colMax, (bool)GControl.RoundCellBox.IsChecked);
        }

        public void Start(int interval)
        {
            Stop();
            _timer.Interval = interval;
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true;
        }

        public void Stop()
        {

            if (_timer.Enabled == true)
            {
                _timer.Enabled = false;
            }
            StopNoiseGen();
        }

        public void StartNoiseGen(int interval)
        {
            StopNoiseGen();
            _noisegenTimer.Interval = interval;
            _noisegenTimer.Elapsed += new ElapsedEventHandler(_noisegenTimer_Elapsed);
            _noisegenTimer.Enabled = true;
        }

        public void StopNoiseGen()
        {

            if (_noisegenTimer.Enabled == true)
            {
                _noisegenTimer.Enabled = false;
            }
        }

        private void _noisegenTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int t = Gen.Next(5);
            switch (t)
            {
                case 0:
                    GenerateRPentomino();
                    break;
                default:
                    GenerateGlider();
                    break;
            }
        }

        private void BuildNeighborList(Cell c, int x, int y)
        {
            Cell[] neighbors = new Cell[8];
            int colMax = Grid.Count;
            int rowMax = Grid.ElementAt(0).Count;

            if (x > 0 && x < colMax - 1 && y > 0 && y < rowMax - 1)
            {
                neighbors[0] = Grid.ElementAt(Math.Abs(x - 1) % colMax).ElementAt(Math.Abs(y) % rowMax);
                neighbors[1] = Grid.ElementAt(Math.Abs(x + 1) % colMax).ElementAt(Math.Abs(y) % rowMax);
                neighbors[2] = Grid.ElementAt(Math.Abs(x) % colMax).ElementAt(Math.Abs(y - 1) % rowMax);
                neighbors[3] = Grid.ElementAt(Math.Abs(x) % colMax).ElementAt(Math.Abs(y + 1) % rowMax);
                neighbors[4] = Grid.ElementAt(Math.Abs(x + 1) % colMax).ElementAt(Math.Abs(y + 1) % rowMax);
                neighbors[5] = Grid.ElementAt(Math.Abs(x + 1) % colMax).ElementAt(Math.Abs(y - 1) % rowMax);
                neighbors[6] = Grid.ElementAt(Math.Abs(x - 1) % colMax).ElementAt(Math.Abs(y - 1) % rowMax);
                neighbors[7] = Grid.ElementAt(Math.Abs(x - 1) % colMax).ElementAt(Math.Abs(y + 1) % rowMax);
            }

            c.Neighbors = neighbors;
        }

        public void Tick()
        {
            for (int i = 0; i < Grid.Count; i++ )
            {
                for (int j = 0; j < Grid.ElementAt(0).Count; j++ )
                {
                    _tempCopy[i, j] = Grid.ElementAt(i).ElementAt(j).IsAlive;
                }
            }

            int x, y;
            for (int i = 0; i < Grid.Count; i++)
            {
                for (int j = 0; j < Grid.ElementAt(0).Count; j++)
                {
                    Cell currentCell = Grid.ElementAt(i).ElementAt(j);
                    x = i;
                    y = j;
                    int count = 0;
                    foreach(Cell c in currentCell.Neighbors)
                    {
                        if (c != null && _tempCopy[c.X, c.Y])
                        {
                            
                            count++;
                        }
                    }
                    if (currentCell.IsAlive)
                    {
                        if (count < 2 || count > 3)
                        {
                            currentCell.IsAlive = false;
                        }
                    }
                    else
                    {
                        if (count == 3)
                        {
                            currentCell.IsAlive = true;
                        }
                        if (HighLifeMode && count == 6)
                        {
                            currentCell.IsAlive = true;
                        }
                    }
                }
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Tick();
        }

        public void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            Stop();
            for (int i = 0; i < Grid.Count; i++)
            {
                for (int j = 0; j < Grid.ElementAt(0).Count; j++)
                {
                    Grid.ElementAt(i).ElementAt(j).IsAlive = false;
                }
            }
        }

        private void Random_Button_Click(object sender, RoutedEventArgs e)
        {
            Stop();
            for (int i = 0; i < Grid.Count; i++)
            {
                for (int j = 0; j < Grid.ElementAt(0).Count; j++)
                {
                    Grid.ElementAt(i).ElementAt(j).IsAlive = (gen.Next(3) == 0);
                }
            }
        }

        public void GenerateRPentomino()
        {
            int x = gen.Next(_rowMax - 2) + 1;
            int y = gen.Next(_colMax - 2) + 1;
            List<Cell> pentCells = new List<Cell>();
            pentCells.Add(Grid.ElementAt(x).ElementAt(y));
            pentCells.Add(Grid.ElementAt(x).ElementAt(y + 1));
            pentCells.Add(Grid.ElementAt(x + 1).ElementAt(y + 1));
            pentCells.Add(Grid.ElementAt(x).ElementAt(y - 1));
            pentCells.Add(Grid.ElementAt(x - 1).ElementAt(y));

            foreach (Cell c in pentCells)
            {
                c.IsAlive = true;
            }
        }

        public void GenerateGlider()
        {
             int x = gen.Next(_rowMax - 6) + 3;
            int y = gen.Next(_colMax - 6) + 3;
            List<Cell> gliderCells = new List<Cell>();
            gliderCells.Add(Grid.ElementAt(x).ElementAt(y));

            int direction = gen.Next(4);
            switch (direction)
            {
                case 0:
                    gliderCells.Add(Grid.ElementAt(x).ElementAt(y + 1));
                    gliderCells.Add(Grid.ElementAt(x).ElementAt(y - 1));
                    gliderCells.Add(Grid.ElementAt(x+1).ElementAt(y + 1));
                    gliderCells.Add(Grid.ElementAt(x + 2).ElementAt(y));
                    break;
                case 1:
                    gliderCells.Add(Grid.ElementAt(x - 1).ElementAt(y));
                    gliderCells.Add(Grid.ElementAt(x + 1).ElementAt(y));
                    gliderCells.Add(Grid.ElementAt(x + 1).ElementAt(y + 1));
                    gliderCells.Add(Grid.ElementAt(x).ElementAt(y + 2));
                    break;
                case 2:
                    gliderCells.Add(Grid.ElementAt(x - 1).ElementAt(y));
                    gliderCells.Add(Grid.ElementAt(x + 1).ElementAt(y));
                    gliderCells.Add(Grid.ElementAt(x - 1).ElementAt(y + 1));
                    gliderCells.Add(Grid.ElementAt(x).ElementAt(y + 2));
                    break;
                default:
                    gliderCells.Add(Grid.ElementAt(x).ElementAt(y + 1));
                    gliderCells.Add(Grid.ElementAt(x).ElementAt(y - 1));
                    gliderCells.Add(Grid.ElementAt(x - 1).ElementAt(y + 1));
                    gliderCells.Add(Grid.ElementAt(x - 2).ElementAt(y));
                    break;
            }
           
            
            

            foreach (Cell c in gliderCells)
            {
                c.IsAlive = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S)
            {
                if (ControlColumn.Width.Value == 0)
                {
                    ControlColumn.Width = new GridLength(310);
                }
                else
                {
                    ControlColumn.Width = new GridLength(0);

                }
            }
            if (e.Key == Key.P)
            {
                if (_timer.Enabled)
                {
                    Stop();
                }
                else
                {
                    Start(1000/(int)PControl.SpeedSlider.Value);
                }
            }
            if (e.Key == Key.C)
            {
                Clear_Button_Click(null, null);
            }
            if (e.Key == Key.T)
            {
                Tick();
            }
            if (e.Key == Key.X)
            {
                if ((bool)CControl.RaveModeCheckBox.IsChecked)
                {
                    CControl.RaveModeCheckBox.IsChecked = false;
                }
                else
                {
                    CControl.RaveModeCheckBox.IsChecked = true;
                }
            }
            if (e.Key == Key.R)
            {
                Start(1000/(int)PControl.SpeedSlider.Value);
                GenerateRPentomino();
                StartNoiseGen(2500);
            }
            if (e.Key == Key.G)
            {
                GenerateGlider();
            }
            if (e.Key == Key.Z)
            {
                GenerateRPentomino();
            }
            if (e.Key == Key.Escape)
            {
                Environment.Exit(0);
            }
        }
    }
}
