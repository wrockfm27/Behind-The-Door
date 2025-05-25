using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonDisabler : MonoBehaviour
{
    public GameObject settingsPanel;   // Reference to the settings panel
    public Button settingsButton;      // Reference to the gear icon button

    void Update()
    {
        if (settingsPanel == null || settingsButton == null)
            return;

        // Disable the button only if the panel is active
        settingsButton.interactable = !settingsPanel.activeSelf;
    }
}
