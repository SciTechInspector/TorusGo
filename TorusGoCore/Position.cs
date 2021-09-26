using DSlide;
using System;

namespace TorusGoCore
{
    public class Position
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
