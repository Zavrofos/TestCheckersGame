using Assets.Scripts;
using Assets.Scripts.Update;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GameModel 
{
    public event Action<FreeCell> FreeCellAdded;
    public event Action<FreeCell> FreeCellRemoved;
    public event Action<Checker, int, int> CheckerMoved;
    public event Action<Checker> EatedChecker;
    public Board Board;

    public Checker SelectedChecker;
    public bool isEatenEnemyChecker;
    public bool isMoved;
    
    public void StartGame()
    {
       Board = new Board(8, 8);
    }

    public void ClearFreeCells(List<FreeCell> freeCells)
    {
        foreach(var cell in freeCells)
        {
            FreeCellRemoved?.Invoke(cell);
        }
        freeCells.Clear();
    }
    public void MoveChecker(Checker checker, int x, int y)
    {
        if(checker is WhiteChecker)
        {
            Board.Checkers[checker.X, checker.Y] = null;
            Board.Checkers[x, y] = new WhiteChecker(x, y);
            CheckerMoved?.Invoke(checker, x, y);
        }
        else if(checker is BlackChecker)
        {
            Board.Checkers[checker.X, checker.Y] = null;
            Board.Checkers[x, y] = new BlackChecker(x, y);
            CheckerMoved?.Invoke(checker, x, y);
        }
    }
    public void EatChecker(Checker selectedChecker, int newPositionX, int newPositionY)
    {
        int x = 0;
        int y = 0;
        if (selectedChecker.X > newPositionX && selectedChecker.Y < newPositionY)
        {
            x = selectedChecker.X - 1;
            y = selectedChecker.Y + 1;
        }
        else if (selectedChecker.X < newPositionX && selectedChecker.Y < newPositionY)
        {
            x = selectedChecker.X + 1;
            y = selectedChecker.Y + 1;
        }
        else if (selectedChecker.X > newPositionX && selectedChecker.Y > newPositionY)
        {
            x = selectedChecker.X - 1;
            y = selectedChecker.Y - 1;
        }
        else if (selectedChecker.X < newPositionX && selectedChecker.Y > newPositionY)
        {
            x = selectedChecker.X + 1;
            y = selectedChecker.Y - 1;
        }

        if (selectedChecker is WhiteChecker)
        {
            if (Board.Checkers[x, y] is BlackChecker)
            {
                Checker checker = Board.Checkers[x, y];
                EatedChecker?.Invoke(checker);
                Board.Checkers[x, y] = null;
                isEatenEnemyChecker = true;
            }
        }
        else if (selectedChecker is BlackChecker)
        {
            if (Board.Checkers[x, y] is WhiteChecker)
            {
                Checker checker = Board.Checkers[x, y];
                EatedChecker?.Invoke(checker);
                Board.Checkers[x, y] = null;
                isEatenEnemyChecker = true;
            }
        }
    }





    public List<FreeCell> CheckNearEnemyAndGetFreeCells(Checker selectedChecker)
    {
        List<Checker> enemyCheckers = CheckCanEateEnemyChecker(selectedChecker);
        List<DirectionToEatChecker> directions = GetDirectionMoveToEatChecker(enemyCheckers, selectedChecker);
        return GetCellToMoveChecker(directions, selectedChecker);
    }
    public List<Checker> CheckCanEateEnemyChecker(Checker selectedChecker)
    {
        List<Checker> enemyCheckers = new List<Checker>();
        for (int i = 0; i < 3; i += 2)
        {
            for (int j = 0; j < 3; j += 2)
            {
                int x = selectedChecker.X - 1 + i;
                int y = selectedChecker.Y + 1 - j;

                if (x >= 0 && x < Board.Width && y >= 0 && y < Board.Height)
                {
                    if (selectedChecker is WhiteChecker)
                    {
                        Checker enemyChecker = Board.Checkers[x, y];
                        if (enemyChecker is BlackChecker)
                        {
                            enemyCheckers.Add(enemyChecker);
                        }
                    }
                    else if (selectedChecker is BlackChecker)
                    {
                        Checker enemyChecker = Board.Checkers[x, y];
                        if (enemyChecker is WhiteChecker)
                        {
                            enemyCheckers.Add(enemyChecker);
                        }
                    }
                }
            }
        }
        return enemyCheckers;
    }
    public List<DirectionToEatChecker> GetDirectionMoveToEatChecker(List<Checker> enemyCheckers, Checker selectedChecker)
    {
        List<DirectionToEatChecker> directionsToEat = new List<DirectionToEatChecker>();

        foreach (var checker in enemyCheckers)
        {
            if (checker.X < selectedChecker.X && checker.Y > selectedChecker.Y)
            {
                int x = checker.X - 1;
                int y = checker.Y + 1;
                if (x >= 0 && y < Board.Height && Board.Checkers[x, y] == null)
                {
                    directionsToEat.Add(DirectionToEatChecker.LeftUp);
                }
            }
            else if (checker.X < selectedChecker.X && checker.Y < selectedChecker.Y)
            {
                int x = checker.X - 1;
                int y = checker.Y - 1;
                if (x >= 0 && y >= 0 && Board.Checkers[x, y] == null)
                {
                    directionsToEat.Add(DirectionToEatChecker.LeftDown);
                }
            }
            else if (checker.X > selectedChecker.X && checker.Y > selectedChecker.Y)
            {
                int x = checker.X + 1;
                int y = checker.Y + 1;
                if (x < Board.Width && y < Board.Height && Board.Checkers[x, y] == null)
                {
                    directionsToEat.Add(DirectionToEatChecker.RightUp);
                }
            }
            else if (checker.X > selectedChecker.X && checker.Y < selectedChecker.Y)
            {
                int x = checker.X + 1;
                int y = checker.Y - 1;
                if (x < Board.Width && y >= 0 && Board.Checkers[x, y] == null)
                {
                    directionsToEat.Add(DirectionToEatChecker.RightDown);
                }
            }
        }
        return directionsToEat;
    }
    public List<FreeCell> GetCellToMoveChecker(List<DirectionToEatChecker> directions, Checker selectedChecker) ///
    {
        List<FreeCell> freeCells = new List<FreeCell>();
        foreach (var direction in directions)
        {
            if (direction == DirectionToEatChecker.LeftUp)
            {
                FreeCell freeCell = new FreeCell(selectedChecker.X - 2, selectedChecker.Y + 2);
                freeCells.Add(freeCell);
                FreeCellAdded.Invoke(freeCell);
            }
            else if (direction == DirectionToEatChecker.LeftDown)
            {
                FreeCell freeCell = new FreeCell(selectedChecker.X - 2, selectedChecker.Y - 2);
                freeCells.Add(new FreeCell(selectedChecker.X - 2, selectedChecker.Y - 2));
                FreeCellAdded.Invoke(freeCell);
            }
            else if (direction == DirectionToEatChecker.RightUp)
            {
                FreeCell freeCell = new FreeCell(selectedChecker.X + 2, selectedChecker.Y + 2);
                freeCells.Add(new FreeCell(selectedChecker.X + 2, selectedChecker.Y + 2));
                FreeCellAdded.Invoke(freeCell);
            }
            else if (direction == DirectionToEatChecker.RightDown)
            {
                FreeCell freeCell = new FreeCell(selectedChecker.X + 2, selectedChecker.Y - 2);
                freeCells.Add(new FreeCell(selectedChecker.X + 2, selectedChecker.Y - 2));
                FreeCellAdded.Invoke(freeCell);
            }
        }
        return freeCells;
    }






    public List<FreeCell> GetFreeCellToMove(Checker selectedChecker)
    {
        List<FreeCell> freeCells = new List<FreeCell>();
        if (selectedChecker is WhiteChecker)
        {
            int rightX = selectedChecker.X + 1;
            int y = selectedChecker.Y + 1;
            int leftX = selectedChecker.X - 1;
            if (leftX >= 0 && y < Board.Height && Board.Checkers[leftX, y] == null)
            {
                var freeCell = new FreeCell(leftX, y);
                freeCells.Add(freeCell);
                FreeCellAdded?.Invoke(freeCell);
            }
            if (rightX < Board.Width && y < Board.Height && Board.Checkers[rightX, y] == null)
            {
                var freeCell = new FreeCell(rightX, y);
                freeCells.Add(freeCell);
                FreeCellAdded?.Invoke(freeCell);
            }
        }
        else if (selectedChecker is BlackChecker)
        {
            int rightX = selectedChecker.X + 1;
            int y = selectedChecker.Y - 1;
            int leftX = selectedChecker.X - 1;
            if (leftX >= 0 && y >= 0 && Board.Checkers[leftX, y] == null)
            {
                var freeCell = new FreeCell(leftX, y);
                freeCells.Add(freeCell);
                FreeCellAdded?.Invoke(freeCell);
            }
            if (rightX < Board.Width && y >= 0 && Board.Checkers[rightX, y] == null)
            {
                var freeCell = new FreeCell(rightX, y);
                freeCells.Add(freeCell);
                FreeCellAdded?.Invoke(freeCell);
            }
        }
        return freeCells;
    }

    public List<Checker> AvailableCheckersToMove(IUpdateble currentUpdate)
    {
        List<Checker> availableCheckers = new List<Checker>();
        if (currentUpdate is UpdateWhiteTurn)
        {
            List<FreeCell> freeCell = new List<FreeCell>();
            foreach (var checker in Board.Checkers)
            {
                if(checker is WhiteChecker)
                {
                    freeCell = CheckNearEnemyAndGetFreeCells(checker);
                    if(freeCell.Count != 0)
                    {
                        availableCheckers.Add(checker);
                        ClearFreeCells(freeCell);
                    }
                }
            }

            if(availableCheckers.Count == 0)
            {
                foreach(var checker in Board.Checkers)
                {
                    if(checker is WhiteChecker)
                    {
                        freeCell = GetFreeCellToMove(checker);
                        if(freeCell.Count != 0)
                        {
                            availableCheckers.Add(checker);
                            ClearFreeCells(freeCell);
                        }
                    }
                }
            }
        }

        else if (currentUpdate is UpdateBlackTurn)
        {
            List<FreeCell> freeCell = new List<FreeCell>();
            foreach (var checker in Board.Checkers)
            {
                if (checker is BlackChecker)
                {
                    freeCell = CheckNearEnemyAndGetFreeCells(checker);
                    if (freeCell.Count != 0)
                    {
                        availableCheckers.Add(checker);
                        ClearFreeCells(freeCell);
                    }
                }
            }

            if (availableCheckers.Count == 0)
            {
                foreach (var checker in Board.Checkers)
                {
                    if (checker is BlackChecker)
                    {
                        freeCell = GetFreeCellToMove(checker);
                        if (freeCell.Count != 0)
                        {
                            availableCheckers.Add(checker);
                            ClearFreeCells(freeCell);
                        }
                    }
                }
            }
        }
        return availableCheckers;
    }
}
