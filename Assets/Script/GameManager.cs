using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerShooting playerShootingRef;

    public static GameManager instance;
    private SaveGameData saveGameData;
    private Player player;
    private ObjectPooler pooler;
    [SerializeField] private StatusIndicator statusIndicator;

    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private GameObject gameWinDisplay;
    [SerializeField] private GameObject playerInfoDisplay;

    // for live counts
    [SerializeField] private int maxLives = 2;
    private static int remainingLives;
    AudioManager audioManager;

    //Texts
    [SerializeField] private Text[] FinishesShowTxt;

    // Spawning Player
    public Transform playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnDelay = 3f;
    public GameObject spawnPrefab;

    private int enemyKillCount;

    //  ---------------------------############
    public int[] percentage = { 7, 3 };  // 7 for coins and 3 for health
    public int total = 10;
    public int randomNumber;

    public static int Remaining_Lives
    {
        get { return remainingLives; }
    }

    private void Awake()
    {
        
        if(instance == null)
        {
            instance = this;
        }

        
    }
    private void Start()
    {
        playerShootingRef.enabled = true;

        player = Player.Instance;
        audioManager = AudioManager.instance;
        saveGameData = SaveGameData.instance;
        pooler = ObjectPooler.Instance;
        remainingLives = maxLives;


        audioManager.PlaySound("GameplayMusic");
        audioManager.SoundPause("MainMenu");
        

    }

    public void EndGame()
    {

        gameOverDisplay.SetActive(true);
        playerInfoDisplay.SetActive(false);
        gameWinDisplay.SetActive(false);
        

        // save coins and finishes to file
        saveGameData.coins += player.CollectedCoinsInAGame;
        saveGameData.finishes += enemyKillCount;
        saveGameData.Save();

    }
    public void GameWin()
    {
        playerShootingRef.enabled = false; //disable the shooting script

        gameOverDisplay.SetActive(false);
        playerInfoDisplay.SetActive(false);
        gameWinDisplay.SetActive(true);
        

        // save coins and finishes to file
        saveGameData.coins += player.CollectedCoinsInAGame;
        saveGameData.finishes += enemyKillCount;
        saveGameData.currentLevel += 1;
        saveGameData.Save();
    }

    IEnumerator RespawnPlayer()
    {
        
        audioManager.PlaySound("Respawning");  // give the currect name of sound
        
        yield return new WaitForSeconds(spawnDelay);

        player._playerStats.Init(); // set health to max health
        statusIndicator.SetHealth(player._playerStats.currHealth, player._playerStats.maxHealth);

        player.transform.SetPositionAndRotation(spawnPoint.position, Quaternion.identity);
        player.gameObject.SetActive(true);

        //Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        GameObject clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        Destroy(clone, 3f);
    }

    public void KillPlayer(Player player )
    {
        //Destroy(player.gameObject);
        player.gameObject.SetActive(false);

        remainingLives -= 1;
        if (remainingLives <= 0)
        {
            audioManager.PlaySound("GameOver");
            Invoke(nameof(EndGame), 2f);
            
        }
        else
        {
            StartCoroutine(RespawnPlayer());
        }

    }

    public void KillEnemy(EnemyScript _enemy)
    {

        GameObject _clone = Instantiate(_enemy.enemyDeathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
        Destroy(_clone, 1f);
        
        CameraShake.Instance.ShakeCamera(_enemy.shakeAmnt, _enemy.shakeLength);
        //Destroy(_enemy.gameObject);
        _enemy.gameObject.SetActive(false);

        // increase kill count by 1
        enemyKillCount++;

        //show finishes
        for (int i = 0; i < FinishesShowTxt.Length; i++)
        {
            FinishesShowTxt[i].text = "FINISHES: " + enemyKillCount.ToString();
        }




        //---- Spawn Coins and Health randomly

        randomNumber = Random.Range(0, total);
        for (int i = 0; i < percentage.Length; i++)
        {
            if (randomNumber <= percentage[i])
            {
                if (i == 0)
                {
                    // spawn coins

                    pooler.SpawnFromPool("Coins", _enemy.transform.position, Quaternion.identity);
                    return;

                }
                else if (i == 1)
                {
                    // spawn health

                    pooler.SpawnFromPool("Health", _enemy.transform.position, Quaternion.identity);
                    return;
                }

            }
            else
            {
                randomNumber -= percentage[i];
            }
        }


    }

}
