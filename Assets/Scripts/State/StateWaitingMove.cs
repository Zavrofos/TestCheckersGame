using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Update
{
    public class StateWaitingMove : IStateble
    {
        public void Move(int mousePositionX, int mousePositionY, GameModel gameModel)
        {
            if(gameModel.Board.AvailableCheckers.Count == 0)
            {
                gameModel.Board.AvailableCheckersToMove(gameModel.isWhiteMove);
            }

            Checker availableChecker = gameModel.Board.GetAvailableCheckerToCheckToMove(mousePositionX, mousePositionY);
            if(availableChecker != null)
            {
                gameModel.SelectedChecker = null;
                gameModel.SelectedChecker = availableChecker;
                gameModel.Board.ClearFreeCells(gameModel.Board.FreeCells);
                gameModel.Board.CheckNearEnemyAndGetFreeCells(gameModel.SelectedChecker, gameModel.isWhiteMove);
                if (gameModel.Board.FreeCells.Count == 0)
                {
                    gameModel.Board.GetFreeCellToMove(gameModel.SelectedChecker);
                }
                gameModel.ChangeState(new StateSelectChecker());
            }
        }
    }
}