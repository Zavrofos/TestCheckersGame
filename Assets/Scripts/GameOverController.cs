using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameOverController : IController
    {
        private GameModel _gameModel;
        private GameView _gameView;

        public GameOverController(GameModel gameModel, GameView gameView)
        {
            _gameModel = gameModel;
            _gameView = gameView;
        }

        public void On()
        {
            _gameModel.GameOverModel.CheckedGameOver += OnCheckGameOverAvailableChecker;
        }
        public void Off()
        {
            _gameModel.GameOverModel.CheckedGameOver -= OnCheckGameOverAvailableChecker;
        }

        private void OnCheckGameOverAvailableChecker(List<Checker> availableCheckersToMove, bool isWhiteMove)
        {
            if(availableCheckersToMove.Count == 0)
            {
                if(isWhiteMove)
                {
                    _gameView.GameOverMenu.gameObject.SetActive(true);
                    _gameView.PlayerWinText.text = "Black Win";
                    _gameView.PointsCanvas.gameObject.SetActive(false);
                    _gameModel.isGameOver = true;
                }
                else
                {
                    _gameView.GameOverMenu.gameObject.SetActive(true);
                    _gameView.PlayerWinText.text = "White Win";
                    _gameView.PointsCanvas.gameObject.SetActive(false);
                    _gameModel.isGameOver = true;
                }
            }
        }
    }
}