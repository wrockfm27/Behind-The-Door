using UnityEngine;
// 🟡 ADD: If using Input System
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Joystick Input")]
    public FixedJoystick joystick;

    [Header("Camera References")]
    public Transform cameraPivot;      // Pivot object for pitch (up/down) rotation of camera
    public Transform cameraYaw;        // Player or parent object that rotates left/right (yaw)

    [Header("Character Movement")]
    public CharacterController controller;
    public float moveSpeed = 4f;

    [Header("Camera Rotation Settings")]
    public float rotationSpeed = 0.2f;
    public float pitchMin = -40f;
    public float pitchMax = 60f;

    private float controlSensitivity = 1f;
    private float yaw = 0f;
    private float pitch = 0f;
    private Vector2 lastTouchPosition;
    private bool isDragging = false;

    private PlayerInputAction inputActions;

    void Start()
    {
        controlSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1f);

        inputActions = new PlayerInputAction();
        inputActions.Enable();

        // Reset any UI-related states
        UIManager.isSettingsOpen = false;
        isDragging = false;
    }

    void Update()
    {
        HandleMovement();
        HandleSwipeRotation();
    }

    void HandleMovement()
    {
        Vector3 camForward = cameraPivot.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cameraPivot.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveInput = camRight * joystick.Horizontal + camForward * joystick.Vertical;

        if (moveInput.magnitude > 1f)
            moveInput = moveInput.normalized;

        controller.Move(moveInput * moveSpeed * Time.deltaTime);
    }

    void HandleSwipeRotation()
    {
        if (UIManager.isSettingsOpen)
            return;

        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x > Screen.width / 2)
            {
                if (touch.phase == UnityEngine.TouchPhase.Began)
                {
                    lastTouchPosition = touch.position;
                    isDragging = true;
                }
                else if (touch.phase == UnityEngine.TouchPhase.Moved && isDragging)
                {
                    Vector2 delta = touch.position - lastTouchPosition;

                    yaw += delta.x * rotationSpeed * controlSensitivity;
                    cameraYaw.rotation = Quaternion.Euler(0, yaw, 0);

                    pitch -= delta.y * rotationSpeed * controlSensitivity;
                    pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
                    cameraPivot.localRotation = Quaternion.Euler(pitch, 0, 0);

                    lastTouchPosition = touch.position;
                }
                else if (touch.phase == UnityEngine.TouchPhase.Ended)
                {
                    isDragging = false;
                }
            }
        }
    }

    public void ResetSwipeState()
    {
        isDragging = false;
    }

    public void UpdateSensitivity(float newSensitivity)
    {
        controlSensitivity = newSensitivity;
    }

    void OnDisable()
    {
        if (inputActions != null)
            inputActions.Disable();
    }

    void OnDestroy()
    {
        if (inputActions != null)
            inputActions.Dispose();
    }
}
