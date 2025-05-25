using UnityEngine;

public class BatteryAligner : MonoBehaviour
{
    void Start()
    {
        // Rotate battery to lay down (Z-axis)
        transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 90f);
    }
}
