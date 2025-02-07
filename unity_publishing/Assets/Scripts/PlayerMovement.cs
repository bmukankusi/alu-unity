using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        // Get tilt values from accelerometer (x, y axes)
        float tiltX = Input.acceleration.x;
        float tiltY = Input.acceleration.y;

        // Move the player based on tilt
        Vector3 moveDirection = new Vector3(tiltX, 0, tiltY);
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
