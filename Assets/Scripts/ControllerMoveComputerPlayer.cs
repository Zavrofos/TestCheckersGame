using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ControllerMoveComputerPlayer : IController
    {
        private GameModel _gameModel;
        private GameView _gameView;

        public ControllerMoveComputerPlayer(GameModel gameModel, GameView gameView)
        {
            _gameModel = gameModel;
            _gameView = gameView;
        }

        public void On()
        {
            _gameModel.ComputerPlayer.MoverComputerPlayer += StartCoroutineMove;
        }
        public void Off()
        {
            _gameModel.ComputerPlayer.MoverComputerPlayer -= StartCoroutineMove;
        }

        private IEnumerator MoveCoroutine()
        {
            while (!_gameModel.isWhiteMove && !_gameModel.isGameOver)
            {
                yield return new WaitForSeconds(0.5f);
                Checker checker = _gameModel.Board.AvailableCheckers[UnityEngine.Random.Range(0, _gameModel.Board.AvailableCheckers.Count)];
                _gameModel.currentState.Move(checker.X, checker.Y, _gameModel);
                yield return new WaitForSeconds(0.5f);
                FreeCell freeCell = _gameModel.Board.FreeCells[UnityEngine.Random.Range(0, _gameModel.Board.FreeCells.Count)];
                _gameModel.currentState.Move(freeCell.X, freeCell.Y, _gameModel);
                yield return new WaitForSeconds(0.5f);
            }
        }
        private void StartCoroutineMove()
        {
            _gameView.StartCoroutineView(MoveCoroutine());
        }
    }
}