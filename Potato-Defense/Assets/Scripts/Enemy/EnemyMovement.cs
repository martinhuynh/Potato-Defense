using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private FarmManager farmManager;

    private Animator anim;

    private float speed = 2f;
    private bool isMoving = false;
    private bool onCrop = false;
    private Vector3 movePoint, target;
    private CropBehavior crop;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        farmManager = GameObject.Find("FarmManager").GetComponent<FarmManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onCrop)
        {
            PathfindNearestCrop();
        } else
        {
            AttackCrop();
        }
    }

    private void PathfindNearestCrop()
    {
        if (!isMoving)
        {
            target = LocateNearestCrop();
            if (target == transform.position) return;

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
            anim.SetBool("isMoving", true);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint, speed * Time.smoothDeltaTime);
            if (transform.position == movePoint)
            {
                isMoving = false;
            }
            if (transform.position == target)
            {
                anim.SetBool("isMoving", false);
                onCrop = true;
                initialPos = transform.position;
                jumpPeak = initialPos + new Vector3(0, 0.4f);
            }
        }
    }

    private Vector3 LocateNearestCrop()
    {
        float closestDistance = float.MaxValue;
        CropBehavior closestCrop = null;
        foreach (KeyValuePair<Vector3Int, CropBehavior> crop in farmManager.getCrops())
        {
            if ((transform.position - crop.Key).magnitude < closestDistance)
            {
                closestDistance = (transform.position - crop.Key).magnitude;
                closestCrop = crop.Value;
            }
        }
        if (!closestCrop) return transform.position;
        crop = closestCrop;
        Vector3 pos = closestCrop.getPosition();
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f;
        return pos;
    }

    private Vector3 jumpPeak, initialPos;
    private bool reachedPeak = false;
    private void AttackCrop()
    {
        if (!reachedPeak)
        {
            transform.position = Vector3.MoveTowards(transform.position, jumpPeak, 1f * Time.smoothDeltaTime);
            if (transform.position == jumpPeak) reachedPeak = true;
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, 2f * Time.smoothDeltaTime);
            if (transform.position == initialPos)
            {
                if (farmManager.getCrops().ContainsKey(Vector3Int.FloorToInt(initialPos)))
                {
                    if (!crop.decrease(1)) onCrop = false;
                    
                } else
                {
                    onCrop = false;
                }
                reachedPeak = false;
            }
        }
    }
}
