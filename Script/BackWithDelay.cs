using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackWithDelay : MonoBehaviour
{
    public AudioClip backSoundClip;
    public float delayBeforeLoad = 1f;

    private GameObject persistentAudioObject;

    public void OnBackPressed()
    {
        // Gawa tayo ng AudioSource sa runtime
        persistentAudioObject = new GameObject("BackSoundPlayer");
        AudioSource source = persistentAudioObject.AddComponent<AudioSource>();
        source.clip = backSoundClip;
        source.Play();

        // Huwag i-destroy pag lipat ng scene
        DontDestroyOnLoad(persistentAudioObject);

        StartCoroutine(LoadMenuAfterDelay());
    }

    IEnumerator LoadMenuAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene("MainMenu");
    }
}
