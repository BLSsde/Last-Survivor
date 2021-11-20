using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private int _currHealth;
        public int currHealth
        {
            get { return _currHealth;  }
            set { _currHealth = Mathf.Clamp(value, 0, maxHealth);  }
        }

        public void Init()
        {
            currHealth = maxHealth;
        }
    }

    public PlayerStats _playerStats = new PlayerStats();
    [SerializeField] private int fallBoundary = -20;

    [SerializeField] private StatusIndicator statusIndicator;

    private void Start()
    {
        _playerStats.Init();

        if(statusIndicator == null)
        {
            Debug.LogError("No status Indicator found on player object ");
        }
        else
        {
            statusIndicator.SetHealth(_playerStats.currHealth, _playerStats.maxHealth);
        }
    }

    private void Update()
    {
        if(transform.position.y <= fallBoundary)
        {
            DamagePlayer(1000);
        }
    }

    public void DamagePlayer( int damage)
    {
        _playerStats.currHealth -= damage;
        if (_playerStats.currHealth <= 0)
        {
            GameManager.KillPlayer(this);
        }

        statusIndicator.SetHealth(_playerStats.currHealth, _playerStats.maxHealth);
    }
}
