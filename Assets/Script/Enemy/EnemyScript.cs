
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;
        private int _currHealth;

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

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(_enemyStats.currHealth, _enemyStats.maxHealth);
        }

    }

    public void DamageEnemy(int damage)
    {
        _enemyStats.currHealth -= damage;
       

        if (_enemyStats.currHealth <= 0)
        {
           GameManager.instance.KillEnemy(this);
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(_enemyStats.currHealth, _enemyStats.maxHealth);
        }
    }


}
