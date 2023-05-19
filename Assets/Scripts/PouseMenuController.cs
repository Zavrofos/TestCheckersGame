using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts
{
    public class PouseMenuController : IController
    {
        private GameView _gameView;
        private InputControls _inputControls;

        public PouseMenuController(GameView gameView, InputControls inputControls)
        {
            _gameView = gameView;
            _inputControls = inputControls;
        }

        public void On()
        {
            _inputControls.MouseControls.Pouse.Enable();
            _inputControls.MouseControls.Pouse.started += PressKeyExit;

        }
        public void Off()
        {
            _inputControls.MouseControls.Pouse.Disable();
            _inputControls.MouseControls.Pouse.started -= PressKeyExit;
        }

        private void PressKeyExit(CallbackContext context)
        {
            _gameView.PouseMenu.ActivePouse();
        }
    }
}