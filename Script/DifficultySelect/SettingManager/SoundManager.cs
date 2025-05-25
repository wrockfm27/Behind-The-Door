using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton pattern: Only one SoundManager across all scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make sure this stays across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
            return; // Stop here so we don't continue in the duplicate
        }

        // Get AudioSource attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // If there's no AudioSource, add one automatically
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found. Adding one automatically.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Optional: re-check in Start if audioSource got lost
    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            Debug.Log("AudioSource was restored in Start().");
        }
    }

    // Public method to play click sound
    public void PlayClickSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Cannot play click sound — missing AudioSource or AudioClip.");
        }
    }
}
