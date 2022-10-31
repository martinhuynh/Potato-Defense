using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum Action
    {
        UP, DOWN, LEFT, RIGHT, STILL, PLOW
    }

    public Animator animator;
    private float speed = 1.0f;

    // Distance to target when next input could be added to movement queue.
    private float earlyWindow = 0.8f;

    // Input queue
    private LinkedList<KeyValuePair<Action, float>> actionQueue = new LinkedList<KeyValuePair<Action, float>>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If an input is entered within the threshold, it will be added to the input queue.
        
        if (actionQueue.Count != 0 && actionQueue.First.Value.Value > earlyWindow) return;
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (actionQueue.Count == 2) actionQueue.RemoveLast();
            actionQueue.AddLast(new KeyValuePair<Action, float>(Action.PLOW, 1f));
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (actionQueue.Count == 2) actionQueue.RemoveLast();
            actionQueue.AddLast(new KeyValuePair<Action, float>(Action.UP, 1f));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (actionQueue.Count == 2) actionQueue.RemoveLast();
            actionQueue.AddLast(new KeyValuePair<Action, float>(Action.LEFT, 1f));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (actionQueue.Count == 2) actionQueue.RemoveLast();
            actionQueue.AddLast(new KeyValuePair<Action, float>(Action.DOWN, 1f));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (actionQueue.Count == 2) actionQueue.RemoveLast();
            actionQueue.AddLast(new KeyValuePair<Action, float>(Action.RIGHT, 1f));
        }
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if (actionQueue.Count == 0)
        {
            // Still is never set. Just to set all other directions to false.
            UpdateAnimation(Action.STILL);
            return;
        }

        // Continue moving in that direction until it is done.
        KeyValuePair<Action, float> pair = actionQueue.First.Value;
        actionQueue.RemoveFirst();
        float value = pair.Value;
        Action action = pair.Key;
        UpdateAnimation(action);

        if (action == Action.PLOW)
        {
            // Plow
            float plowLeft = value;
            if (plowLeft != 0)
            {
                float timeToRemove = PlayerStats.harvestSpeed * Time.deltaTime;
                if (plowLeft - timeToRemove <= 0)
                {
                    // Times complete
                    TileMapManager.tileMapManager.setDirt(transform.position);
                }
                else
                {
                    plowLeft -= timeToRemove;
                    actionQueue.AddFirst(new KeyValuePair<Action, float>(action, plowLeft));
                }
                return;
            }
        } else
        {
            float amountToMove = PlayerStats.movementSpeed * Time.deltaTime;
            // Movement
            if (value - amountToMove <= 0)
            {
                amountToMove = value;
            }
            else
            {
                value -= amountToMove;
                actionQueue.AddFirst(new KeyValuePair<Action, float>(action, value));
            }
            if (action == Action.RIGHT)
            {
                pos.x += amountToMove;
            }
            else if (action == Action.LEFT)
            {
                pos.x -= amountToMove;
            }
            else if (action == Action.UP)
            {
                pos.y += amountToMove;
            }
            else if (action == Action.DOWN)
            {
                pos.y -= amountToMove;
            }
            transform.position = pos;
        }

        
    }

    public void OnTriggerStay(Collider other)
    {
        
    }

    private void UpdateAnimation(Action action)
    {
        animator.SetBool("Plow", action == Action.PLOW);
        animator.SetBool("Left", action == Action.LEFT);
        animator.SetBool("Right", action == Action.RIGHT);
        animator.SetBool("Up", action == Action.UP);
        animator.SetBool("Down", action == Action.DOWN);
        animator.speed = 1 + speed / 2f;
    }
}
