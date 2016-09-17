using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ball_tracker
{
    class ROI
    {
        private Point RectStartPoint;
        private Rectangle rect;

        public void setRecStartPoint(Point point)
        {
            RectStartPoint = point;
        }

        public void setRect(Point point, Size size)
        {
            rect.Location = point;
            rect.Size = size;
        }

        public Point getRectStartPoint()
        {
            return RectStartPoint;
        }

        public Rectangle getRect()
        {
            return rect;
        }
    }
}
