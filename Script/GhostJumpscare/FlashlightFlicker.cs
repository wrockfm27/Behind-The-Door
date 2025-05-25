using UnityEngine;
using System.Collections;

public class FlashlightFlicker : MonoBehaviour
{
    [Header("Flashlight Settings")]
    public Light flashlight;                 // I-assign mo dito ang Spot Light
    public float flickerInterval = 0.1f;     // Gaano kabilis ang patay-sindi

    [Header("Detection Settings")]
    public float detectionDistance = 8f;    // Layo ng detection mula sa camera
    public string ghostTag = "Ghost";        // Dapat tama ang tag ng GhostLady

    private bool isFlickering = false;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;

        // Safety check
        if (flashlight == null)
        {
            Debug.LogWarning("❗ 'flashlight' is not assigned in the Inspector.");
        }

        if (mainCam == null)
        {
            Debug.LogError("❗ Main Camera not found!");
        }
    }

    void Update()
    {
        if (mainCam == null || flashlight == null) return;

        // Cast ray from camera forward
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        RaycastHit hit;

        // Visual debug line in Scene view
        Debug.DrawRay(ray.origin, ray.direction * detectionDistance, Color.red);

        int layerMask = ~0; // Raycast sa lahat ng layers

        if (Physics.Raycast(ray, out hit, detectionDistance, layerMask))
        {
            Debug.Log("🎯 Ray hit: " + hit.collider.name);

            if (hit.collider.CompareTag(ghostTag))
            {
                if (!isFlickering)
                {
                    StartCoroutine(FlickerFlashlight());
                }
                return;
            }
        }

        // If ghost not in view or ray misses
        StopAllCoroutines();
        isFlickering = false;
        flashlight.enabled = true;
    }

    IEnumerator FlickerFlashlight()
    {
        isFlickering = true;

        while (true)
        {
            flashlight.enabled = !flashlight.enabled;
            yield return new WaitForSeconds(flickerInterval);
        }
    }
}
