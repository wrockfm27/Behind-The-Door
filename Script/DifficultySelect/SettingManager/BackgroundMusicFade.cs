using UnityEngine;
using System.Collections;

public class BackgroundMusicFade : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.volume = 0.5f;     // Start at 50% volume
            audioSource.playOnAwake = false;
            audioSource.Stop();           // Just in case
            StartCoroutine(DelayedPlayAndFade());
        }
    }

    private IEnumerator DelayedPlayAndFade()
    {
        yield return new WaitForSeconds(3f);   // ⏳ Wait 3 seconds

        audioSource.Play();                    // ▶️ Start music
        float duration = 20f;
        float startVolume = audioSource.volume;
        float targetVolume = 1.0f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        audioSource.volume = targetVolume; // Ensure it's full at the end
    }
}
