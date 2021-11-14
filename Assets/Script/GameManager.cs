using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private void Awake()
    {
        
        if(gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameManager>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    [SerializeField] private float spawnDelay = 3f;
    public GameObject spawnPrefab;

    public IEnumerator RespawnPlayer()
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
        gameManager.StartCoroutine(gameManager.RespawnPlayer());
    }
    
    public static void KillEnemy(Enemy enemy )
    {
        Destroy(enemy.gameObject);
        
    }
}
