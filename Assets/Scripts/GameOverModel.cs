using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameOverModel 
    {
        public event Action<List<Checker>, bool> CheckedGameOver;

        public void CheckGameOver(List<Checker> availableCheckersToMove, bool isWhiteMove)
        {
            CheckedGameOver?.Invoke(availableCheckersToMove, isWhiteMove);
        }
    }
}