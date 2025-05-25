using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // ← dagdag ito
public class ChapterLockPopup : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject okButton; // ← Add this
    public TMP_Text popupText;


    public void ShowPopup(string message)
    {
        popupText.text = message;
        popupPanel.SetActive(true);
        okButton.SetActive(false); // Hide first
        StartCoroutine(ShowOKWithDelay(0.9f)); // Delay in seconds
    }

    IEnumerator ShowOKWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        okButton.SetActive(true);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}
