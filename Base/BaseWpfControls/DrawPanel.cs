using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Base.Wpf
{
    class DrawPanel : Panel
    {
        public Action<DrawingContext> DrawAction { get; set; }

        protected override void OnRender(DrawingContext dc)
        {
            DrawAction?.Invoke(dc);
        }

    }
}