using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PointsModel 
    {
        public event Action<Checker> AddedPoint;

        public int WhiteplayerPoints;
        public int BlackPlayerPoints;

        public void AddPoint(Checker checker)
        {
            AddedPoint?.Invoke(checker);
        }
    }
}