using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private FarmManager farmManager;
    private ItemManager itemManager;

    private Animator anim;

    private float speed = 1f;
    private int attackPower = 5;
    private bool isMoving = false, isAttacking = false;
    private bool onCrop = false;
    private Vector3 movePoint, target;

    private CropBehavior crop;
    private FenceBehavior targetFence;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        farmManager = GameObject.Find("FarmManager").GetComponent<FarmManager>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
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
        if (isAttacking)
        {
            AttackFence();
            return;
        }
        if (!isMoving)
        {
            target = LocateNearestCrop();
            if (target == transform.position)
            {
                if (farmManager.getCrops().ContainsKey(Vector3Int.FloorToInt(target)))
                {
                    if (!(crop.getState() == Farm.GROWING || crop.getState() == Farm.DONE)) return;
                } 
                else
                {
                    return;
                }
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
            anim.SetBool("isMoving", true);
            if (!itemManager.isAvailable(Vector3Int.FloorToInt(movePoint)))
            {
                targetFence = itemManager.getFence(Vector3Int.FloorToInt(movePoint));
                isAttacking = true;
                initialAttackPos = transform.position;
                AttackPeak = initialAttackPos + (movePoint - transform.position) / 2;
                nextAttack = Time.time + 2f;
                return;
            }
            isMoving = true;
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
                nextJump = Time.time + 2f;
            }
        }
    }

    private Vector3 LocateNearestCrop()
    {
        float closestDistance = float.MaxValue;
        CropBehavior closestCrop = null;
        if (farmManager.getCrops().Count == 0) return transform.position;
        foreach (KeyValuePair<Vector3Int, CropBehavior> crop in farmManager.getCrops())
        {
            if (crop.Value.getState() == Farm.GROWING || crop.Value.getState() == Farm.DONE)
            {
                if ((transform.position - crop.Key).magnitude < closestDistance)
                {
                    closestDistance = (transform.position - crop.Key).magnitude;
                    closestCrop = crop.Value;
                }
            }
        }
        if (!closestCrop)
        {
            anim.SetBool("isMoving", false);
            return transform.position;
        }
        crop = closestCrop;
        Vector3 pos = closestCrop.getPosition();
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f;
        return pos;
    }

    private Vector3 jumpPeak, initialPos;
    private bool reachedPeak = false;
    private float nextJump;
    private void AttackCrop()
    {
        if (!reachedPeak)
        {
            transform.position = Vector3.MoveTowards(transform.position, jumpPeak, 1f * Time.smoothDeltaTime);
            if (transform.position == jumpPeak) reachedPeak = true;
        } 
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, 2f * Time.smoothDeltaTime);
            if (transform.position == initialPos)
            {
                if (Time.time >= nextJump)
                {
                    if (crop.getState() == Farm.GROWING || crop.getState() == Farm.DONE)
                    {
                        if (!crop.decrease(attackPower)) onCrop = false;
                    } 
                    else
                    {
                        onCrop = false;
                    }
                    nextJump = Time.time + 2f;
                    reachedPeak = false;
                }
            }
        }
    }

    private Vector3 AttackPeak, initialAttackPos;
    private bool reachedAttackPeak = false, fenceDestroyed = false;
    private float nextAttack;
    private void AttackFence()
    {
        if (!reachedAttackPeak)
        {
            transform.position = Vector3.MoveTowards(transform.position, AttackPeak, 2f * Time.smoothDeltaTime);
            if (transform.position == AttackPeak) {
                if (itemManager.containsFence(targetFence))
                {
                    if (!targetFence.decrease(attackPower / 2))
                    {
                        fenceDestroyed = true;
                    }
                }
                else
                {
                    fenceDestroyed = true;
                }
                reachedAttackPeak = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialAttackPos, 1f * Time.smoothDeltaTime);
            if (transform.position == initialAttackPos)
            {
                if (fenceDestroyed || !itemManager.containsFence(targetFence))
                {
                    isAttacking = false;
                    fenceDestroyed = false;
                    return;
                }
                if (Time.time >= nextAttack)
                {
                    nextAttack = Time.time + 2f;
                    reachedAttackPeak = false;
                }
            }
        }
    }
}
