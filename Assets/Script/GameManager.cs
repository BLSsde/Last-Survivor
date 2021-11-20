using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public Transform playerPrefab;
    public Transform spawnPoint;
    [SerializeField] private float spawnDelay = 3f;
    public GameObject spawnPrefab;

    public CameraShake cameraShake;

    private void Awake()
    {
        
        if(gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameManager>();
        }
    }
    private void Start()
    {
        if(cameraShake == null)
        {
            Debug.LogError("No camera shake referenced in GameManager ");
        }
    }

    public IEnumerator _RespawnPlayer()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        Destroy(clone, 3f);
    }

    public static void KillPlayer(Player player )
    {
        Destroy(player.gameObject);
        gameManager.StartCoroutine(gameManager._RespawnPlayer());
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
