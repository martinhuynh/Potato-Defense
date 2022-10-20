using UnityEngine;

/*
 * Modify wasd_movement_2d to grid style/tile movement.
 * Limit movement so that it stays on the screen.
*/

public class Movement : MonoBehaviour
{
    public KeyCode jumpKey;

    private float speed;

    // Limits movement of the square.
    // Middle of screen is (0, 0).
    private float maxXOffset = 1f;
    private float maxYOffset = 1f;
    private float maxWidth = 9f;
    private float maxHeight = 4f;

    void Start()
    {
        speed = 1f;
    }

    void Update()
    {
        Vector3 pos = transform.position;

        // When W is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Check if action is within bounds.
            if (maxXOffset + maxHeight > pos.y + speed)
            {
                pos.y += speed;
            }
        }

        // When S is pressed
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Check if action is within bounds.
            if (-1f * (maxXOffset + maxHeight) < pos.y - speed)
            {
                pos.y -= speed;
            }
        }

        // When A is pressed
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Check if action is within bounds.
            if (-1f * (maxYOffset + maxWidth) < pos.x - speed)
            {
                pos.x -= speed;
            }
        }

        // When D is pressed
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Check if action is within bounds.
            if (maxYOffset + maxWidth > pos.x + speed)
            {
                pos.x += speed;
            }
        }

        transform.position = pos;
    }
}
