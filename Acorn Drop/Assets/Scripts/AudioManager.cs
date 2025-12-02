using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip death;
    public AudioClip cut;
    public AudioClip pop;
    public AudioClip whoosh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        MusicSource.clip = background;
        MusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
