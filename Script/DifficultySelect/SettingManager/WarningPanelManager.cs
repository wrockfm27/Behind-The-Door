using UnityEngine;

public class WarningPanelManager : MonoBehaviour
{
    public GameObject warningPanel;             // Your warning UI panel
    public CanvasGroup gearButtonCanvasGroup;   // Reference to the gear icon group

    public void OpenWarning()
    {
        warningPanel.SetActive(true);
        gearButtonCanvasGroup.interactable = false;
        gearButtonCanvasGroup.blocksRaycasts = false;
    }

    public void CloseWarning()
    {
        warningPanel.SetActive(false);
        gearButtonCanvasGroup.interactable = true;
        gearButtonCanvasGroup.blocksRaycasts = true;
    }
}
