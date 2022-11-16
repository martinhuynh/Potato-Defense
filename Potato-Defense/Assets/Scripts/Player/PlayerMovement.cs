using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private ItemManager itemManager;

    [SerializeField]
    private FarmManager farmManager;

    [SerializeField]
    private TileMapManager mapManager;

    public Animator animator;

    private bool idle = true;

    private List<EnemyBehavior> enemies = new List<EnemyBehavior>();

    // Input queue
    private LinkedList<IEnumerator> actionQueue = new LinkedList<IEnumerator>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If an input is entered within the threshold, it will be added to the input queue.
        Vector3 pos = transform.position;

        IEnumerator newAction = null;
        if (Input.GetKeyDown(KeyCode.J))
        {
            //newAction = new KeyValuePair<Action, float>(Action.FARM, 1f);
            newAction = farm();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            newAction = move(Action.UP);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            newAction = move(Action.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            newAction = move(Action.DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            newAction = move(Action.RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            // Should go to what item is selected (7,8,9,0) and place it.
            newAction = placeFence();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (actionQueue.Count != 0) return;
            newAction = attack();
        }
        else
        {
            return;
        }

        // At most have next input queued.
        if (actionQueue.Count == 2) actionQueue.RemoveLast();
        actionQueue.AddLast(newAction);
    }

    public IEnumerator attack()
    {
        idle = false;
        UpdateAnimation(Action.ATTACK);
        List<EnemyBehavior> toRemove = new List<EnemyBehavior>();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null) enemies.RemoveAt(i);
            enemies[i].TakeDamage(PlayerStats.attackPower);
        }
        while (!Input.GetKeyUp(KeyCode.K)) yield return null;

        actionQueue.RemoveFirst();
        idle = true;

        // Axe is pulled back. If it stays pulled back for 1 second then reset.
        float maxIdle = 1f;
        while (maxIdle > 0)
        {
            if (actionQueue.Count != 0) yield break;
            maxIdle -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        UpdateAnimation(Action.IDLE);
        yield break;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collided with " + other.gameObject.name);
        if (other.gameObject.name.Contains("Enemy"))
        {
            //Debug.Log("Enemy Entered");
            enemies.Add(other.gameObject.GetComponent<EnemyBehavior>());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Enemy"))
        {
            enemies.Remove(other.gameObject.GetComponent<EnemyBehavior>());
        }
    }

    public IEnumerator farm()
    {
        idle = false;
        Vector3 pos = transform.position;
        Farm state = farmManager.getState(pos);
        if (!mapManager.isAvailable(pos))
        {
            actionQueue.RemoveFirst();
            idle = true;
            yield break;
        }
        float duration = 2f;
        //Debug.Log(state);
        switch (state)
        {
            case Farm.GRASS:
                UpdateAnimation(Action.FARM);
                while (duration > 0)
                {
                    float elapsed = PlayerStats.farmingSpeed * Time.fixedDeltaTime;
                    duration -= elapsed;
                    yield return new WaitForFixedUpdate();
                }
                farmManager.plow(pos);
                break;
            case Farm.PLOWED:
                farmManager.plant(pos);
                break;
            case Farm.DONE:
                UpdateAnimation(Action.FARM);
                while (duration > 0)
                {
                    float elapsed = PlayerStats.farmingSpeed * Time.fixedDeltaTime;
                    duration -= elapsed;
                    yield return new WaitForFixedUpdate();
                }
                farmManager.harvest(pos);
                break;
        }
        actionQueue.RemoveFirst();
        idle = true;
        yield break;
    }

    public IEnumerator placeFence()
    {
        idle = false;
        itemManager.place(transform.position);
        actionQueue.RemoveFirst();
        idle = true;
        yield break;
    }

    public IEnumerator move(Action direction)
    {
        idle = false;
        Vector3 pos = transform.position;
        float distance = 1f;
        bool jumpable = mapManager.jumpable(pos, direction);
        if (jumpable)
        {
            distance = 2f;
        }
        else if (!mapManager.onFenceValid(pos, direction))
        {
            actionQueue.RemoveFirst();
            idle = true;
            yield break;
        }
        UpdateAnimation(direction);


        float jumped = 0f;
        while (distance > 0)
        {
            float toMove = PlayerStats.movementSpeed * Time.fixedDeltaTime;
            //float toMove_y = Time.fixedDeltaTime;
            if (toMove >= distance)
            {
                toMove = distance;
            }
            distance -= toMove;
            int orderLayer = (int)(-100 * transform.position.y);

            if (jumpable)
            {
                float jumpAmount = 2 * Mathf.Cos(distance - 1) * Time.fixedDeltaTime;
                if (distance < 1.4 && distance > 1)
                {
                    orderLayer -= (direction == Action.UP) ? -50 : 0;
                    pos.y += jumpAmount;
                    jumped += jumpAmount;
                }
                else if (distance > 0.6 && distance < 1)
                {
                    orderLayer += (direction == Action.DOWN) ? 50 : 0;
                    pos.y -= jumpAmount;
                    jumped -= jumpAmount;
                }
            }
            GetComponent<Renderer>().sortingOrder = orderLayer;
            if (distance == 0) pos.y -= jumped;
            switch (direction)
            {
                case Action.RIGHT:
                    pos.x += toMove;
                    break;
                case Action.LEFT:
                    pos.x -= toMove;
                    break;
                case Action.UP:
                    pos.y += toMove;
                    break;
                case Action.DOWN:
                    pos.y -= toMove;
                    break;
            }
            transform.position = pos;
            yield return new WaitForFixedUpdate();
        }
        idle = true;
        actionQueue.RemoveFirst();
        yield break;
    }

    void FixedUpdate()
    {
        GetComponent<Renderer>().sortingOrder = (int)(-100 * transform.position.y);
        if (idle && actionQueue.Count != 0)
        {
            StartCoroutine(actionQueue.First.Value);
        }
        else if (idle) UpdateAnimation(Action.STILL);
    }


    private void UpdateAnimation(Action action)
    {
        animator.SetBool("Plow", action == Action.FARM);
        animator.SetBool("Left", action == Action.LEFT);
        animator.SetBool("Right", action == Action.RIGHT);
        animator.SetBool("Up", action == Action.UP);
        animator.SetBool("Down", action == Action.DOWN);
        animator.SetBool("Attack", action == Action.ATTACK);
        animator.SetBool("Idle", action == Action.IDLE);
        if (action == Action.FARM)
        {
            animator.speed = 1 + ((PlayerStats.farmingSpeed - 1) / 2);
        }
        else
            animator.speed = PlayerStats.movementSpeed;
    }
}