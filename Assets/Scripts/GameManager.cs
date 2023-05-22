using Assets.Scripts;
using Assets.Scripts.Update;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public InputControls InputControls;
    public GameModel GameModel;
    public GameView GameView;
    private IController controllerFreeCell;
    private IController _inputController;
    public Dictionary<Checker, List<IController>> controllersCheckers = new Dictionary<Checker, List<IController>>();
    private IController controllerClick;
    private IController _pouseMenuController;
    private IController _pointsController;
    private IController _gameOverController;
    private IController _controllerMoveComputerPlayer;
    
    

    private void Awake()
    {
        GameModel = new GameModel();
        GameModel.StartGame();
        GameView = GetComponent<GameView>();
        InputControls = new InputControls();
        controllerFreeCell = new ControllerFreeCell(GameModel, GameView);
        _inputController = new InputCommandController(GameModel, InputControls);
        _pouseMenuController = new PouseMenuController(GameView, InputControls);
        _pointsController = new ControllerPoints(GameModel, GameView);
        _gameOverController = new GameOverController(GameModel, GameView);
        _controllerMoveComputerPlayer = new ControllerMoveComputerPlayer(GameModel, GameView);

        foreach (var checker in GameModel.Board.Checkers)
        {
            if (checker != null)
            {
                List<IController> controllers = new List<IController>();
                controllers.Add(new ControllerMoveChecker(GameModel, checker, GameView));
                controllers.Add(new ControllerEatingCheckers(checker, GameModel, GameView));
                controllersCheckers.Add(checker, controllers);
            }
        }

        controllerClick = new ControllerClickMouse(GameModel);

        GameView.Boxes = new SpriteRenderer[GameModel.Board.Width, GameModel.Board.Height];
        GameView.Checkers = new CheckerPrefab[GameModel.Board.Width, GameModel.Board.Height];
        GameView.DrowBoard(GameModel);

    }

    public void ResetGame()
    {
        GameModel.ResetGame();
        GameView.ResetGame();
    }

    private void ConstrollersOn()
    {
        foreach (var listControllers in controllersCheckers)
        {
            foreach (var controller in listControllers.Value)
            {
                controller.On();
            }
        }
        controllerFreeCell.On();
        controllerClick.On();
        _inputController.On();
        _pouseMenuController.On();
        _pointsController.On();
        _gameOverController.On();
        _controllerMoveComputerPlayer.On();
    }

    private void ControllersOff()
    {
        foreach (var listControllers in controllersCheckers)
        {
            foreach (var controller in listControllers.Value)
            {
                controller.Off();
            }
        }
        controllerFreeCell.Off();
        controllerClick.Off();
        _inputController.Off();
        _pouseMenuController.Off();
        _pointsController.Off();
        _gameOverController.Off();
        _controllerMoveComputerPlayer.Off();
    }

    private void OnEnable()
    {
        ConstrollersOn();
    }

    private void OnDisable()
    {
        ControllersOff();
    }

}
