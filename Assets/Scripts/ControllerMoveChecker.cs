using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts
{
    public class ControllerMoveChecker : IController
    {
        private GameModel _gameModel;
        private Checker _checker;
        private GameView _gameView;

        public ControllerMoveChecker(GameModel gameModel, Checker checker, GameView gameView)
        {
            _gameModel = gameModel;
            _checker = checker;
            _gameView = gameView;
        }

        public void On()
        {
            _checker.CheckerMoved += OnMove;
        }

        public void Off()
        {
            _checker.CheckerMoved -= OnMove;
        }

        private void OnMove(int oldX, int oldY, int newPositionX, int newPositionY)
        {
            if (_gameModel.Board.Checkers[oldX, oldY].IsWhite == _gameModel.isWhiteMove)
            {
                CheckerPrefab checkerPrefab = _gameView.Checkers[oldX, oldY];
                _gameView.Checkers[oldX, oldY] = null;
                checkerPrefab.Transform.position = new Vector2(newPositionX, newPositionY);
                _gameView.Checkers[newPositionX, newPositionY] = checkerPrefab;


                Checker checker = _gameModel.Board.Checkers[oldX, oldY];
                _gameModel.Board.Checkers[newPositionX, newPositionY] = checker;
                _gameModel.Board.Checkers[oldX, oldY] = null;
            }

            if(_gameModel.Board.Checkers[newPositionX, newPositionY].IsWhite && newPositionY == _gameModel.Board.Height - 1 ||
                !_gameModel.Board.Checkers[newPositionX, newPositionY].IsWhite && newPositionY == 0)
            {
                _gameModel.Board.Checkers[newPositionX, newPositionY].IsKing = true;
                if(_gameModel.Board.Checkers[newPositionX, newPositionY].IsWhite)
                {
                    _gameView.Checkers[newPositionX, newPositionY].SpriteRenderer.sprite = _gameView.Checkers[newPositionX, newPositionY].WhiteKingSprite;
                }
                else
                {
                    _gameView.Checkers[newPositionX, newPositionY].SpriteRenderer.sprite = _gameView.Checkers[newPositionX, newPositionY].BlackKingSprite;
                }
            }
        }
    }
}