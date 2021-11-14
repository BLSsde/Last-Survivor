using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int Health = 100;
    }

    public PlayerStats _playerStats = new PlayerStats();
    [SerializeField] private int fallBoundary = -20;

    private void Update()
    {
        if(transform.position.y <= fallBoundary)
        {
            DamagePlayer(1000);
        }
    }
    public void DamagePlayer( int damage)
    {
        _playerStats.Health -= damage;
        if (_playerStats.Health <= 0)
        {
            GameManager.KillPlayer(this);
        }
    }
}
