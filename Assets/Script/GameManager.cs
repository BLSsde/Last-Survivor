using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private int maxLives = 3;
    private static int remainingLives;
    public static int Remaining_Lives
    {
        get { return remainingLives; }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    [SerializeField] private float spawnDelay = 3f;
    public GameObject spawnPrefab;

    public CameraShake cameraShake;
    [SerializeField] private GameObject gameOverDisplay;

    // reference to audioManager
    private AudioManager audioManager;

    private void Awake()
    {
        
        if(gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameManager>();
        }
    }
    private void Start()
    {
        remainingLives = maxLives;
        if(cameraShake == null)
        {
            Debug.LogError("No camera shake referenced in GameManager ");
        }

        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No AudioManager in the scene");
        }
    }

    public void EndGame()
    {
        gameOverDisplay.SetActive(true);

    }

    public IEnumerator _RespawnPlayer()
    {
        audioManager.PlaySound("Respawn");  // give the currect name of sound
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        Destroy(clone, 3f);
    }

    public static void KillPlayer(Player player )
    {
        Destroy(player.gameObject);
        remainingLives -= 1;
        if(remainingLives <= 0)
        {
            gameManager.EndGame();
        }
        else
        {
            gameManager.StartCoroutine(gameManager._RespawnPlayer());
        }
    }
    
    public static void KillEnemy(Enemy enemy )
    {
        gameManager._KillEnemy(enemy);
        
    }
    // local KillEnemy Method
    public void _KillEnemy(Enemy _enemy)
    {
        GameObject _clone = Instantiate(_enemy.enemyDeathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
        Destroy(_clone, 5f);
        cameraShake.Shake(_enemy.shakeAmnt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }

}
