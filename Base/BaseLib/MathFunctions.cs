namespace BaseLib
{
    public static class MathFunctions
    {
        public static double sqrt(double x) => System.Math.Sqrt(x);
        public static Vector2d vec2(double x, double y) => new Vector2d(x, y);
    }
}
