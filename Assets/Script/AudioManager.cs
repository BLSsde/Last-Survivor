
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    private AudioSource source;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0, 0.5f)]
    public float randomPitch = 0.1f;
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }
    public AudioSource GetSource
    {
        get { return source; }
    }

    public void _Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }
    public void _PauseSound()
    {
        source.Pause();
    }
   
}

// Main Class start from here
public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    public static AudioManager instance;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            go.transform.SetParent(this.transform);
            sounds[i].SetSource(go.AddComponent<AudioSource>());
            if(sounds[i].name == "MainMenu" || sounds[i].name == "GameplayMusic")
            {
                go.GetComponent<AudioSource>().loop = true;
            }
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i]._Play();
                return;
            }
        }

    }

    public void SoundPause(string _name)
    {
       for(int i =0; i<sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i]._PauseSound();
                return;
            }
        }
    }
}
