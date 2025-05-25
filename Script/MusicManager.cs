using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource bgmSource;

    // Ilagay dito ang names ng scenes na gusto mong may background music
    public string[] allowedScenes = { "MainMenu", "ChapterSelect", "DifficultySelect" };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Para hindi ma-destroy pag lipat ng scene
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
            return;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CheckIfShouldPlayBGM(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckIfShouldPlayBGM(scene.name);
    }

    void CheckIfShouldPlayBGM(string sceneName)
    {
        bool shouldPlay = false;

        foreach (string s in allowedScenes)
        {
            if (s == sceneName)
            {
                shouldPlay = true;
                break;
            }
        }

        if (shouldPlay)
        {
            if (!bgmSource.isPlaying)
                bgmSource.Play();
        }
        else
        {
            bgmSource.Stop();
        }
    }
}
