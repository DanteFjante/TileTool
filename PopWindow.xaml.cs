using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Point = System.Windows.Point;

namespace TileMaker
{
    /// <summary>
    /// Interaction logic for PopWindow.xaml
    /// </summary>
    public partial class PopWindow : UserControl
    {

        private const string largeWindowButtonText = "━";
        private const string smallWindowButtonText = "▢";

        private const int MinimizedHeight = 25;
        private const int MinimizedWidth = 85;

        private bool drag = false;
        private Point lastpos = new Point(0, 0);

        private Popup _PopWin;
        private ContentControl _PopContent;
        private Label _WindowName;
        private Button _Resize;
        private Button _Close;
        private Label _ExtraText;
        private Grid _ColorGrid;

        private DependencyProperty MaximizedProperty = DependencyProperty.Register("Maximized", typeof(bool), typeof(PopWindow));
        private DependencyProperty ExtraTextProperty = DependencyProperty.Register("ExtraText", typeof(string), typeof(PopWindow));
        private DependencyProperty WinColorProperty = DependencyProperty.Register("WindowColor", typeof(Brush), typeof(PopWindow));


        public Brush WindowColor
        {
            get => (Brush) GetValue(WinColorProperty);
            set
            {
                _ColorGrid.Background = value;
                SetValue(WinColorProperty, value);
            }
        }

        public bool Maximized
        {
            get => (bool) GetValue(MaximizedProperty);
            set => SetValue(MaximizedProperty, value);
        }

        public string ExtraText
        {
            get => (string) GetValue(ExtraTextProperty);
            set
            {
                SetValue(ExtraTextProperty, value);
                _ExtraText.Content = value;
            }
        }


        public string Title
        {
            get
            {
                return (string) _WindowName.Content;
            }
            set
            {
                _WindowName.Content = value;
            }
        }



        public PopWindow()
        {
            InitializeComponent();
            ApplyTemplate();
            Init();

            _Resize.Click += (sender, args) =>
            {
                Maximized = !Maximized;

                _PopWin.Height = Maximized ? MinimizedHeight : _PopContent.Height + MinimizedHeight;
                _PopWin.Width = Maximized ? MinimizedWidth : _PopContent.Width + MinimizedWidth;

                FixResizeButtonText();
            };

            _Close.Click += (sender, args) =>
            {
                _PopWin.IsOpen = false;
            };

            _WindowName.MouseMove += (sender, args) =>
            {
                if (!drag && args.LeftButton == MouseButtonState.Pressed)
                {
                    lastpos = args.GetPosition(null);
                    drag = true;
                }

                if (drag && args.LeftButton == MouseButtonState.Released)
                    drag = false;

                if (args.LeftButton == MouseButtonState.Pressed) 
                {
                    Point pos = args.GetPosition(null); ;

                    _PopWin.HorizontalOffset += pos.X - lastpos.X;
                    _PopWin.VerticalOffset += pos.Y - lastpos.Y;
                }

            };
        }
        private void Init()
        {
            _PopWin = (Popup)Template.FindName("PopWin", this);
            _PopContent = (ContentControl)Template.FindName("PopContent", this);
            _Resize = (Button)Template.FindName("Resize", this);
            _Close = (Button)Template.FindName("Close", this);
            _ExtraText = (Label)Template.FindName("ExtraText", this);
            _WindowName = (Label)Template.FindName("WindowName", this);
            _ColorGrid = (Grid) Template.FindName("ColorGrid", this);

            Width = Math.Max(25, Width);
            Height = Math.Max(75, Height);

            _PopWin.IsOpen = true;

            FixResizeButtonText();
        }
        private void FixResizeButtonText()
        {
            if (Maximized)
                _Resize.Content = largeWindowButtonText;
            else
                _Resize.Content = smallWindowButtonText;
        }
    }
}
