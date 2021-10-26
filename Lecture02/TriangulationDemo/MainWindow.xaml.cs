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
using Base.Wpf;
using BaseLib;

namespace TriangulationDemo
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            drawControl.Controller = new DrawController
            {
                MinX = -1,
                MaxX = 1,
                MinY = -1,
                MaxY = 1,
                Draw = Draw
            };
        }

        void Draw(DrawContext dc)
        {
            dc.PointSize = 5;
            //dc.LineWidth = 2;

            dc.FillColor = Colors.DeepPink;
            //dc.LineColor = Colors.DeepPink;
            dc.DrawPoints(new Vector2d(0, 0));
        }

        void OnValue1Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        void OnValue2Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }
    }
}
