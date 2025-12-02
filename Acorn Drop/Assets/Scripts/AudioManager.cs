using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource BackgroundMusic;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
