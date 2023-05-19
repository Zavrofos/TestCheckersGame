using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts
{
    public class InputCommandController : IController
    {
        private readonly InputControls _controls;
        private CommandModel _commandModel;

        public InputCommandController(CommandModel commandModel, InputControls inputControls)
        {
            _controls = inputControls;
            _commandModel = commandModel;
        }

        public void On()
        {
            _controls.MouseControls.Click.started += OnClick;
            _controls.MouseControls.Enable();
        }

        public void Off()
        {
            _controls.MouseControls.Click.started -= OnClick;
            _controls.MouseControls.Disable();
        }

        private void OnClick(CallbackContext context)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            _commandModel.GetMousePosition(position.x, position.y);
        }
    }
}