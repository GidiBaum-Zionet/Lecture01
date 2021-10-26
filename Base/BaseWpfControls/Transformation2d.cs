using System.Collections.Generic;
using System.Linq;
using BaseLib;

namespace Base.Wpf
{
    public class Transformation2d
    {
        double _ScaleX = 1;
        double _ScaleY = 1;
        double _X0;
        double _Y0;

        public void SetX(double min, double max, double winWidth)
        {
            _X0 = min;
            _ScaleX = winWidth / (max - min);
        }
        public void SetY(double min, double max, double winHeight)
        {
            _Y0 = max;
            _ScaleY = winHeight / (max - min);
        }

        public Vector2d Apply
            (Vector2d v) => new((v.X - _X0) * _ScaleX, (_Y0 - v.Y) * _ScaleY);
        public IEnumerable<Vector2d> Apply(IEnumerable<Vector2d> v) => v.Select(Apply);
    }
}