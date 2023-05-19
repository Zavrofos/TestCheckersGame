using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ControllerEatingCheckers : IController
    {
        private Checker _checker;
        private GameModel _gameModel;
        private GameView _gameView;

        public ControllerEatingCheckers(Checker checker, GameModel gameModel, GameView gameView)
        {
            _checker = checker;
            _gameModel = gameModel;
            _gameView = gameView;
        }

        public void On()
        {
            _checker.EatedChecker += OnEat;
        }

        private void OnEat(int oldPositionX, int oldPositionY, int newPositionX, int newPositionY)
        {
            Checker checker = _gameModel.Board.Checkers[newPositionX, newPositionY];
            int x = 0;
            int y = 0;
            if (oldPositionX > newPositionX && oldPositionY < newPositionY)
            {
                int distance = oldPositionX - newPositionX;
                for(int i = 1; i < distance; i++)
                {
                    x = oldPositionX - i;
                    y = oldPositionY + i;
                    if(_gameModel.Board.Checkers[x, y] != null)
                    {
                        break;
                    }
                }
            }
            else if (oldPositionX < newPositionX && oldPositionY < newPositionY)
            {
                int distance = newPositionX - oldPositionX;
                for (int i = 1; i < distance; i++)
                {
                    x = oldPositionX + i;
                    y = oldPositionY + i;
                    if (_gameModel.Board.Checkers[x, y] != null)
                    {
                        break;
                    }
                }
            }
            else if (oldPositionX > newPositionX && oldPositionY > newPositionY)
            {

                int distance = oldPositionX - newPositionX;
                for (int i = 1; i < distance; i++)
                {
                    x = oldPositionX - i;
                    y = oldPositionY - i;
                    if (_gameModel.Board.Checkers[x, y] != null)
                    {
                        break;
                    }
                }
            }
            else if (oldPositionX < newPositionX && oldPositionY > newPositionY)
            {
                int distance = newPositionX - oldPositionX;
                for (int i = 1; i < distance; i++)
                {
                    x = oldPositionX + i;
                    y = oldPositionY - i;
                    if (_gameModel.Board.Checkers[x, y] != null)
                    {
                        break;
                    }
                }
            }

            if(x != 0 && y != 0)
            {
                if (checker.IsWhite)
                {
                    if (_gameModel.Board.Checkers[x, y] != null && !_gameModel.Board.Checkers[x, y].IsWhite)
                    {
                        Checker enemychecker = _gameModel.Board.Checkers[x, y];
                        _gameModel.Board.Checkers[x, y] = null;
                        _gameView.Destroy(enemychecker);
                        _gameModel.isEatenEnemyChecker = true;
                    }
                }
                else if (!checker.IsWhite)
                {
                    if (_gameModel.Board.Checkers[x, y] != null && _gameModel.Board.Checkers[x, y].IsWhite)
                    {
                        Checker enemychecker = _gameModel.Board.Checkers[x, y];
                        _gameModel.Board.Checkers[x, y] = null;
                        _gameView.Destroy(enemychecker);
                        _gameModel.isEatenEnemyChecker = true;
                    }
                }
            }
        }

        public void Off()
        {
            _checker.EatedChecker -= OnEat;
        }
    }
}