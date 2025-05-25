using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerInputAction playerInput;

    void Awake()
    {
        playerInput = new PlayerInputAction();
        playerInput.Enable(); // ✅ Enable input on start
    }

    void OnDisable()
    {
        playerInput.Disable(); // ✅ Proper cleanup
    }

    void OnDestroy()
    {
        playerInput.Dispose(); // ✅ Optional cleanup for safety
    }

    // Optional method to disable input manually (e.g. before scene change)
    public void ForceDisableInput()
    {
        if (playerInput != null)
        {
            playerInput.Disable();
        }
    }
}
