using System.Windows;
using System.Windows.Media;
using Base.Math;
using Base.Wpf;
using BaseLib;
using static BaseLib.MathFunctions;

namespace TriangulationDemo
{

    public partial class MainWindow : Window
    {
        Vector2d Q1 = vec2(0, -0.8);
        Vector2d Q2 = vec2(0,-0.8);
        Vector2d P1 = vec2(-0.5, -0.95);
        Vector2d P2 = vec2(0.5, -0.95);

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
            dc.LineWidth = 3;
            dc.LineColor = Colors.Black;


            dc.FillColor = Colors.DeepPink;


            var n1 = (Q1 - P1).Normalize();
            var p1Large = P1 + n1 * 2;
            var n2 = (Q2 - P2).Normalize();
            var p2Large = P2 + n2 * 2;

            dc.DrawPoints(P1, P2);

            dc.FillColor = Colors.Blue;
            dc.DrawPoints(Q1, Q2);

            dc.FillColor = Colors.DarkRed;

            dc.DrawLines(P1, p1Large);
            dc.DrawLines(P2, p2Large);

            var intersection = FindIntersection(P1, n1, P2, n2);
            dc.PointSize = 9;
            dc.FillColor = Colors.BlueViolet;

            dc.DrawPoints(intersection);

        }

        public static Vector2d FindIntersection(Vector2d p0, Vector2d n0, Vector2d p1, Vector2d n1)
        {
            var m = new Matrix2
            {
                [0, 0] = 1,
                [1, 0] = -n0 * n1,
                [0, 1] = n0 * n1,
                [1, 1] = -1
            };

            var b = vec2((p1 - p0) * n0, (p1 - p0) * n1);
            var l = m.Inverse() * b;

            return p0 + n0 * l.X;
        }

        void OnValue1Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Q1.X = e.NewValue;

            drawControl?.Controller?.Redraw();
        }

        void OnValue2Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Q2.X = e.NewValue;

            drawControl?.Controller?.Redraw();
        }
    }
}
