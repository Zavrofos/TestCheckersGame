using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ControllerMoveChecker : IController
    {
        private GameModel _gameModel;
        private GameView _gameView;

        public ControllerMoveChecker(GameModel gameModel, GameView gameView)
        {
            _gameModel = gameModel;
            _gameView = gameView;
        }

        public void On()
        {
            _gameModel.CheckerMoved += OnMove;
        }

        private void OnMove(Checker checker, int newPositionX, int newPositionY)
        {

            Transform transform = _gameView.Checkers[checker.X, checker.Y];
            transform.position = new Vector2(newPositionX, newPositionY);

            _gameView.Checkers[checker.X, checker.Y] = null;
            _gameView.Checkers[newPositionX, newPositionY] = transform;
        }

        public void Off()
        {
            _gameModel.CheckerMoved -= OnMove;
        }

    }
}