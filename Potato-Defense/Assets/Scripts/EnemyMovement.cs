using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 2f;
    private bool isMoving = false;
    private bool doPathfinding = false;
    private bool reachedTarget = true;
    private Vector3 movePoint, target;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PathfindNearestCrop();
        // Testing purposes
        if (Input.GetKeyDown(KeyCode.P))
        {
            doPathfinding = true;
        }
    }

    private void PathfindNearestCrop()
    {
        if (doPathfinding)
        {
            if (!isMoving)
            {
                if (reachedTarget)
                {
                    target = LocateNearestCrop();
                }

                float distanceX = System.Math.Abs(transform.position.x - target.x);
                float distanceY = System.Math.Abs(transform.position.y - target.y);

                Vector3 direction;

                if (distanceX >= distanceY)
                {
                    direction = Vector3.Scale((target - transform.position), Vector3.right).normalized;
                    anim.SetFloat("XInput", direction.x);
                    anim.SetFloat("YInput", 0);
                }
                else
                {
                    direction = Vector3.Scale((target - transform.position), Vector3.up).normalized;
                    anim.SetFloat("YInput", direction.y);
                    anim.SetFloat("XInput", 0);
                }
                movePoint = transform.position + direction;
                isMoving = true;
                reachedTarget = false;
                anim.SetBool("isMoving", true);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, movePoint, speed * Time.smoothDeltaTime);
                if (transform.position == movePoint)
                {
                    isMoving = false;
                    anim.SetBool("isMoving", false);
                }
                if (transform.position == target)
                {
                    reachedTarget = true;
                    doPathfinding = false;
                }
            }
        }
    }

    private Vector3 LocateNearestCrop()
    {
        return GameObject.Find("Player").transform.position;
    }
}
