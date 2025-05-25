
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject mobileControls;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MainMenu" || sceneName == "ChapterSelect")
        {
            mobileControls.SetActive(false);  // Hide joystick
        }
        else
        {
            mobileControls.SetActive(true);   // Show joystick
        }
    }
}
