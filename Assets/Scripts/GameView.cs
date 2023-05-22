using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private GameObject BoxPrefab;
    [SerializeField] private CheckerPrefab _checker;
    public PouseMenu PouseMenu;
    public TMP_Text WhitePlayerPoints;
    public TMP_Text BlackPlayerPoints;
    public Canvas GameOverMenu;
    public Canvas PointsCanvas;
    public TMP_Text PlayerWinText;
    
    public SpriteRenderer[,] Boxes;
    public CheckerPrefab[,] Checkers;
    public List<CheckerPrefab> EatenCheckers = new List<CheckerPrefab>();
    private Vector2 _positionEatedWhiteChecker = new Vector2(9, 7);
    private Vector2 _positionEatedBlackChecker = new Vector2(9, 0);

    public void DrowBoard(GameModel gameModel)
    {
        for (int x = 0; x < gameModel.Board.Width; x++)
        {
            for (int y = 0; y < gameModel.Board.Height; y++)
            {
                if (gameModel.Board.BoardBox[x, y].isWhite == true)
                {
                    GameObject box = Instantiate(BoxPrefab);
                    box.GetComponent<Transform>().position = new Vector2(x, y);
                    SpriteRenderer spriteRendererComponent = box.GetComponent<SpriteRenderer>();
                    spriteRendererComponent.color = Color.white;
                    Boxes[x, y] = spriteRendererComponent;
                }
                else
                {
                    GameObject box = Instantiate(BoxPrefab);
                    box.GetComponent<Transform>().position = new Vector2(x, y);
                    SpriteRenderer spriteRendererComponent = box.GetComponent<SpriteRenderer>();
                    spriteRendererComponent.color = Color.gray;
                    Boxes[x, y] = spriteRendererComponent;
                }

                if (gameModel.Board.Checkers[x, y] != null && gameModel.Board.Checkers[x, y].IsWhite)
                {
                    CheckerPrefab whiteCheker = Instantiate(_checker);
                    whiteCheker.SpriteRenderer.sprite = _checker.WhiteCheckerSprite;
                    whiteCheker.InitialSprite = whiteCheker.WhiteCheckerSprite;
                    whiteCheker.Transform.position = new Vector2(x, y);
                    whiteCheker.InitialPosition = new Vector2(x, y);
                    Checkers[x, y] = whiteCheker;
                }

                if (gameModel.Board.Checkers[x, y] != null && !gameModel.Board.Checkers[x, y].IsWhite)
                {
                    CheckerPrefab blackChecker = Instantiate(_checker);
                    blackChecker.SpriteRenderer.sprite = _checker.BlackCheckerSprite;
                    blackChecker.InitialSprite = blackChecker.BlackCheckerSprite;
                    blackChecker.Transform.position = new Vector2(x, y);
                    blackChecker.InitialPosition = new Vector2(x, y);
                    Checkers[x, y] = blackChecker;
                }
            }
        }
    }

    public void Destroy(Checker checker)
    {
        if(checker.IsWhite)
        {
            CheckerPrefab EatenChecker = Checkers[checker.X, checker.Y];
            EatenChecker.Transform.position = _positionEatedWhiteChecker;
            EatenChecker.SpriteRenderer.sprite = EatenChecker.WhiteCheckerSprite;
            EatenCheckers.Add(EatenChecker);
        }
        else
        {
            CheckerPrefab EatenChecker = Checkers[checker.X, checker.Y];
            EatenChecker.Transform.position = _positionEatedBlackChecker;
            EatenChecker.SpriteRenderer.sprite = EatenChecker.BlackCheckerSprite;
            EatenCheckers.Add(EatenChecker);
        }
        Checkers[checker.X, checker.Y] = null;
    }

    public void ResetGame()
    {
        for(int x = 0; x < Checkers.GetLength(0); x++)
        {
            for(int y = 0; y < Checkers.GetLength(1); y++)
            {
                CheckerPrefab checker = Checkers[x, y];
                if(checker != null)
                {
                    checker.Transform.position = Vector2.zero;
                    checker.SpriteRenderer.sprite = checker.InitialSprite;
                    EatenCheckers.Add(checker);
                    Checkers[x, y] = null;
                }
            }
        }
        foreach(var checker in EatenCheckers)
        {
            checker.Transform.position = checker.InitialPosition;

            int x = (int)checker.InitialPosition.x;
            int y = (int)checker.InitialPosition.y;
            Checkers[x, y] = checker;
        }
        WhitePlayerPoints.text = "x 0";
        BlackPlayerPoints.text = "x 0";
        PointsCanvas.gameObject.SetActive(true);
    }

    public void StartCoroutineView(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
