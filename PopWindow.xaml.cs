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

namespace TileMaker
{
    /// <summary>
    /// Interaction logic for PopWindow.xaml
    /// </summary>
    public partial class PopWindow : UserControl
    {
        private bool drag = false;
        private Point dragStart;

        public PopWindow()
        {
            InitializeComponent();

            WindowName.MouseMove += (sender, args) =>
            {
                if (args.LeftButton == MouseButtonState.Pressed && !drag)
                    dragStart = PopWin.PlacementRectangle.Location;

                if (args.LeftButton == MouseButtonState.Released && drag)
                    drag = false;

                if (args.LeftButton == MouseButtonState.Pressed)
                {
                    Point pos = args.GetPosition(this);
                    Point p = new Point(dragStart.X + pos.X, dragStart.Y + pos.Y);
                    Rect r = new Rect(p , PopWin.PlacementRectangle.Size);
                    
                    PopWin.PlacementRectangle = r;
                }

            };
        }
    }
}
