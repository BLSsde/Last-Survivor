using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;
        private int _currHealth;

        public int damage = 40 ; // damage which does by enemy to the player

        public int currHealth
        {
            get { return _currHealth; }
            set { _currHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            currHealth = maxHealth;
        }
    }

    public EnemyStats _enemyStats = new EnemyStats();

    public GameObject enemyDeathParticles;
    public float shakeAmnt = 0.1f;
    public float shakeLength = 0.1f;

    [Header("Optional:")]
    [SerializeField] private StatusIndicator statusIndicator;

    private void Start()
    {
        _enemyStats.Init();

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(_enemyStats.currHealth, _enemyStats.maxHealth);
        }

        if(enemyDeathParticles == null)
        {
            Debug.LogError("No death particles ");
        }
   
    }

    public void DamageEnemy(int damage)
    {
        _enemyStats.currHealth -= damage;
        if (_enemyStats.currHealth <= 0)
        {
            GameManager.KillEnemy(this);
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(_enemyStats.currHealth, _enemyStats.maxHealth);
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        Player _player = _collision.collider.GetComponent<Player>();
        if( _player != null)
        {
            _player.DamagePlayer(_enemyStats.damage);
        }
    }
}
