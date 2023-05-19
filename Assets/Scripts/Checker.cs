using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checker 
{
    public event Action<int, int, int, int> CheckerMoved; 
    public event Action<int, int, int, int> EatedChecker;

    public int X;
    public int Y;
    public int InitialX;
    public int InitialY;
    public readonly bool IsWhite;
    public bool IsKing;

    public Checker(int x, int y, bool isWhite)
    {
        X = x;
        Y = y;
        InitialX = x;
        InitialY = y;
        IsWhite = isWhite;
    }

    public void Move(int x, int y) 
    {
        int oldX = X;
        int oldY = Y;

        X = x;
        Y = y;

        CheckerMoved?.Invoke(oldX, oldY, x, y);
        EatedChecker?.Invoke(oldX, oldY, x, y);
    }
}
