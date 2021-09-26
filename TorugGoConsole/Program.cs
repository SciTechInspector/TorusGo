using DSlide;
using System;
using System.ComponentModel;
using System.Linq;
using TorusGoCore;

namespace TorugGoConsole
{
    public abstract class GameView : DataSlideBase
    {
        public abstract GameManager GameManager { get; set; }
        public virtual string CurrentView => GetCurrentView();

        public void Initialize()
        {
            var gameManager = base.dataManager.CreateInstance<GameManager>();
            gameManager.Initialize();

            this.GameManager = gameManager;
        }

        public string GetCurrentView()
        {
            var stringToDisplay = "";

            for (int y = 0; y < this.GameManager.CurrentGame.YSize; y++)
            {
                for (int x = 0; x < this.GameManager.CurrentGame.XSize; x++)
                {
                    var character = "+";
                    var content = this.GameManager.CurrentGame.GetContentAtPosition(x, y);
                    if (content == PositionContentEnum.BlackStone)
                        character = "B";

                    if (content == PositionContentEnum.WhiteStone)
                        character = "W";

                    stringToDisplay += character + " ";
                }
                stringToDisplay += "\r\n";
            }

            if (this.GameManager.CurrentPlayerToPlay.Color == PlayerColor.Black)
                stringToDisplay += "Black to Play:\r\n";
            else
                stringToDisplay += "White to Play:\r\n";

            return stringToDisplay;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var currentDataManager = DataManager.Current;

            currentDataManager.EnterEditMode();
            var gameView = currentDataManager.CreateInstance<GameView>();
            gameView.Initialize();
            currentDataManager.ExitEditMode();
            currentDataManager.SendChangeNotifications();

            gameView.PropertyChanged += GameView_PropertyChanged;

            Console.WriteLine(gameView.CurrentView);

            var newLine = Console.ReadLine();

            var move = newLine.Split(' ').Select(x => int.Parse(x)).ToList();
            var position = new Position(move[0], move[1]);
            gameView.GameManager.Play(position);
        }

        private static void GameView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GameView.CurrentView))
            {
                Console.Clear();
                var gameView = sender as GameView;
                Console.Write(gameView.CurrentView);
                var newLine = Console.ReadLine();

                var move = newLine.Split(' ').Select(x => int.Parse(x)).ToList();
                var position = new Position(move[0], move[1]);
                gameView.GameManager.Play(position);
            }
        }
    }
}
