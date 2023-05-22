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
    public PointsModel PointsModel;
    public GameOverModel GameOverModel;
    public ComputerPlayerBlack ComputerPlayer;
    public IStateble currentState;
    public Checker SelectedChecker;
    public bool isEatenEnemyChecker;
    public bool isWhiteMove = true;
    public bool isGameOver = false;

    public int WhitePlayerPoints = 0;
    public int BlackPlayerPoints = 0;

    public void StartGame()
    {
        Board = new BoardModel(8, 8);
        CommandModel = new CommandModel(Board.Width, Board.Height);
        PointsModel = new PointsModel();
        GameOverModel = new GameOverModel();
        currentState = new StateWaitingMove();
        ComputerPlayer = new ComputerPlayerBlack();
    }
    public void ChangeState(IStateble newState)
    {
        currentState = newState;
    }

    public void ResetGame()
    {
        Board.ResetGame();
        isEatenEnemyChecker = false;
        isWhiteMove = true;
        isGameOver = false;
        SelectedChecker = null;
        currentState = new StateWaitingMove();
        PointsModel.WhiteplayerPoints = 0;
        PointsModel.BlackPlayerPoints = 0;
    }
}
