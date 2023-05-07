using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Update
{
    public class UpdateWhiteTurn : IUpdateble
    {
        private GameModel _gameModel;
        private GameManager _gameManager;
        private List<FreeCell> _freeCells;
        private List<Checker> _availableCheckersToMove;
        public UpdateWhiteTurn(GameModel gameModel, GameManager gameManager)
        {
            _gameModel = gameModel;
            _gameManager = gameManager;
            _freeCells = new List<FreeCell>();
            _availableCheckersToMove = new List<Checker>();
        }

        public void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                (int, int) mousePosition = _gameManager.GetMousePosition();
                if(_availableCheckersToMove.Count == 0) 
                {
                    _availableCheckersToMove = _gameModel.AvailableCheckersToMove(_gameManager.currentUpdate);
                }

                if(mousePosition.Item1 >= 0 && mousePosition.Item1 < _gameModel.Board.Width &&
                    mousePosition.Item2 >= 0 && mousePosition.Item2 < _gameModel.Board.Height)
                {
                    if (_availableCheckersToMove.Count == 0)
                    {
                        _gameManager.ChangeUpdate();
                    }
                    foreach(var freeCell in _freeCells)
                    {
                        if(mousePosition.Item1 == freeCell.X && mousePosition.Item2 == freeCell.Y)
                        {
                            _gameModel.MoveChecker(_gameModel.SelectedChecker, mousePosition.Item1, mousePosition.Item2);
                            _gameModel.EatChecker(_gameModel.SelectedChecker, mousePosition.Item1, mousePosition.Item2);
                            _gameModel.SelectedChecker = _gameModel.Board.Checkers[mousePosition.Item1, mousePosition.Item2];
                            _gameModel.ClearFreeCells(_freeCells);
                            if(_gameModel.isEatenEnemyChecker)
                            {
                                _freeCells = _gameModel.CheckNearEnemyAndGetFreeCells(_gameModel.SelectedChecker);
                                if(_freeCells.Count == 0)
                                {
                                    _gameManager.ChangeUpdate();
                                }
                            }
                            else
                            {
                                _gameManager.ChangeUpdate();
                            }
                            return;
                        }
                    }
                    if(!_gameModel.isEatenEnemyChecker)
                    {
                        foreach (var checker in _availableCheckersToMove)
                        {
                            if (mousePosition.Item1 == checker.X && mousePosition.Item2 == checker.Y)
                            {
                                _gameModel.ClearFreeCells(_freeCells);
                                _gameModel.SelectedChecker = null;
                                _gameModel.SelectedChecker = _gameModel.Board.Checkers[checker.X, checker.Y];
                                _freeCells = _gameModel.CheckNearEnemyAndGetFreeCells(_gameModel.SelectedChecker);
                                if (_freeCells.Count == 0)
                                {
                                    _freeCells = _gameModel.GetFreeCellToMove(_gameModel.SelectedChecker);
                                }
                            }
                        }
                    }
                }


                //if(mousePosition.Item1 >= 0 && mousePosition.Item1 < _gameModel.Board.Width &&
                //    mousePosition.Item2 >= 0 && mousePosition.Item2 < _gameModel.Board.Height)
                //{
                //    if (_gameModel.Board.Checkers[mousePosition.Item1, mousePosition.Item2] is WhiteChecker)
                //    {
                //        if (_gameModel.SelectedChecker == null)
                //        {
                //            _gameModel.SelectedChecker = _gameModel.Board.Checkers[mousePosition.Item1, mousePosition.Item2];
                //            _freeCells = _gameModel.CheckNearEnemyAndGetFreeCells(_gameModel.SelectedChecker);
                //        }
                //        else
                //        {
                //            _gameModel.SelectedChecker = null;
                //            _gameModel.ClearFreeCells(_freeCells);

                //            _gameModel.SelectedChecker = _gameModel.Board.Checkers[mousePosition.Item1, mousePosition.Item2];
                //            _freeCells = _gameModel.CheckNearEnemyAndGetFreeCells(_gameModel.SelectedChecker); 
                //        }
                //    }
                //    else if (_gameModel.SelectedChecker != null)
                //    {
                //        if(_freeCells.Count != 0)
                //        {
                //            foreach (var cell in _freeCells)
                //            {
                //                if (mousePosition.Item1 == cell.X && mousePosition.Item2 == cell.Y)
                //                {
                //                    _gameModel.MoveChecker(_gameModel.SelectedChecker, mousePosition.Item1, mousePosition.Item2);
                //                    _gameModel.EatChecker(_gameModel.SelectedChecker, mousePosition.Item1, mousePosition.Item2);
                //                    _gameModel.ClearFreeCells(_freeCells);
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //}
            }
        }
    }
}