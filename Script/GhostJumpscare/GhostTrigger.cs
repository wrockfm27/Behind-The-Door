using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    public GameObject[] ghosts; // Array of ghost GameObjects
    public Transform player;
    public float spawnDistance = 1f; // Distance ng ghost mula sa player
    public LayerMask obstacleLayer; // Mga layers kung saan hindi pwede dumaan ang ghost (tulad ng pader)

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            ShowGhostBehindPlayer();
        }
    }

    void ShowGhostBehindPlayer()
    {
        if (ghosts == null || ghosts.Length == 0 || player == null)
        {
            Debug.LogWarning("❗ Ghosts or Player reference is missing sa Inspector!");
            return;
        }

        Vector3 spawnPosition = player.position - player.forward * spawnDistance;
        RaycastHit hit;

        // Optional: Check kung may obstacle directly behind
        if (Physics.Raycast(player.position, -player.forward, out hit, spawnDistance, obstacleLayer))
        {
            spawnPosition = hit.point + hit.normal * 0.5f;
        }

        // 🔽 NEW: Raycast pababa mula sa spawnPosition para ma-align sa lupa
        if (Physics.Raycast(spawnPosition + Vector3.up * 2f, Vector3.down, out RaycastHit groundHit, 5f))
        {
            spawnPosition.y = groundHit.point.y + 0.05f; // konting taas para di mag-clipping
        }
        else
        {
            Debug.LogWarning("⚠️ Ground not detected sa ilalim ng ghost spawn point.");
        }

        foreach (GameObject ghost in ghosts)
        {
            ghost.transform.position = spawnPosition;
            ghost.transform.LookAt(player);
            ghost.SetActive(true);
        }

        AudioSource ghostAudio = ghosts[0].GetComponent<AudioSource>();
        if (ghostAudio != null)
        {
            ghostAudio.Play();
        }
    }

}
