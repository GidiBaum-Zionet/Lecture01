using System;
using System.Windows.Input;

namespace Base.Wpf
{
    public class DrawController
    {
        public DrawControl DrawControl { get; set; }

        public Action Init { get; set; }
        public Action<DrawContext> Draw { get; set; }
        public Action<int, int> Resize { get; set; }
        public Action Dispose { get; set; }

        public Action<MouseEventArgs> MouseDown { get; set; }
        public Action<MouseEventArgs> MouseUp { get; set; }
        public Action<MouseEventArgs> MouseMove { get; set; }
        public Action<MouseWheelEventArgs> MouseWheel { get; set; }
        public Action MouseEnter { get; set; }
        public Action MouseLeave { get; set; }

        public Action<KeyEventArgs> KeyUp { get; set; }
        public Action<KeyEventArgs> KeyDown { get; set; }

        public void Redraw() => DrawControl?.Redraw();

        public float MinX { get; set; }
        public float MaxX { get; set; }
        public float MinY { get; set; }
        public float MaxY { get; set; }

    }
}
