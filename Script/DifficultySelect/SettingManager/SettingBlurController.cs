using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SettingsBlurController : MonoBehaviour
{
    public PostProcessVolume volume;
    private DepthOfField dof;

    void Start()
    {
        if (volume.profile.TryGetSettings(out dof))
        {
            dof.active = false; // Disable sa start
        }
    }

    public void OpenSettings()
    {
        dof.active = true;
    }

    public void CloseSettings()
    {
        dof.active = false;
    }
}
