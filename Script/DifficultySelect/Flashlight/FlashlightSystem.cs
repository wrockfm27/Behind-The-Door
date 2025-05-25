using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Audio; // Optional kung gagamit ka ng AudioMixer

public class FlashlightSystem : MonoBehaviour
{
    // 🔦 FLASHLIGHT SETTINGS
    [Header("Flashlight Settings")]
    public Light flashlight;
    public float batteryLife = 100f;      // Initial battery percentage
    public float drainRate = 0.1f;        // Rate at which battery drains per second

    // 🖥️ UI ELEMENTS
    [Header("UI")]
    public TextMeshProUGUI batteryStatusText;  // Text para sa battery status
    public GameObject emptyBatteryWarning;     // Panel na may CanvasGroup para sa warning
    public GameObject flashlightButton;        // 🔥 Button na mag-toggle ng flashlight

    // 🔊 AUDIO
    [Header("Audio")]
    public AudioSource audioSource;            // Audio source na gagamitin
    public AudioClip flashlightOnSound;        // Sound kapag binuksan ang flashlight
    public AudioClip flashlightOffSound;       // Sound kapag pinatay ang flashlight

    // 🔧 INTERNAL STATE
    private bool isFlashlightOn = false;      // State kung naka-on o off ang flashlight
    private bool isShowingWarning = false;    // State kung ipinapakita ang warning
    private CanvasGroup warningCanvasGroup;   // Canvas group para sa smooth fade effect

    // 🟢 INIT - Initialize components and settings
    void Start()
    {
        flashlight.enabled = false; // Default: naka-off ang flashlight
        isFlashlightOn = false;

        // Kunin ang CanvasGroup para sa fade effect ng warning
        if (emptyBatteryWarning != null)
        {
            warningCanvasGroup = emptyBatteryWarning.GetComponent<CanvasGroup>();
            warningCanvasGroup.alpha = 0f;
            emptyBatteryWarning.SetActive(false);
        }

        // ✅ UPDATE: Enable flashlight button lang kapag may battery
        if (flashlightButton != null)
        {
            flashlightButton.SetActive(batteryLife > 0f); // Ipakita lang kung may battery
        }

        UpdateBatteryUI(); // I-update agad ang battery UI sa start
    }

    // 🔁 UPDATE LOOP - Main logic for battery drain and flashlight control
    void Update()
    {
        // Kung naka-on ang flashlight at may battery pa
        if (flashlight.enabled && batteryLife > 0f)
        {
            batteryLife -= drainRate * Time.deltaTime;
            batteryLife = Mathf.Clamp(batteryLife, 0f, 100f); // Limit ng battery percentage
            UpdateBatteryUI();

            // Kung ubos na ang battery, auto-turn off ng flashlight
            if (batteryLife <= 0f)
            {
                flashlight.enabled = false;
                isFlashlightOn = false;
                if (flashlightButton != null)
                {
                    flashlightButton.SetActive(false); // I-disable kapag ubos na ang battery
                }
            }
        }
    }

    // 🔘 BUTTON CALL - Toggle flashlight through button
    public void ToggleFlashlightButton()
    {
        if (batteryLife > 0)
        {
            ToggleFlashlight(!isFlashlightOn);
        }
        else
        {
            ShowBatteryWarning(); // Show warning kung ubos ang battery
        }
    }

    // 🔁 TOGGLE FLASHLIGHT - Actual logic for turning flashlight on/off
    private void ToggleFlashlight(bool state)
    {
        isFlashlightOn = state;
        flashlight.enabled = state;

        // 🎵 Play audio when toggling
        if (audioSource != null)
        {
            if (state && flashlightOnSound != null)
                audioSource.PlayOneShot(flashlightOnSound);
            else if (!state && flashlightOffSound != null)
                audioSource.PlayOneShot(flashlightOffSound);
        }

        if (state) HideBatteryWarning(); // Auto-hide warning kapag na-on ang flashlight
    }

    // ❌ HIDE WARNING - Hide warning panel
    private void HideBatteryWarning()
    {
        if (emptyBatteryWarning != null)
        {
            emptyBatteryWarning.SetActive(false);
            isShowingWarning = false;
        }
    }

    // 🔁 UPDATE BATTERY UI - Updates the battery status text
    private void UpdateBatteryUI()
    {
        if (batteryStatusText != null)
            batteryStatusText.text = "Battery: " + Mathf.RoundToInt(batteryLife) + "%";
    }

    // ⚠️ SHOW WARNING - Show warning when battery is empty
    private void ShowBatteryWarning()
    {
        if (emptyBatteryWarning != null && !isShowingWarning)
        {
            isShowingWarning = true;
            emptyBatteryWarning.SetActive(true);
            StartCoroutine(FadeOutWarning());
        }
    }

    // 🕶️ FADE OUT WARNING - Smooth fade out effect for the warning panel
    private IEnumerator FadeOutWarning()
    {
        warningCanvasGroup.alpha = 1f;
        yield return new WaitForSeconds(2f);

        float fadeDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            warningCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        warningCanvasGroup.alpha = 0f;
        emptyBatteryWarning.SetActive(false);
        isShowingWarning = false;
    }

    // 🔋 RECHARGE BATTERY - Logic for refilling the battery
    public void RechargeBattery(float amount)
    {
        batteryLife = Mathf.Clamp(batteryLife + amount, 0f, 100f);
        UpdateBatteryUI();

        // Auto-on flashlight kapag may battery ulit
        if (batteryLife > 0 && !flashlight.enabled)
        {
            ToggleFlashlight(true);
            if (flashlightButton != null)
            {
                flashlightButton.SetActive(true); // Enable button kapag may battery na ulit
            }
        }
    }

    // ✅ RESUME GAME - Method na tatawagin pag ni-resume ang game
    public void ResumeGame()
    {
        if (flashlightButton != null)
        {
            Debug.Log("ResumeGame: Flashlight button reference found.");
            flashlightButton.SetActive(true);
            Debug.Log("Flashlight button is now active.");
        }
        else
        {
            Debug.LogWarning("ResumeGame: Flashlight button reference is NULL.");
        }
    }
}
