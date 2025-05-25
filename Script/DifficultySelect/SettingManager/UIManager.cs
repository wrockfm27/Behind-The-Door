using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool isSettingsOpen = false;

    [Header("UI References")]
    public GameObject flashlightButton;
    public GameObject settingsPanel;
    public GameObject warningPanel;

    [Header("Player Script Reference")]
    public PlayerMovement playerMovementScript;

    // 🟡 Open the settings panel
    public void OpenSettings()
    {
        isSettingsOpen = true;
        Time.timeScale = 0f;

        if (flashlightButton != null)
            flashlightButton.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    // 🟢 Close the settings panel and resume game
    public void CloseSettings()
    {
        isSettingsOpen = false;
        Time.timeScale = 1f;

        if (flashlightButton != null)
            flashlightButton.SetActive(true);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (playerMovementScript != null)
            playerMovementScript.ResetSwipeState();
    }

    // ⚠️ Show warning confirmation panel before returning to main menu
    public void OpenWarningPanel()
    {
        if (warningPanel != null)
            warningPanel.SetActive(true);
    }

    // ❌ Cancel going to main menu
    public void CancelWarningPanel()
    {
        if (warningPanel != null)
            warningPanel.SetActive(false);
    }

    // ✅ Confirm and go back to Main Menu
    public void ConfirmReturnToMainMenu()
    {
        Time.timeScale = 1f;
        isSettingsOpen = false;
        SceneManager.LoadScene("MainMenu"); // Palitan mo ng actual scene name kung iba
    }
}
