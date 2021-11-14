using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int Health = 100;
    }

    public EnemyStats _enemyStats = new EnemyStats();
    
    public void DamageEnemy(int damage)
    {
        _enemyStats.Health -= damage;
        if (_enemyStats.Health <= 0)
        {
            GameManager.KillEnemy(this);
        }
    }
}
