using System;
using System.Drawing;

namespace Manina.Windows.Forms
{
    public class DrawWithBoundsEventArgs : EventArgs
    {
        public Graphics Graphics { get; private set; }
        public Rectangle Bounds { get; private set; }

        public DrawWithBoundsEventArgs(Graphics graphics, Rectangle bounds)
        {
            Graphics = graphics;
            Bounds = bounds;
        }
    }
}
