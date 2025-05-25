using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // ← change to your exact scene name
        Debug.Log("Back to Main Menu");
    }
    [SerializeField] private string mainMenuSceneName = "MainMenu"; // Palitan ng exact name ng main menu scene mo

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

}
