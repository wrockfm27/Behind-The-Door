using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuButtonHandler : MonoBehaviour

{
    public void LoadEasy()
    {
        StartCoroutine(LoadEasyWithDelay());
    }

    private IEnumerator LoadEasyWithDelay()
    {
        // 🔍 Hanapin ang GameObject na may tag na "Music"
        GameObject music = GameObject.FindWithTag("Music");

        // ⏳ Maghintay ng 3 seconds
        yield return new WaitForSeconds(3f);

        // ❌ I-destroy ang music kung meron
        if (music != null)
        {
            Destroy(music);
        }

        // ▶️ Load Gameplay_Easy scene
        SceneManager.LoadScene("MainMenu");
    }
}

