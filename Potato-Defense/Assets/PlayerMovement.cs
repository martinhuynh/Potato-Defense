using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum Direction
    {
        UP, DOWN, LEFT, RIGHT, STILL
    }

    public Animator animator;
    private float speed = 5.0f;
    private float earlyWindow = 0.2f;

    // Input queue
    private LinkedList<KeyValuePair<Direction, float>> toMove = new LinkedList<KeyValuePair<Direction, float>>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If an input is entered within the threshold, it will be added to the input queue.
        if (toMove.Count >= 2 || (toMove.Count != 0 && toMove.First.Value.Value > earlyWindow)) return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            toMove.AddLast(new KeyValuePair<Direction, float>(Direction.UP, 1f));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            toMove.AddLast(new KeyValuePair<Direction, float>(Direction.LEFT, 1f));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            toMove.AddLast(new KeyValuePair<Direction, float>(Direction.DOWN, 1f));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            toMove.AddLast(new KeyValuePair<Direction, float>(Direction.RIGHT, 1f));
        }
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if (toMove.Count == 0)
        {
            // Still is never set. Just to set all other directions to false.
            UpdateAnimation(Direction.STILL);
            return;
        }

        // Continue moving in that direction until it is done.
        float amountToMove = speed * Time.deltaTime;
        KeyValuePair<Direction, float> pair = toMove.First.Value;
        toMove.RemoveFirst();
        float leftToMove = pair.Value;
        Direction direction = pair.Key;

        UpdateAnimation(direction);

        if (leftToMove - amountToMove <= 0)
        {
            amountToMove = leftToMove;
        }
        else
        {
            leftToMove -= amountToMove;
            toMove.AddFirst(new KeyValuePair<Direction, float>(direction, leftToMove));
        }

        if (direction == Direction.RIGHT)
        {
            pos.x += amountToMove;
        }
        else if (direction == Direction.LEFT)
        {
            pos.x -= amountToMove;
        }
        else if (direction == Direction.UP)
        {
            pos.y += amountToMove;
        }
        else if (direction == Direction.DOWN)
        {
            pos.y -= amountToMove;
        }
        transform.position = pos;
    }

    private void UpdateAnimation(Direction direction)
    {
        animator.SetBool("Left", direction == Direction.LEFT);
        animator.SetBool("Right", direction == Direction.RIGHT);
        animator.SetBool("Up", direction == Direction.UP);
        animator.SetBool("Down", direction == Direction.DOWN);
        animator.speed = 1 + speed / 2f;
    }
}
