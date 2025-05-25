using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter1Select : MonoBehaviour
{
    public void GoToDifficulty()
    {
        SceneManager.LoadScene("DifficultySelect");
    }
}
