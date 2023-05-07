using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board 
{
    public int Height;
    public int Width;
    public Box[,] BoardBox;
    public Checker[,] Checkers;

    public Board(int height, int width)
    {
        BoardBox = new Box[height, width];
        Checkers = new Checker[height, width];
        Height = height;
        Width = width;
        DrowBoard();
        PlacementOfCheckers();
    }

    public void DrowBoard()
    {

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                int index = x % 2 + y % 2;

                if (index % 2 == 0)
                {
                    BoardBox[x, y] = new Box() { isWhite = false };
                }
                else
                {
                    BoardBox[x, y] = new Box() { isWhite = true };
                }
            }
        }
    }
    public void PlacementOfCheckers()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                int index = x % 2 + y % 2;
                if (index % 2 == 0)
                {
                    Checkers[x, y] = new WhiteChecker(x, y);

                }
                else
                {
                    Checkers[x, Height - 1 - y] = new BlackChecker(x, Height - 1 - y);
                }
            }
        }
    }
}

