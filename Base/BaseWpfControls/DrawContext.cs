using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Base.Wpf.Extensions;
using BaseLib;

namespace Base.Wpf
{
    public class DrawContext
    {
        public DrawingContext DrawingContext { get; set; }

        Pen _CurrentPen;
        Brush _CurrentBrush;

        public float PointSize { get; set; } = 5;

        public Transformation2d Transformation { get; } = new();


        #region Property: LineWidth

        float _LineWidth = 1;

        public float LineWidth
        {
            get => _LineWidth;
            set
            {
                if (_LineWidth != value) return;
                _LineWidth = value;
                _CurrentPen = new Pen(new SolidColorBrush(LineColor), LineWidth);
            }
        }

        #endregion

        #region Property: FillColor

        Color _FillColor = Colors.Black;
        public Color FillColor
        {
            get => _FillColor;
            set
            {
                if (_FillColor != value)
                {
                    _FillColor = value;
                    _CurrentBrush = new SolidColorBrush(value);
                }
            }
        }
        #endregion

        #region Property: LineColor

        Color _LineColor = Colors.Black;
        public Color LineColor
        {
            get => _LineColor;
            set
            {
                if (_LineColor != value)
                {
                    _LineColor = value;
                    _CurrentPen = new Pen(new SolidColorBrush(_LineColor), LineWidth);
                }
            }
        }
        #endregion

        Visual _Visual;

        public DrawContext(Visual visual)
        {
            _Visual = visual;

            LineColor = Colors.Black;
            FillColor = Colors.Black;
        }

        public void DrawPoints(IEnumerable<Vector2d> points) => DrawPoints(points.ToArray());

        public void DrawPoints(params Vector2d[] points)
        {
            foreach (var p in points.Transform(Transformation))
            {
                DrawingContext.DrawEllipse(_CurrentBrush, null, p.ToPoint(), PointSize, PointSize);
            }
        }

        public void DrawLines(IEnumerable<Vector2d> points) => DrawLines(points.ToArray());

        public void DrawLines(params Vector2d[] points)
        {
            var p0 = points[0].Transform(Transformation).ToPoint();

            for (var i = 1; i < points.Length; i++)
            {
                var p = points[i].Transform(Transformation).ToPoint();
                DrawingContext.DrawLine(_CurrentPen, p0, null, p, null);
                p0 = p;
            }
        }

        public void DrawLineLoop(IEnumerable<Vector2d> positions) => DrawLineLoop(positions.ToArray());

        public void DrawLineLoop(params Vector2d[] positions)
        {
            var points = positions.Select(p => p.Transform(Transformation).ToPoint()).ToList();
            var n = points.Count;

            for (var i = 0; i < n; i++)
            {
                DrawingContext.DrawLine(_CurrentPen, 
                    points[i], null, points[(i+1)%n], null);
            }
        }

        public void DrawText(Vector2d position, string text,
            Color color, double fontSize = 18)
        {
            var p = position.Transform(Transformation).ToPoint();

            var formattedText = new FormattedText(text,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                18,
                new SolidColorBrush(color),
                VisualTreeHelper.GetDpi(_Visual).PixelsPerDip);

            DrawingContext.DrawText(formattedText, p);
        }

    }
}