using Assets.Scripts.Update;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ComputerPlayerBlack 
    {
        public event Action MoverComputerPlayer;
        public void Move()
        {
            MoverComputerPlayer?.Invoke();
        }
    }
}