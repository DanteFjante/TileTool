using System.Windows.Controls;
using System.Windows.Input.StylusPlugIns;

namespace TileMaker.Code.Canvas
{
    public class PixelCanvas : InkCanvas
    {
        public PixelCanvas() : base()
        {
            DynamicRenderer = new PixelRenderer();
        }
    }
}