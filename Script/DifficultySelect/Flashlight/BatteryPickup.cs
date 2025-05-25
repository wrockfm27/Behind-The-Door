using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public Camera mainCamera; // Reference sa Main Camera
    public FlashlightSystem flashlightScript; // Reference sa Flashlight System script
    public float rechargeAmount = 25f; // Amount na madadagdag sa battery

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Touch start (tapping)
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = mainCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Battery"))
                    {
                        // Refill battery
                        flashlightScript.RechargeBattery(rechargeAmount);

                        // Destroy or deactivate battery object
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
