using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterButton : MonoBehaviour
{
    public AudioSource clickSound;

    public void OnChapter1Clicked()
    {
        StartCoroutine(PlaySoundAndLoad());
    }

    IEnumerator PlaySoundAndLoad()
    {
        clickSound.Play();
        yield return new WaitForSeconds(clickSound.clip.length); // wait sound duration
        SceneManager.LoadScene("DifficultySelect");
    }
}
