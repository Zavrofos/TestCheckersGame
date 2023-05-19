using Assets.Scripts;
using Assets.Scripts.Update;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GameModel 
{
    public BoardModel Board;
    public CommandModel CommandModel;
    public IStateble currentState;
    public Checker SelectedChecker;
    public bool isEatenEnemyChecker;
    public bool isWhiteMove = true;

    public void StartGame()
    {
       Board = new BoardModel(8, 8);
       CommandModel = new CommandModel(Board.Width, Board.Height);
       currentState = new StateWaitingMove();
    }
    public void ChangeState(IStateble newState)
    {
        currentState = newState;
    }

    public void ResetGame()
    {

    }
}
