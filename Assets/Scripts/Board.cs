using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardModel
{
    public event Action<FreeCell> FreeCellAdded;
    public event Action<FreeCell> FreeCellRemoved;

    public List<FreeCell> FreeCells = new List<FreeCell>();
    public List<Checker> AvailableCheckers = new List<Checker>();
    public List<Checker> EatedCheckers = new List<Checker>();
    
    public int Height;
    public int Width;
    public Box[,] BoardBox;
    public Checker[,] Checkers;

    public BoardModel(int height, int width)
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
                    Checkers[x, y] = new Checker(x, y, true);
                }
                else
                {
                    Checkers[x, Height - 1 - y] = new Checker(x, Height - 1 - y, false);
                }
            }
        }
    }

    public void ClearFreeCells(List<FreeCell> freeCells)
    {
        foreach (var cell in freeCells)
        {
            FreeCellRemoved?.Invoke(cell);
        }
        freeCells.Clear();
    }




    public void CheckNearEnemyAndGetFreeCells(Checker selectedChecker, bool isWhiteMove)
    {
        if(selectedChecker.IsKing)
        {
            List<Checker> enemyCheckers = CheckCanEateEnemyCheckerForKing(selectedChecker, isWhiteMove);
            FreeCells = GetCellToMoveCheckerForKing(enemyCheckers, selectedChecker);
        }
        else
        {
            List<Checker> enemyCheckers = CheckCanEateEnemyChecker(selectedChecker, isWhiteMove);
            FreeCells = GetCellToMoveChecker(enemyCheckers, selectedChecker);
        }
    }
    private List<Checker> CheckCanEateEnemyChecker(Checker selectedChecker, bool isWhiteMove)
    {
        List<Checker> enemyCheckers = new List<Checker>();
        if (selectedChecker.IsKing)
        {
            for (int i = 1; i < Width; i++)
            {
                int xRight = selectedChecker.X + i;
                int xLeft = selectedChecker.X - i;
                int yUp = selectedChecker.Y + i;
                int yDown = selectedChecker.Y - i;

                if (xRight < Width && yUp < Height)
                {
                    Checker checker = Checkers[xRight, yUp];
                    if (checker != null && checker.IsWhite != isWhiteMove)
                    {
                        enemyCheckers.Add(checker);
                    }
                }
                if (xLeft >= 0 && yUp < Height)
                {
                    Checker checker = Checkers[xLeft, yUp];
                    if (checker != null && checker.IsWhite != isWhiteMove)
                    {
                        enemyCheckers.Add(checker);
                    }
                }
                if (xRight < Height && yDown >= 0)
                {
                    Checker checker = Checkers[xRight, yDown];
                    if (checker != null && checker.IsWhite != isWhiteMove)
                    {
                        enemyCheckers.Add(checker);
                    }
                }
                if (xLeft >= 0 && yDown >= 0)
                {
                    Checker checker = Checkers[xRight, yDown];
                    if (checker != null && checker.IsWhite != isWhiteMove)
                    {
                        enemyCheckers.Add(checker);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i += 2)
            {
                for (int j = 0; j < 3; j += 2)
                {
                    int x = selectedChecker.X - 1 + i;
                    int y = selectedChecker.Y + 1 - j;

                    if (x >= 0 && x < Width && y >= 0 && y < Height)
                    {
                        if (selectedChecker.IsWhite == isWhiteMove)
                        {
                            Checker enemyChecker = Checkers[x, y];
                            if (enemyChecker != null && enemyChecker.IsWhite != isWhiteMove)
                            {
                                enemyCheckers.Add(enemyChecker);
                            }
                        }
                    }
                }
            }
        }
        return enemyCheckers;
    }
    private List<FreeCell> GetCellToMoveChecker(List<Checker> enemyCheckers, Checker selectedChecker)
    {
        List<FreeCell> freeCells = new List<FreeCell>();

        foreach (var checker in enemyCheckers)
        {
            if (checker.X < selectedChecker.X && checker.Y > selectedChecker.Y)
            {
                int x = checker.X - 1;
                int y = checker.Y + 1;
                if (x >= 0 && y < Height && Checkers[x, y] == null)
                {
                    FreeCell freeCell = new FreeCell(x, y);
                    freeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);

                }
            }
            if (checker.X < selectedChecker.X && checker.Y < selectedChecker.Y)
            {
                int x = checker.X - 1;
                int y = checker.Y - 1;
                if (x >= 0 && y >= 0 && Checkers[x, y] == null)
                {
                    FreeCell freeCell = new FreeCell(x, y);
                    freeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);
                }
            }
            if (checker.X > selectedChecker.X && checker.Y > selectedChecker.Y)
            {
                int x = checker.X + 1;
                int y = checker.Y + 1;
                if (x < Width && y < Height && Checkers[x, y] == null)
                {
                    FreeCell freeCell = new FreeCell(x, y);
                    freeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);
                }
            }
            if (checker.X > selectedChecker.X && checker.Y < selectedChecker.Y)
            {
                int x = checker.X + 1;
                int y = checker.Y - 1;
                if (x < Width && y >= 0 && Checkers[x, y] == null)
                {
                    FreeCell freeCell = new FreeCell(x, y);
                    freeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);
                }
            }
        }
        return freeCells;
    }
    private List<Checker> CheckCanEateEnemyCheckerForKing(Checker selectedChecker, bool isWhiteMove)
    {
        List<Checker> enemyCheckers = new List<Checker>();
        bool isLeftUp = false;
        bool isRightUp = false;
        bool isLeftDown = false;
        bool isRightDown = false;

        for (int i = 1; i < Width; i++)
        {
            int xRight = selectedChecker.X + i;
            int xLeft = selectedChecker.X - i;
            int yUp = selectedChecker.Y + i;
            int yDown = selectedChecker.Y - i;

            if (xRight < Width && yUp < Height && !isRightUp)
            {
                Checker checker = Checkers[xRight, yUp];
                if (checker != null && checker.IsWhite != isWhiteMove)
                {
                    int x = xRight + 1;
                    int y = yUp + 1;
                    if (x < Width && y < Height && Checkers[xRight + 1, yUp + 1] == null)
                    {
                        enemyCheckers.Add(checker);
                    }
                    isRightUp = true;
                }
            }
            if (xLeft >= 0 && yUp < Height && !isLeftUp)
            {
                Checker checker = Checkers[xLeft, yUp];
                if (checker != null && checker.IsWhite != isWhiteMove)
                {
                    int x = xLeft - 1;
                    int y = yUp + 1;
                    if (x >=0 && y < Height && Checkers[xLeft - 1, yUp + 1] == null)
                    {
                        enemyCheckers.Add(checker);
                    }
                    isLeftUp = true;
                }
            }
            if (xRight < Height && yDown >= 0 && !isRightDown)
            {
                Checker checker = Checkers[xRight, yDown];
                if (checker != null && checker.IsWhite != isWhiteMove)
                {
                    int x = xRight + 1;
                    int y = yDown - 1;
                    if (x < Width && y >= 0 && Checkers[xRight + 1, yDown - 1] == null)
                    {
                        enemyCheckers.Add(checker);
                    }
                    isRightDown = true;
                }
            }
            if (xLeft >= 0 && yDown >= 0 && !isLeftDown)
            {
                Checker checker = Checkers[xLeft, yDown];
                if (checker != null && checker.IsWhite != isWhiteMove)
                {
                    int x = xLeft - 1;
                    int y = yDown - 1;
                    if (x >= 0 && y >=0 && Checkers[xLeft - 1, yDown - 1] == null)
                    {
                        enemyCheckers.Add(checker);
                    }
                    isLeftDown = true;
                }
            }
        }
        return enemyCheckers;
    }
    private List<FreeCell> GetCellToMoveCheckerForKing(List<Checker> enemyCheckers, Checker selectedChecker)
    {
        List<FreeCell> freeCells = new List<FreeCell>();
        bool isLeftUp = false;
        bool isRightUp = false;
        bool isLeftDown = false;
        bool isRightDown = false;
        foreach (var enemyChecker in enemyCheckers)
        {
            if(enemyChecker.X < selectedChecker.X && enemyChecker.Y > selectedChecker.Y && !isLeftUp)
            {
                for(int i = 1; i < Height; i++)
                {
                    int x = enemyChecker.X - i;
                    int y = enemyChecker.Y + i;
                    if (x >= 0 && y < Height && Checkers[x, y] == null)
                    {
                        FreeCell freeCell = new FreeCell(x, y);
                        freeCells.Add(freeCell);
                        FreeCellAdded?.Invoke(freeCell);
                    }
                    else
                    {
                        isLeftUp = true;
                        break;
                    }
                }
                
            }
            if (enemyChecker.X > selectedChecker.X && enemyChecker.Y > selectedChecker.Y && !isRightUp)
            {
                for (int i = 1; i < Height; i++)
                {
                    int x = enemyChecker.X + i;
                    int y = enemyChecker.Y + i;
                    if (x < Width && y < Height && Checkers[x, y] == null)
                    {
                        FreeCell freeCell = new FreeCell(x, y);
                        freeCells.Add(freeCell);
                        FreeCellAdded?.Invoke(freeCell);
                    }
                    else
                    {
                        isRightUp = true;
                        break;
                    }
                }
            }
            if (enemyChecker.X < selectedChecker.X && enemyChecker.Y < selectedChecker.Y && !isLeftDown)
            {
                for (int i = 1; i < Height; i++)
                {
                    int x = enemyChecker.X - i;
                    int y = enemyChecker.Y - i;
                    if (x >= 0 && y >= 0 && Checkers[x, y] == null)
                    {
                        FreeCell freeCell = new FreeCell(x, y);
                        freeCells.Add(freeCell);
                        FreeCellAdded?.Invoke(freeCell);
                    }
                    else
                    {
                        isLeftDown = true;
                        break;
                    }
                }
            }
            if (enemyChecker.X > selectedChecker.X && enemyChecker.Y < selectedChecker.Y && !isRightDown)
            {
                for (int i = 1; i < Height; i++)
                {
                    int x = enemyChecker.X + i;
                    int y = enemyChecker.Y - i;
                    if (x < Width && y >= 0 && Checkers[x, y] == null)
                    {
                        FreeCell freeCell = new FreeCell(x, y);
                        freeCells.Add(freeCell);
                        FreeCellAdded?.Invoke(freeCell);
                    }
                    else
                    {
                        isRightDown = true;
                        break;
                    }
                }
            }
        }
        return freeCells;
    }
    


    public void GetFreeCellToMove(Checker selectedChecker)
    {
        if(selectedChecker.IsKing)
        {
            bool isLeftUp = false;
            bool isRightUp = false;
            bool isLeftDown = false;
            bool isRightDown = false;

            for (int i = 1; i < Height; i++)
            {
                int xRight = selectedChecker.X + i;
                int xLeft = selectedChecker.X - i;
                int yUp = selectedChecker.Y + i;
                int yDown = selectedChecker.Y - i;

                if (xRight < Width && yUp < Height && Checkers[xRight, yUp] == null && !isRightUp)
                {
                    FreeCell freeCell = new FreeCell(xRight, yUp);
                    FreeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);
                }
                else 
                {
                    isRightUp = true;
                }

                if (xRight < Width && yDown >=0 && Checkers[xRight, yDown] == null && !isRightDown)
                {
                    FreeCell freeCell = new FreeCell(xRight, yDown);
                    FreeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);
                }
                else
                {
                    isRightDown = true;
                }

                if (xLeft >=0 && yUp < Height && Checkers[xLeft, yUp] == null && !isLeftUp)
                {
                    FreeCell freeCell = new FreeCell(xLeft, yUp);
                    FreeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);
                }
                else
                {
                    isLeftUp = true;
                }

                if (xLeft >= 0 && yDown >=0 && Checkers[xLeft, yDown] == null && !isLeftDown)
                {
                    FreeCell freeCell = new FreeCell(xLeft, yDown);
                    FreeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);
                }
                else
                {
                    isLeftDown = true;
                }
            }
        }
        else
        {
            if (selectedChecker.IsWhite)
            {
                int rightX = selectedChecker.X + 1;
                int y = selectedChecker.Y + 1;
                int leftX = selectedChecker.X - 1;
                if (leftX >= 0 && y < Height && Checkers[leftX, y] == null)
                {
                    var freeCell = new FreeCell(leftX, y);
                    FreeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);
                }
                if (rightX < Width && y < Height && Checkers[rightX, y] == null)
                {
                    var freeCell = new FreeCell(rightX, y);
                    FreeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);
                }
            }
            else if (!selectedChecker.IsWhite)
            {
                int rightX = selectedChecker.X + 1;
                int y = selectedChecker.Y - 1;
                int leftX = selectedChecker.X - 1;
                if (leftX >= 0 && y >= 0 && Checkers[leftX, y] == null)
                {
                    var freeCell = new FreeCell(leftX, y);
                    FreeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);
                }
                if (rightX < Width && y >= 0 && Checkers[rightX, y] == null)
                {
                    var freeCell = new FreeCell(rightX, y);
                    FreeCells.Add(freeCell);
                    FreeCellAdded?.Invoke(freeCell);
                }
            }
        }
    }


    public void AvailableCheckersToMove(bool isWhiteMove)
    {
        foreach (var checker in Checkers)
        {
            if (checker != null && checker.IsWhite == isWhiteMove)
            {
                CheckNearEnemyAndGetFreeCells(checker, isWhiteMove);
                if (FreeCells.Count != 0)
                {
                    AvailableCheckers.Add(checker);
                    ClearFreeCells(FreeCells);
                }
            }
        }

        if (AvailableCheckers.Count == 0)
        {
            foreach (var checker in Checkers)
            {
                if (checker != null && checker.IsWhite == isWhiteMove)
                {
                    GetFreeCellToMove(checker);
                    if (FreeCells.Count != 0)
                    {
                        AvailableCheckers.Add(checker);
                        ClearFreeCells(FreeCells);
                    }
                }
            }
        }
    }



    public FreeCell GetFreeCellToCheckToMove(int mousePositionX, int mousePositionY)
    {
        foreach (var freeCell in FreeCells)
        {
            if (freeCell.X == mousePositionX && freeCell.Y == mousePositionY)
            {
                return freeCell;
            }
        }
        return null;
    }
    public Checker GetAvailableCheckerToCheckToMove(int mousePositionX, int mousePositionY)
    {
        foreach (var availableChecker in AvailableCheckers)
        {
            if (availableChecker.X == mousePositionX && availableChecker.Y == mousePositionY)
            {
                return availableChecker;
            }
        }
        return null;
    }



    public void ResetGame()
    {
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                Checker chercker = Checkers[x, y];
                if (chercker != null)
                {
                    EatedCheckers.Add(chercker);
                    Checkers[x, y] = null;
                }
            }
        }

        foreach(var checker in EatedCheckers)
        {
            checker.Reset();
            Checkers[checker.X, checker.Y] = checker;
        }

        FreeCells.Clear();
        AvailableCheckers.Clear();
    }
}

