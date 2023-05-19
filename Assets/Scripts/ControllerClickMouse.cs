using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ControllerClickMouse : IController
    {
        private GameModel _gameModel;

        public ControllerClickMouse(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public void On()
        {
            _gameModel.CommandModel.Clicked += OnClick;
        }
        public void Off()
        {
            _gameModel.CommandModel.Clicked -= OnClick;
        }
        private void OnClick(int mousePositionX, int mousePositionY)
        {
            _gameModel.currentState.Move(mousePositionX, mousePositionY, _gameModel);
        }
    }
}