using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private GameObject BoxPrefab;
    [SerializeField] private CheckerPrefab _checker;
    public PouseMenu PouseMenu;
    
    public SpriteRenderer[,] Boxes;
    public CheckerPrefab[,] Checkers;
    public Text Move;

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
                    whiteCheker.Transform.position = new Vector2(x, y);
                    Checkers[x, y] = whiteCheker;
                    //Transform checker = Instantiate(_whiteChecker);
                    //checker.position = new Vector2(x,y);
                    //Checkers[x, y] = checker;
                }

                if (gameModel.Board.Checkers[x, y] != null && !gameModel.Board.Checkers[x, y].IsWhite)
                {
                    CheckerPrefab blackChecker = Instantiate(_checker);
                    blackChecker.SpriteRenderer.sprite = _checker.BlackCheckerSprite;
                    blackChecker.Transform.position = new Vector2(x, y);
                    Checkers[x, y] = blackChecker;

                    //Transform checker = Instantiate(_blackChecker);
                    //checker.position = new Vector2(x, y);
                    //Checkers[x, y] = checker;
                }
            }
        }
    }

    public void Destroy(Checker checker)
    {
        Destroy(Checkers[checker.X, checker.Y].gameObject);
        Checkers[checker.X, checker.Y] = null;
    }
}
