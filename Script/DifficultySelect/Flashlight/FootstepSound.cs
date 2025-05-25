using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource footstepAudio;
    public float moveThreshold = 0.1f; // sensitivity ng pag-detect ng movement
    public Joystick joystick; // assign sa inspector kung mobile joystick

    void Update()
    {
        // Movement detection using joystick magnitude
        bool isMoving = joystick.Direction.magnitude > moveThreshold;

        if (isMoving)
        {
            if (!footstepAudio.isPlaying)
                footstepAudio.Play();
        }
        else
        {
            if (footstepAudio.isPlaying)
                footstepAudio.Stop();
        }
    }
}
