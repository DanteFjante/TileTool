using System.Windows.Ink;
using System.Windows.Input;

namespace TileMaker.Code.Canvas
{
    public class PixelStroke : Stroke
    {

        public float pixelWidth;
        public float pixelHeight;

        public PixelStroke(StylusPointCollection stylusPoints) : base(stylusPoints)
        {
            AlignStylusPoints(ref stylusPoints);
        }

        public PixelStroke(StylusPointCollection stylusPoints, DrawingAttributes drawingAttributes) : base(stylusPoints, drawingAttributes)
        {
            AlignStylusPoints(ref stylusPoints);
        }

        private void AlignStylusPoints(ref StylusPointCollection stylusPoints)
        {
            for (var i = 0; i < stylusPoints.Count; i++)
            {
                double x = stylusPoints[i].X, y = stylusPoints[i].Y;
                GetPixelPos(ref x, ref y); 
                stylusPoints[i] = new StylusPoint(x, y);
            }
        }

        private  void GetPixelPos(ref double x, ref double y)
        {
            int px = (int) (x * pixelWidth);
            int py = (int) (y * pixelHeight);

            x = px / pixelWidth;
            y = py / pixelHeight;

        }

    }
}