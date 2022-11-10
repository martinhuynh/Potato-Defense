using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum Action
    {
        UP, DOWN, LEFT, RIGHT, STILL, FARM
    }

    private float offset_x = 0.5f, offset_y = 0.7f;

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
        Vector3 pos = transform.position;
        if (actionQueue.Count != 0 && actionQueue.First.Value.Value > earlyWindow) return;
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (actionQueue.Count == 2) actionQueue.RemoveLast();
            actionQueue.AddLast(new KeyValuePair<Action, float>(Action.FARM, 1f));
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (actionQueue.Count == 2) actionQueue.RemoveLast();
            actionQueue.AddLast(new KeyValuePair<Action, float>(Action.UP, calculateDistance(pos.y, 1f) + offset_y));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (actionQueue.Count == 2) actionQueue.RemoveLast();
            actionQueue.AddLast(new KeyValuePair<Action, float>(Action.LEFT, calculateDistance(pos.x, -1f) - offset_x));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (actionQueue.Count == 2) actionQueue.RemoveLast();
            actionQueue.AddLast(new KeyValuePair<Action, float>(Action.DOWN, calculateDistance(pos.y, -1f) - offset_y));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (actionQueue.Count == 2) actionQueue.RemoveLast();
            actionQueue.AddLast(new KeyValuePair<Action, float>(Action.RIGHT, calculateDistance(pos.x, 1f) + offset_x));
        }
    }

    private float calculateDistance(float current, float amount)
    {
        return Mathf.Abs(Mathf.Floor(current + amount) - current);
    }

    void FixedUpdate()
    {
        GetComponent<Renderer>().sortingOrder = (int)(-100 * transform.position.y);

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

        if (action == Action.FARM)
        {
            // Plow
            float actionLeft = value;
            if (FarmManager.fm.plowable(transform.position) && actionLeft != 0)
            {
                UpdateAnimation(action);
                float timeToRemove = PlayerStats.farmingSpeed * Time.deltaTime;
                if (actionLeft - timeToRemove <= 0)
                {
                    FarmManager.fm.plow(transform.position);
                }
                else
                {
                    actionLeft -= timeToRemove;
                    actionQueue.AddFirst(new KeyValuePair<Action, float>(action, actionLeft));
                }
                return;
            }
            // If plantable.
            else if (FarmManager.fm.plant(transform.position)) {
                return;
            }
            else if (FarmManager.fm.harvestable(transform.position) && actionLeft != 0) {
                UpdateAnimation(action);
                float timeToRemove = PlayerStats.farmingSpeed * Time.deltaTime;
                if (actionLeft - timeToRemove <= 0)
                {
                    FarmManager.fm.harvest(transform.position);
                }
                else
                {
                    actionLeft -= timeToRemove;
                    actionQueue.AddFirst(new KeyValuePair<Action, float>(action, actionLeft));
                }
                return;
            }
        }
        else
        {
            UpdateAnimation(action);
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
        animator.SetBool("Plow", action == Action.FARM);
        animator.SetBool("Left", action == Action.LEFT);
        animator.SetBool("Right", action == Action.RIGHT);
        animator.SetBool("Up", action == Action.UP);
        animator.SetBool("Down", action == Action.DOWN);
        animator.speed = 1 + speed / 2f;
    }
}