using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private bool isSwiping = false;

    [SerializeField] private float swipeThreshold = 50f; // Minimum distance para mag-trigger ang swipe
    [SerializeField] private float moveSpeed = 5f;

    private void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    endTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    if (isSwiping)
                    {
                        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                        if (swipeDelta.magnitude >= swipeThreshold)
                        {
                            Vector2 swipeDirection = swipeDelta.normalized;
                            MoveInDirection(swipeDirection);
                        }
                    }

                    isSwiping = false;
                    break;
            }
        }
    }

    private void MoveInDirection(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
