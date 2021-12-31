
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveSpawner spawner;
    [SerializeField] private Animator waveAnimator;
    [SerializeField] private Text waveCountdownText;
    [SerializeField] private Text waveCountText;
    [SerializeField] private WaveSpawner.SpawnState previousState;
    
    void Update()
    {
        if(spawner.ISGAMEWIN == true)
        {
            return;
        }

        switch (spawner.Spawn_StateGet)
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
        if (previousState != WaveSpawner.SpawnState.COUNTING)
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

            waveCountText.text = (spawner.Next_Wave - 1).ToString();
        }
    }

}