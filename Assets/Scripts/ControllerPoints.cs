using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ControllerPoints : IController
    {
        private GameModel _gameModel;
        private GameView _gameView;

        public ControllerPoints(GameModel gameModel, GameView gameView)
        {
            _gameModel = gameModel;
            _gameView = gameView;
        }
        public void On()
        {
            _gameModel.PointsModel.AddedPoint += OnAddPoints;
        }
        public void Off()
        {
            _gameModel.PointsModel.AddedPoint -= OnAddPoints;
        }

        private void OnAddPoints(Checker checker)
        {
            if(checker.IsWhite)
            {
                _gameModel.PointsModel.BlackPlayerPoints++;
                _gameView.BlackPlayerPoints.text = $"x {_gameModel.PointsModel.BlackPlayerPoints}";
            }
            else
            {
                _gameModel.PointsModel.WhiteplayerPoints++;
                _gameView.WhitePlayerPoints.text = $"x {_gameModel.PointsModel.WhiteplayerPoints}";
            }
        }
    }
}