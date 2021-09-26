using DSlide;
using System;

namespace TorusGoCore
{
    public enum PositionContentEnum { Empty, BlackStone, WhiteStone }

    public abstract class PositionContents : DataSlideBase
    {
        public Position Position { get; private set; }

        public abstract PositionContentEnum Content { get; set; }

        public void Initialize(Position position)
        {
            this.Position = position;
        }
    }
}
