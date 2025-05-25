using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter1Button : MonoBehaviour
{
    public string sceneToLoad = "DifficultySelect";

    public void OnChapter1Click()
    {
        StartCoroutine(PlaySoundAndLoadScene());
    }

    IEnumerator PlaySoundAndLoadScene()
    {
        PersistentSoundManager.Instance.PlayClickSound();

        float delay = PersistentSoundManager.Instance.audioSource.clip.length;
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneToLoad);
    }
}
