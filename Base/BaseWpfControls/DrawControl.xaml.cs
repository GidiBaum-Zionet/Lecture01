using System.Windows;

namespace Base.Wpf
{
    public partial class DrawControl
    {
        readonly DrawPanel _Panel;
        readonly DrawContext _DrawContext;

        bool _IsInit;
        bool _IsLoaded;

        #region Controller

        public DrawController Controller
        {
            get => (DrawController)GetValue(ControllerProperty);
            set => SetValue(ControllerProperty, value);
        }

        public static readonly DependencyProperty ControllerProperty =
            DependencyProperty.Register("Controller", typeof(DrawController), typeof(DrawControl),
                new FrameworkPropertyMetadata(null, (s, e) => (s as DrawControl)?.SetDrawController()));

        void SetDrawController()
        {
            if (Controller != null)
            {
                Controller.DrawControl = this;

                if (!_IsInit && _IsLoaded)
                {
                    _IsInit = true;
                    Controller?.Init?.Invoke();
                }

            }
        }

        #endregion

        public DrawControl()
        {
            _Panel = new DrawPanel();
            _DrawContext = new DrawContext(_Panel);
            
            InitializeComponent();

            this.Loaded += DrawControl_Loaded;

            AddChild(_Panel);

            _Panel.DrawAction = dc =>
            {
                _DrawContext.DrawingContext = dc;

                if (Controller != null)
                {
                    _DrawContext.Transformation.SetX(Controller.MinX, Controller.MaxX, _Panel.ActualWidth);
                    _DrawContext.Transformation.SetY(Controller.MinY, Controller.MaxY, _Panel.ActualHeight);
                }

                Controller?.Draw?.Invoke(_DrawContext);
            };

        }
        public void Redraw() => _Panel?.InvalidateVisual();

        void DrawControl_Loaded(object sender, RoutedEventArgs e)
        {
            _IsLoaded = true;

            if (!_IsInit)
            {
                _IsInit = true;
                Controller?.Init?.Invoke();
                Controller?.Redraw();
            }
        }
    }
}
