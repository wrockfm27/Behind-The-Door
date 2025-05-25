using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartNewGame()
    {
        Debug.Log("New Game button clicked!");
        // I-start ang coroutine para sa delay
        StartCoroutine(LoadSceneWithDelay());
    }

    private System.Collections.IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(1f); // 2-second delay
        SceneManager.LoadScene("ChapterSelect");
    }
}
