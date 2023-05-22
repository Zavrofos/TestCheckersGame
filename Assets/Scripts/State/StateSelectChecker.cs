using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Update
{
    public class StateSelectChecker : IStateble
    {
        public void Move(int mousePositionX, int mousePositionY, GameModel gameModel)
        {
            FreeCell freeCell = gameModel.Board.GetFreeCellToCheckToMove(mousePositionX, mousePositionY);
            if (freeCell != null)
            {
                gameModel.SelectedChecker.Move(mousePositionX, mousePositionY);
                if (gameModel.isEatenEnemyChecker)
                {
                    gameModel.SelectedChecker = null;
                    gameModel.SelectedChecker = gameModel.Board.Checkers[mousePositionX, mousePositionY];
                    gameModel.Board.ClearFreeCells(gameModel.Board.FreeCells);
                    gameModel.Board.CheckNearEnemyAndGetFreeCells(gameModel.SelectedChecker, gameModel.isWhiteMove);
                    gameModel.isEatenEnemyChecker = false;
                    if (gameModel.Board.FreeCells.Count != 0)
                    {
                        return;
                    }
                }
                gameModel.SelectedChecker = null;
                gameModel.Board.AvailableCheckers.Clear();
                gameModel.Board.ClearFreeCells(gameModel.Board.FreeCells);
                gameModel.isWhiteMove = !gameModel.isWhiteMove;
                gameModel.ChangeState(new StateWaitingMove());
                gameModel.Board.AvailableCheckersToMove(gameModel.isWhiteMove);
                gameModel.GameOverModel.CheckGameOver(gameModel.Board.AvailableCheckers, gameModel.isWhiteMove);
                //gameModel.currentState.Move(mousePositionX, mousePositionY, gameModel);
                if (!gameModel.isWhiteMove && !gameModel.isGameOver)
                {
                    //gameModel.Board.AvailableCheckersToMove(gameModel.isWhiteMove);
                    //gameModel.GameOverModel.CheckGameOver(gameModel.Board.AvailableCheckers, gameModel.isWhiteMove);
                    gameModel.ComputerPlayer.Move();
                }
                return;
            }

            Checker availableChecker = gameModel.Board.GetAvailableCheckerToCheckToMove(mousePositionX, mousePositionY);
            if(availableChecker != null)
            {
                gameModel.ChangeState(new StateWaitingMove());
                gameModel.currentState.Move(mousePositionX, mousePositionY, gameModel);
            }
        }
    }
}