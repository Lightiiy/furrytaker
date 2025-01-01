using System.Collections;
using UnityEngine;

namespace Game.Enemies
{
    public interface IEnemy
    {
        void DealDamage(int amount);
        IEnumerator HandleCollisionRebound(Vector2 collisionDirection);
    }
}