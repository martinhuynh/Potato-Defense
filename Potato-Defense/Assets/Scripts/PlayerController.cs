using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private Tilemap groundTilemap;
    private Tilemap objectsTilemap;
    private Animator animator;

    private float speed = 3f;

    private bool isWalking = false;
    private Vector3Int targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        groundTilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        objectsTilemap = GameObject.Find("Objects").GetComponent<Tilemap>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (!isWalking)
        {
            Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)GetInput());
            if (CanMove(gridPosition) && gridPosition != transform.position)
            {
                targetPosition = gridPosition;
                isWalking = true;
                animator.SetBool("isWalking", true);
            }
        } 
        else
        {
            if (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.smoothDeltaTime);
            }
            else
            {
                isWalking = false;
                animator.SetBool("isWalking", false);
            }
        }
    }

    private bool CanMove(Vector3Int newPosition)
    {
        if (!groundTilemap.HasTile(newPosition) || objectsTilemap.HasTile(newPosition)) return false;
        return true;
    }

    private Vector2 GetInput()
    {
        if (Input.GetKeyDown("w"))
        {
            animator.SetFloat("XInput", 0);
            animator.SetFloat("YInput", 1);
            return new Vector2(0, 1);
        }
        if (Input.GetKeyDown("a"))
        {
            animator.SetFloat("XInput", -1);
            animator.SetFloat("YInput", 0);
            return new Vector2(-1, 0);
        }
        if (Input.GetKeyDown("s"))
        {
            animator.SetFloat("XInput", 0);
            animator.SetFloat("YInput", -1);
            return new Vector2(0, -1);
        }
        if (Input.GetKeyDown("d"))
        {
            animator.SetFloat("XInput", 1);
            animator.SetFloat("YInput", 0);
            return new Vector2(1, 0);
        }
        return new Vector2(0, 0);
    }
}
