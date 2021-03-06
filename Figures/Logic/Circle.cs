using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures.Logic
{
    internal class Circle : Figure
    {
        private int radius;
        private int squaredRadius;
        private int diameter;

        public Circle(float x, float y)
        {
            posX = x;
            posY = y;
            SetRadius(5);
        }

        public override bool IsInside(float x, float y)
        {
            var dx = x - posX;
            var dy = y - posY;

            if (dx * dx + dy * dy <= squaredRadius) return true;

            return false;
        }

        public override void Draw(Graphics g)
        {
            var x0 = posX - radius;
            var y0 = posY - radius;

            Pen pen = Pens.Black;

            if (isSelected) pen = Pens.Green;

            g.DrawEllipse(pen, x0, y0, diameter, diameter);
        }

        /// <summary>
        /// Set radius for circle
        /// </summary>
        /// <param name="r">Radius</param>
        public void SetRadius(int r)
        {
            radius = r;
            squaredRadius = radius * radius;
            diameter = radius * 2;
        }
    }
}
