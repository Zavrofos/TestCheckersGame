using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private GameObject BoxPrefab;
    [SerializeField] private Transform _blackChecker;
    [SerializeField] private Transform _whiteChecker;
    [SerializeField] private Button ButtonWhite;
    [SerializeField] private Button ButtonBlack;
    public SpriteRenderer[,] Boxes;
    public Transform[,] Checkers;
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

                if (gameModel.Board.Checkers[x, y] is WhiteChecker)
                {
                    Transform checker = Instantiate(_whiteChecker);
                    checker.position = new Vector2(x,y);
                    Checkers[x, y] = checker;
                }

                if (gameModel.Board.Checkers[x, y] is BlackChecker)
                {
                    Transform checker = Instantiate(_blackChecker);
                    checker.position = new Vector2(x, y);
                    Checkers[x, y] = checker;
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
