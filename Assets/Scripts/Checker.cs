using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker 
{
    public int X;
    public int Y;
    public readonly bool IsWhite;

    public Checker(int x, int y, bool isWhite)
    {
        X = x;
        Y = y;
        IsWhite = isWhite;
    }
}
