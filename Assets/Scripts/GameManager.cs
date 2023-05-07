using Assets.Scripts;
using Assets.Scripts.Update;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameModel GameModel;
    public GameView GameView;
    private IController controllerFreeCell;
    private IController controllerMoveChecker;
    private IController controllerEatingChecker;

    public IUpdateble currentUpdate;

    private void Awake()
    {
        GameModel = new GameModel();
        GameModel.StartGame();
        GameView = GetComponent<GameView>();
        controllerFreeCell = new ControllerFreeCell(GameModel, GameView);
        controllerMoveChecker = new ControllerMoveChecker(GameModel, GameView);
        controllerEatingChecker = new ControllerEatingCheckers(GameModel, GameView);
        GameView.Boxes = new SpriteRenderer[GameModel.Board.Width, GameModel.Board.Height];
        GameView.Checkers = new Transform[GameModel.Board.Width, GameModel.Board.Height];
        GameView.DrowBoard(GameModel);
        currentUpdate = new UpdateWhiteTurn(GameModel, this);
    }

    private void Update()
    {
        currentUpdate.Update();
    }

    public void ChangeUpdate()
    {
        if(currentUpdate is UpdateWhiteTurn)
        {
            currentUpdate = new UpdateBlackTurn(GameModel, this);
            GameView.Move.text = "BlackMove";
            GameModel.isEatenEnemyChecker = false;
        }
        else
        {
            currentUpdate = new UpdateWhiteTurn(GameModel, this);
            GameView.Move.text = "WhiteMove";
            GameModel.isEatenEnemyChecker = false;
        }
    }

    public (int,int) GetMousePosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,
            Input.mousePosition.y));
        int x = (int)Mathf.Round(mousePosition.x);
        int y = (int)Mathf.Round(mousePosition.y);
        return (x, y);
    }

    private void OnEnable()
    {
        controllerFreeCell.On();
        controllerMoveChecker.On();
        controllerEatingChecker.On();
    }

    private void OnDisable()
    {
        controllerFreeCell.Off();
        controllerMoveChecker.Off();
        controllerEatingChecker.Off();
    }
}
