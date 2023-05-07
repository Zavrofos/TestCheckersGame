using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ControllerEatingCheckers : IController
    {
        private GameModel _gameModel;
        private GameView _gameView;

        public ControllerEatingCheckers(GameModel gameModel, GameView gameView)
        {
            _gameModel = gameModel;
            _gameView = gameView;
        }

        public void On()
        {
            _gameModel.EatedChecker += OnEat;
        }

        private void OnEat(Checker checker)
        {
            _gameView.Destroy(checker);
            _gameModel.isEatenEnemyChecker = true;
        }

        public void Off()
        {
            _gameModel.EatedChecker -= OnEat;
        }
    }
}