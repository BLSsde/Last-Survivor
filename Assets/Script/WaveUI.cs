using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveSpawner spawner;
    [SerializeField] private Animator waveAnimator;
    [SerializeField] private Text waveCountdownText;
    [SerializeField] private Text waveCountText;
    [SerializeField] private WaveSpawner.SpawnState previousState;

  
    void Start()
    {
        if(spawner == null)
        {
            Debug.LogError("NO WaveSpawner Script referenced!");
            this.enabled = false;
        }
        if(waveAnimator == null)
        {
            Debug.LogError("NO WaveAnimator Attached!");
            this.enabled = false;
        }
        if(waveCountdownText == null)
        {
            Debug.LogError("NO WaveCountdownText referenced!");
            this.enabled = false;
        }
        if(waveCountText == null)
        {
            Debug.LogError("NO WaveCountdownText referenced!");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(spawner.Spawn_StateGet)
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;

            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
        }

        previousState = spawner.Spawn_StateGet;
    }

    private void UpdateCountingUI()
    {
        if(previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);
        }

        waveCountdownText.text = ((int)spawner.Wave_Countdown).ToString();
    }
    private void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true);

            waveCountText.text = spawner.Next_Wave.ToString();
        }
    }
    
}
