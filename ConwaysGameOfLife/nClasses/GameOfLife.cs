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
using System.Windows.Shapes;

namespace ConwaysGameOfLife.nClasses
{
    public class GameOfLife
    {
        private Timer _timer;

        private List<List<Cell>> _grid;
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

        public void BuildGrid(int rows, int columns)
        {

        }

        public void Start()
        {
            _timer = new Timer(1000);//bind this value to speed slider
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true;
        }

        public void Stop()
        {
            _timer.Enabled = false;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }
        
    }
}
