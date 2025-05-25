using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsUIManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject exitConfirmationPanel;

    // Tawagin kapag pinindot ang "Main Menu" button sa settings panel
    public void OnMainMenuButton()
    {
        // Ipakita ang Exit Confirmation Panel
        exitConfirmationPanel.SetActive(true);
        settingsPanel.SetActive(false); // Optional: itago ang settings panel
    }

    // Tawagin kapag pinindot ang "No" sa confirmation
    public void OnCancelExit()
    {
        // Itago ulit ang confirmation panel
        exitConfirmationPanel.SetActive(false);
        settingsPanel.SetActive(true); // Optional: ibalik ang settings panel
    }

    // Tawagin kapag pinindot ang "Yes"
    public void OnConfirmExit()
    {
        // Palitan ng pangalan ng iyong Main Menu Scene
        SceneManager.LoadScene("MainMenu");
    }
}
