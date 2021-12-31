using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    [SerializeField] private float WaitingTimeBWEnemy = 0.5f;
    [SerializeField] private int maxWaves =4;
    private int currWave = 1; // Index of next wave which will be spawn
    private bool isGameWin = false;
    public bool ISGAMEWIN
    {
        get { return isGameWin; }
    }
    public int Next_Wave
    {
        get { return currWave + 1; }
    }
    public int Max_Wave
    {
        get { return maxWaves; }
    }

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    public float Wave_Countdown
    {
        get { return waveCountdown; }
    }

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    public SpawnState Spawn_StateGet
    {
        get { return state; }
    }


    //Referencing
    ObjectPooler pooler;
    private void Start()
    {
        pooler = ObjectPooler.Instance;
      
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(isGameWin == false)
        {
            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(currWave));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
    
    }
    private void GameWinIn()
    {
        GameManager.instance.GameWin();
    }

    private void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (currWave > maxWaves-1)
        {
            // Call Game Win Fn()
            isGameWin = true;
            Invoke(nameof(GameWinIn), 2f);
            
        }
        else
        {
            currWave++;
           
        }

        
    }

    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }
    IEnumerator SpawnWave(int index)
    {
        
        state = SpawnState.SPAWNING;

        for (int i = 0; i < pooler.pools[index].size; i++)
        {
           
            // Spawn Enemy Using Object pooling
            Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            pooler.SpawnFromPool("Enemy" + currWave.ToString(), _sp.position, Quaternion.identity);

            yield return new WaitForSeconds(WaitingTimeBWEnemy);
        }

        state = SpawnState.WAITING;

        yield return null;
    }

   
}