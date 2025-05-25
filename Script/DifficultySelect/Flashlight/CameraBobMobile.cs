using UnityEngine;

public class CameraBobMobile : MonoBehaviour
{
    public Transform player;  // Parent na may movement (player)
    public float bobFrequency = 8f;
    public float bobAmount = 0.1f;
    public float midpoint = 0.0f;

    private float timer = 0.0f;
    private Vector3 lastPlayerPosition;

    void Start()
    {
        if (player == null)
            player = transform.root; // Default: kunin parent ng camera

        lastPlayerPosition = player.position;
        midpoint = transform.localPosition.y;
    }

    void Update()
    {
        Vector3 movement = player.position - lastPlayerPosition;
        movement.y = 0; // Ignore vertical movement (jumping etc.)
        float speed = movement.magnitude / Time.deltaTime;

        if (speed > 0.1f)
        {
            timer += Time.deltaTime * bobFrequency;
            float newY = midpoint + Mathf.Sin(timer) * bobAmount;
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }
        else
        {
            timer = 0.0f;
            Vector3 pos = transform.localPosition;
            pos.y = Mathf.Lerp(pos.y, midpoint, Time.deltaTime * 5f);
            transform.localPosition = pos;
        }

        lastPlayerPosition = player.position;
    }
}
