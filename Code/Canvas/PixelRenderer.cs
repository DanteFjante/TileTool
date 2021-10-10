using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;

namespace TileMaker.Code.Canvas
{
    public class PixelRenderer : DynamicRenderer
    {
        protected override void OnDraw(DrawingContext drawingContext, StylusPointCollection stylusPoints, Geometry geometry, Brush fillBrush)
        {
            base.OnDraw(drawingContext, stylusPoints, geometry, fillBrush);
        }

        protected override void OnAdded()
        {
            base.OnAdded();
        }

        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            base.OnStylusDown(rawStylusInput);
        }

        protected override void OnStylusMove(RawStylusInput rawStylusInput)
        {
            base.OnStylusMove(rawStylusInput);
        }
    }
}