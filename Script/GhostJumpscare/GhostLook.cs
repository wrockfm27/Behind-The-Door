using UnityEngine;

public class GhostLook : MonoBehaviour
{
    [Header("📌 Reference sa Camera")]
    [Tooltip("Ito ang camera ng player, kadalasan ay Main Camera.")]
    public Transform playerCamera;  // Reference sa camera ng player

    [Header("👻 Disappear Settings")]
    [Tooltip("Kung anong anggulo (degrees) mula sa camera ang kailangan bago mawala ang ghost.")]
    public float disappearAngle = 75f; // Angle kung kailan mawawala ang ghost

    [Tooltip("Delay bago magsimulang mag-check kung dapat mawala ang ghost.")]
    public float delayBeforeCheck = 1.0f; // Gaano katagal bago simulan ang checking

    [Tooltip("Gaano katagal dapat nakatalikod ang player bago mawala ang ghost.")]
    public float minLookAwayTime = 1.0f; // Gaano katagal na nakatalikod bago mawala

    // Internal timers
    private float timeSinceActivated = 0f;  // Timer mula pag-on ng ghost
    private float lookAwayTimer = 0f;       // Timer habang nakatalikod ang player

    // ✅ Auto-assign camera kapag naka-enable ang ghost
    private void OnEnable()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main?.transform;
            Debug.LogWarning("📸 Auto-assigned Main Camera sa GhostLook script.");
        }

        timeSinceActivated = 0f;
        lookAwayTimer = 0f;
    }

    // ✅ Update kada frame para i-check ang anggulo ng tingin ng player
    private void Update()
    {
        if (playerCamera != null)
        {
            LookAtPlayerYOnly();  // Para hindi mukhang "nakahiga"
        }

        timeSinceActivated += Time.deltaTime;

        if (timeSinceActivated < delayBeforeCheck)
            return; // Hintayin muna ang delay bago simulan ang checking

        // 🔁 Kumuha ng direction mula player papunta sa ghost
        Vector3 directionToGhost = (transform.position - playerCamera.position).normalized;

        // 📏 I-compute ang angle ng tingin ng camera kumpara sa ghost
        float angle = Vector3.Angle(playerCamera.forward, directionToGhost);
        Debug.Log("👁️ Angle to ghost: " + angle);

        if (angle > disappearAngle)
        {
            lookAwayTimer += Time.deltaTime;
            Debug.Log("⏳ Look away timer: " + lookAwayTimer);

            if (lookAwayTimer >= minLookAwayTime)
            {
                Debug.Log("👻 Ghost disappears!");
                gameObject.SetActive(false); // Mawawala ang ghost
            }
        }
        else
        {
            // Kung nakatingin pa rin, i-reset ang timer
            lookAwayTimer = 0f;
        }
    }

    // ✅ Horizontal look only (di mag tilt pataas/baba)
    private void LookAtPlayerYOnly()
    {
        Vector3 direction = playerCamera.position - transform.position;
        direction.y = 0f; // I-ignore ang taas

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }
}
