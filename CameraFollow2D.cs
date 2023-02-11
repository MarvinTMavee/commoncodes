using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    // The transform component of the object to follow
    public Transform target;

    // The speed at which the camera follows the target
    public float smoothSpeed = 0.5f;

    // The offset distance between the camera and the target
    public Vector2 offset;

    // The size of the area near the edges of the camera where the player's position will trigger a quicker camera movement
    public float edgeBuffer = 1f;

    void LateUpdate()
    {
        // Calculate the desired position for the camera based on the target's position and the offset
        Vector3 desiredPosition = (Vector3)target.position + (Vector3)offset;

        // Calculate the difference between the target's x position and the camera's x position
        float xDiff = target.position.x - transform.position.x;

        // Calculate the difference between the target's y position and the camera's y position
        float yDiff = target.position.y - transform.position.y;

        // Calculate half the width of the screen based on the camera's orthographic size and aspect ratio
        float screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        // Calculate half the height of the screen based on the camera's orthographic size
        float screenHalfHeight = Camera.main.orthographicSize;

        // Check if the target is near the edge of the screen along the x axis
        if (Mathf.Abs(xDiff) > screenHalfWidth - edgeBuffer)
        {
            // If the target is near the edge, set the desired x position to be equal to the target's x position
            desiredPosition.x = target.position.x;
        }

        // Check if the target is near the edge of the screen along the y axis
        if (Mathf.Abs(yDiff) > screenHalfHeight - edgeBuffer)
        {
            // If the target is near the edge, set the desired y position to be equal to the target's y position
            desiredPosition.y = target.position.y;
        }

        // Smoothly move the camera from its current position to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Set the camera's z position to be fixed at -10
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
