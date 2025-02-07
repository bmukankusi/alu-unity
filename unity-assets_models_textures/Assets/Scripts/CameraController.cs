using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Assign Player in the Inspector
    public Vector3 offset = new Vector3(0, 2, -5); // Camera offset from Player
    public float rotationSpeed = 100f;
    public bool requireRightClick = true; // Toggle free look or right-click drag

    private float yaw = 0f;  // Rotation around Y-axis
    private float pitch = 0f; // Rotation around X-axis

    private void Start()
    {
        transform.position = player.position + offset; // Set initial position
        transform.LookAt(player);
    }

    private void LateUpdate()
    {
        // Camera follows player
        transform.position = player.position + offset;

        // Rotate camera based on mouse movement
        if (!requireRightClick || Input.GetMouseButton(1)) // Right-click to rotate
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -40f, 80f); // Prevents extreme up/down rotation

            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }
}
