using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaADM.Common
{
    class Element
    {
        public int X { get; set; }
        public int Y { get; set; }

        public (int X, int Y) Coordinates { set {
                this.X = value.X;
                this.Y = value.Y;
            } }
        public int Category { get; set; }

        public Element(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo(Element core)
        {
            int dx = core.X - X;
            int dy = core.Y - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
