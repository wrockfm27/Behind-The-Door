using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSound : MonoBehaviour
{
    public AudioSource clickSound;

    public void LoadSceneWithSound(string sceneName)
    {
        StartCoroutine(PlaySoundAndLoad(sceneName));
    }

    IEnumerator PlaySoundAndLoad(string sceneName)
    {
        // Gawing persistent ang AudioSource GameObject para di madelete sa scene change
        DontDestroyOnLoad(clickSound.gameObject);

        clickSound.Play();
        yield return new WaitForSeconds(clickSound.clip.length); // hintayin matapos ang sound

        SceneManager.LoadScene(sceneName);

        // Optional: Destroy mo na lang manually pagkatapos kung ayaw mong permanenteng nandyan
        Destroy(clickSound.gameObject, 0.5f); // hintayin lang para di tumambay
    }
}
