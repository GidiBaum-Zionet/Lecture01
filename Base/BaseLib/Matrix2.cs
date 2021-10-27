using BaseLib;
using static BaseLib.MathFunctions;

namespace Base.Math
{
    public class Matrix2
    {
        double[,] _M = new double[2, 2];

        public double this[int i, int j]
        {
            get => _M[i, j];
            set => _M[i, j] = value;
        }

        public double Det() => _M[0, 0] * _M[1, 1] - _M[0, 1] * _M[1, 0];

        public static Vector2d operator *(Matrix2 m, Vector2d v) =>
            vec2(m[0, 0] * v.X + m[1, 0] * v.Y, m[0, 1] * v.X + m[1, 1] * v.Y);

        public static Matrix2 operator *(Matrix2 m, double s)
        {
            return new Matrix2
            {
                [0, 0] = m[0, 0] * s, [1, 0] = m[1, 0] * s,
                [0, 1] = m[0, 1] * s, [1, 1] = m[1, 1] * s
            };
        }

        public Matrix2 Inverse() =>
            new Matrix2
            {
                [0, 0] = _M[1, 1],
                [1, 0] = -_M[1, 0],
                [0, 1] = -_M[0, 1],
                [1, 1] = _M[0, 0]
            } * (1 / Det());
    }
}
