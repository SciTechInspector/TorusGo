using DSlide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorusGoCore
{
    public enum PlayerColor { Black, White, Unknown }

    public abstract class Player : DataSlideBase
    {
        public abstract GameManager CurrentGameManager { get; set; }
        public abstract string Username { get; set; }
        public virtual GameBoard CurrentGame => CurrentGameManager.CurrentGame;
        public virtual PlayerColor Color
        {
            get
            {
                if (this.CurrentGameManager.BlackPlayer == this)
                    return PlayerColor.Black;

                if (this.CurrentGameManager.WhitePlayer == this)
                    return PlayerColor.White;

                return PlayerColor.Unknown;
            }
        }

        public void Initialize(GameManager gameManager)
        {
            this.CurrentGameManager = gameManager;
        }
    }


    public abstract class GameManager : DataSlideBase
    {
        public abstract GameBoard CurrentGame { get; set; }
        public abstract Player CurrentPlayerToPlay { get; set; }

        public abstract Player BlackPlayer { get; set; }

        public abstract Player WhitePlayer { get; set; }

        public void Initialize()
        {
            var currentGame = base.dataManager.CreateInstance<GameBoard>();
            currentGame.Initialize(18, 18);

            this.CurrentGame = currentGame;

            var blackPlayer = base.dataManager.CreateInstance<Player>();
            var whitePlayer = base.dataManager.CreateInstance<Player>();

            blackPlayer.Initialize(this);
            whitePlayer.Initialize(this);

            this.BlackPlayer = blackPlayer;
            this.WhitePlayer = whitePlayer;

            this.CurrentPlayerToPlay = blackPlayer;
        }

        public void Play(Position position)
        {
            base.dataManager.EnterEditMode();
            CurrentGame.SetContentAtPosition(position, CurrentPlayerToPlay.Color == PlayerColor.Black ? PositionContentEnum.BlackStone : PositionContentEnum.WhiteStone);
            this.CurrentPlayerToPlay = this.CurrentPlayerToPlay == this.BlackPlayer ? this.WhitePlayer : this.BlackPlayer;
            base.dataManager.ExitEditMode();
            base.dataManager.SendChangeNotifications();
        }
    }
}
