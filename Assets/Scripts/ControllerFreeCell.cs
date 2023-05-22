using System;
using System.Collections;
using UnityEngine;


public class ControllerFreeCell : IController
{
    private GameModel _gameModel;
    private GameView _gameView;

    public ControllerFreeCell(GameModel gameModel, GameView gameView)
    {
        _gameModel = gameModel;
        _gameView = gameView;
    }

    public void On()
    {
        _gameModel.Board.FreeCellAdded += OnFreeCellAdded;
        _gameModel.Board.FreeCellRemoved += OnFreeCellRemoved;

    }

    public void Off()
    {
        _gameModel.Board.FreeCellAdded -= OnFreeCellAdded;
        _gameModel.Board.FreeCellRemoved -= OnFreeCellRemoved;
    }

    private void OnFreeCellRemoved(FreeCell freeCell)
    {
        _gameView.Boxes[freeCell.X, freeCell.Y].color = Color.gray;
    }

    private void OnFreeCellAdded(FreeCell freeCell)
    {
        if(_gameModel.isWhiteMove)
        {
            _gameView.Boxes[freeCell.X, freeCell.Y].color = Color.green;
        }
    }
}
