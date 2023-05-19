using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CommandModel
    {
        public event Action<int, int> Clicked;

        private int _widthBoard;
        private int _heightBoard;

        public CommandModel(int widthBoard, int heightBoard)
        {
            _widthBoard = widthBoard;
            _heightBoard = heightBoard;
        }

        public void GetMousePosition(float fX, float fY)
        {
            int x = (int)Mathf.Round(fX);
            int y = (int)Mathf.Round(fY);
            if (x >= 0 && x < _widthBoard && y >= 0 && y < _heightBoard)
            {
                Clicked?.Invoke(x, y);
            }
        }
    }
}