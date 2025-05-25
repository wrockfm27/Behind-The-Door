using UnityEngine;

public class PersistentSoundManager : MonoBehaviour
{
    public static PersistentSoundManager Instance;

    public AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 🔒 Para di madestruct
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClickSound()
    {
        if (audioSource != null && audioSource.clip != null)
            audioSource.Play();
    }
}
