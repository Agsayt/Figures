using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures.Logic
{
    abstract class Figure
    {
        public float posX, posY;
        public bool isSelected;
        public int localId;
        public string name;

        /// <summary>
        /// Check if point is inside the figure
        /// </summary>
        /// <param name="x">Point x</param>
        /// <param name="y">Point y</param>
        /// <returns>Bool result</returns>
        public abstract bool IsInside(float x, float y);

        /// <summary>
        /// Draw figure on object
        /// </summary>
        /// <param name="g">Graphic of object</param>
        public abstract void Draw(Graphics g);

       
        override
        public String ToString()
        {
            return name;
        }
    }
}
