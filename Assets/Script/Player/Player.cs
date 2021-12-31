
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
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

    //for daimond and coins
    [SerializeField] private GameObject CoinDestroyParticle;
    // UI Info
    
    private int coinsInAGame;
    [SerializeField] private Text coinsInPlayerInfo;
    [SerializeField] private Text[] CoinShowTxt;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
       
        
    }
    private void Start()
    {
        coinsInAGame = 0;
       
        _playerStats.Init();

        statusIndicator.SetHealth(_playerStats.currHealth, _playerStats.maxHealth);
       
    }

    public int CollectedCoinsInAGame
    {
        get { return coinsInAGame; }
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
        AudioManager.instance.PlaySound("Damage");  //Audio

        if (_playerStats.currHealth <= 0)
        {
            GameManager.instance.KillPlayer(this);
        }

        statusIndicator.SetHealth(_playerStats.currHealth, _playerStats.maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.CompareTag("Coins"))
        {
            // Increase Coins by 1 
            coinsInAGame++;
            //show coins 
            coinsInPlayerInfo.text = coinsInAGame.ToString();

            for (int i = 0; i < CoinShowTxt.Length; i++)
            {
                CoinShowTxt[i].text = "COINS: " + coinsInAGame.ToString();
            }

            AudioManager.instance.PlaySound("Bonus");

            GameObject _clone = Instantiate(CoinDestroyParticle, target.transform.position, Quaternion.identity) as GameObject;
            target.gameObject.SetActive(false); // disabling the coins object
            Destroy(_clone, 0.5f);
            
        }
        
    }
}
