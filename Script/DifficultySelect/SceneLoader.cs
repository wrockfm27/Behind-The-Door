using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadChapterSelect()
    {
        SceneManager.LoadScene("ChapterSelect"); // make sure exact ang pangalan
    }
}
