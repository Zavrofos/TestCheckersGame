using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Update
{
    public interface IStateble 
    {
        void Move(int mousePositionX, int mousePositionY, GameModel gameModel);
    }
}