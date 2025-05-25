using UnityEngine;

public class MobileLook : MonoBehaviour
{
    [Header("Sensitivity Settings")]
    [SerializeField] private float lookSensitivity = 0.1f;

    [Header("Deadzone Settings")]
    [SerializeField] private float inputDeadzone = 0.05f;

    [Header("Player Reference")]
    public Transform playerBody;

    private float xRotation = 0f;
    private int lookFingerId = -1;
    private Vector2 lastTouchPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerBody == null)
        {
            Debug.LogWarning("Player Body not assigned in MobileLook script!");
        }
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // PC testing (mouse input)
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        RotateCamera(mouseX, mouseY);
#else
        // Mobile input
        HandleTouchLook();
#endif
    }

    void HandleTouchLook()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
            {
                lookFingerId = touch.fingerId;
                lastTouchPosition = touch.position;
            }
            else if (touch.fingerId == lookFingerId && touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition;

                if (delta.magnitude > inputDeadzone * 100f)
                {
                    float lookX = delta.x * lookSensitivity;
                    float lookY = delta.y * lookSensitivity;

                    // Debug logs (optional)
                    // Debug.Log("LookX: " + lookX + " | LookY: " + lookY);

                    RotateCamera(lookX, lookY);
                }

                lastTouchPosition = touch.position;
            }
            else if (touch.fingerId == lookFingerId && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                lookFingerId = -1;
            }
        }
    }

    void RotateCamera(float lookX, float lookY)
    {
        // Vertical rotation (camera only)
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -45f, 60f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (player body)
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * lookX);
        }
    }
}
