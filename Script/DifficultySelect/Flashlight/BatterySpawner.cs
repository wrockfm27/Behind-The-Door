using UnityEngine;
using System.Collections.Generic;  // <-- Import this para gumana ang List<>

public class BatterySpawner : MonoBehaviour
{
    [Header("Battery Spawner Settings")]
    public GameObject batteryPrefab; // 🔋 Battery Prefab (drag mo sa Inspector)
    public int numberOfBatteries = 5; // ✨ Ilang batteries ang gusto mo
    public Vector3 areaSize = new Vector3(10f, 1f, 10f); // 📦 Laki ng area kung saan pwedeng mag-spawn
    public Vector3 center = Vector3.zero; // 📍 Sentro ng spawning area (pwede i-set sa Inspector)
    public float minDistanceBetweenBatteries = 10f; // ⬅️ spacing control


    void Start()
    {
        SpawnBatteries();
    }

    void SpawnBatteries()
    {
        List<Vector3> placedPositions = new List<Vector3>();

        int spawned = 0;
        int maxAttempts = 100; // Prevent infinite loop

        while (spawned < numberOfBatteries && maxAttempts > 0)
        {
            Vector3 randomPos = GetRandomPosition();
            bool tooClose = false;

            foreach (Vector3 placed in placedPositions)
            {
                if (Vector3.Distance(placed, randomPos) < minDistanceBetweenBatteries)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                Instantiate(batteryPrefab, randomPos, Quaternion.identity);
                placedPositions.Add(randomPos);
                spawned++;
            }

            maxAttempts--;
        }
    }

    // 🎲 Kumuha ng random position sa loob ng area
    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
        float z = Random.Range(-areaSize.z / 2f, areaSize.z / 2f);
        float y = center.y; // 🔒 Fixed height — usually ang sahig

        return center + new Vector3(x, y, z);
    }

    // 🔳 Optional: Draw Gizmo sa editor para makita mo yung area
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, areaSize);
    }
}
