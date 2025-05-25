using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject settingsPanel;       // Panel that appears like paper
    public GameObject joystickObject;      // Joystick to disable/enable

    [Header("Sliders")]
    public Slider sensitivitySlider;
    public Slider volumeSlider;

    [Header("Buttons")]
    public Button resumeButton;
    public Button menuButton;

    private float currentSensitivity = 1f;

    void Start()
    {
        // Load saved settings
        currentSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);

        // Apply saved settings
        sensitivitySlider.value = currentSensitivity;
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        // Apply to player if found
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().UpdateSensitivity(currentSensitivity);
        }

        // Listeners for UI
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        resumeButton.onClick.AddListener(ResumeGame);
        menuButton.onClick.AddListener(GoToMainMenu);

        // Hide panel at start
        settingsPanel.SetActive(false);
    }

    // Called when gear icon is pressed
    public void ToggleSettingsPanel()
    {
        bool active = !settingsPanel.activeSelf;
        settingsPanel.SetActive(active);
        Time.timeScale = active ? 0 : 1;

        // Toggle joystick visibility
        if (joystickObject != null)
            joystickObject.SetActive(!active);
    }

    // Apply sensitivity
    public void OnSensitivityChanged(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value);

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().UpdateSensitivity(value);
        }
    }

    // Apply volume
    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    // Resume gameplay
    public void ResumeGame()
    {
        // ✅ Call UIManager to handle closing
        GameObject uiManager = GameObject.Find("UIManager");
        if (uiManager != null)
        {
            UIManager ui = uiManager.GetComponent<UIManager>();
            ui.CloseSettings(); // This handles Time.timeScale, panel, flag, swipe reset
        }

        // ✅ Toggle joystick back on (if not already handled)
        if (joystickObject != null)
            joystickObject.SetActive(true);
    }

    // Go back to main menu scene
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu"); // Make sure this matches your actual menu scene name
    }
}
