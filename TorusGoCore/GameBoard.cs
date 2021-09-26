using DSlide;
using System;

namespace TorusGoCore
{
    public class GameBoard : DataSlideBase
    {
        public int XSize { get; private set; }
        public int YSize { get; private set; }

        public DataSlideCollection<PositionContents> PositionContents { get; private set; }

        public void Initialize(int xSize, int ySize)
        {
            var positionContents = new DataSlideCollection<PositionContents>();
            this.XSize = xSize;
            this.YSize = ySize;
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    var contents = base.dataManager.CreateInstance<PositionContents>();
                    contents.Initialize(new Position(x, y));
                    positionContents.Add(contents);
                }
            }

            this.PositionContents = positionContents;
        }

        public int GetIndexForPosition(Position position)
        {
            return position.Y * this.YSize + position.X;
        }

        public Position GetPositionForIndex(int index)
        {
            return new Position(index / this.YSize, index % this.YSize);
        }

        public void SetContentAtPosition(Position position, PositionContentEnum content)
        {
            var index = GetIndexForPosition(position);
            PositionContents[index].Content = content;
        }

        public PositionContentEnum GetContentAtPosition(int x, int y)
        {
            var index = GetIndexForPosition(new Position(x, y));
            return PositionContents[index].Content;
        }
    }
}
