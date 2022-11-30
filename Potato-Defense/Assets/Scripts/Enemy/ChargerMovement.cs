using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class ChargerMovement : MonoBehaviour
{
    private FarmManager farmManager;
    private Camera cam;
    private Tilemap map;
    private GameObject skull, skullArrow;
    private Animator anim;

    [SerializeField]
    private Tile warning;

    private float spawnHeight, spawnWidth;
    private float warningTime = 3f;
    private float speed = 3f;
    private bool isMoving = false;
    private Vector3 direction;
    private bool rebound = false;

    // Start is called before the first frame update
    void Start()
    {
        farmManager = GameObject.Find("FarmManager").GetComponent<FarmManager>();
        map = GameObject.Find("ChargerWarning").GetComponent<Tilemap>();
        cam = Camera.main;
        skull = gameObject.transform.Find("Skull").gameObject;
        skullArrow = gameObject.transform.Find("SkullArrow").gameObject;
        anim = gameObject.GetComponent<Animator>();
        spawnHeight = cam.orthographicSize;
        spawnWidth = spawnHeight * cam.aspect;

        setTargetAndMoveToTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Move();
        }
    }

    public IEnumerator doWarning(List<Vector3Int> warningPositions)
    {
        for (int i = 0; i < warningTime; i++)
        {
            foreach(Vector3Int pos in warningPositions) {
                map.SetTile(pos, warning);
            }
            yield return new WaitForSeconds(0.75f);
            foreach (Vector3Int pos in warningPositions)
            {
                map.SetTile(pos, null);
            }
            yield return new WaitForSeconds(0.25f);
        }
        skull.SetActive(false);
        skullArrow.SetActive(false);
        isMoving = true;
        anim.SetBool("isMoving", true);
    }

    private void setTargetAndMoveToTarget()
    {
        int cropIndex = Random.Range(0, farmManager.getCrops().Count);
        Vector3Int targetCropPos = farmManager.getCrops().ElementAt(cropIndex).Key;
        List<Vector3Int> warningPositions = new List<Vector3Int>();

        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: // North
                transform.position = new Vector3(targetCropPos.x + 0.5f, spawnHeight + 0.5f);
                skull.transform.position = new Vector3(transform.position.x, transform.position.y - 1.4f);
                skullArrow.transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f);
                skullArrow.transform.eulerAngles = new Vector3(0, 0, 180);
                anim.SetFloat("YInput", -1);
                for (float i = -spawnHeight; i < spawnHeight; i++)
                {
                    warningPositions.Add(map.WorldToCell(new Vector3(targetCropPos.x, i)));
                }
                direction = new Vector3(0, -1);
                break;
            case 1: // East
                transform.position = new Vector3(spawnWidth + 0.5f, targetCropPos.y + 0.5f);
                skull.transform.position = new Vector3(transform.position.x - 1.4f, transform.position.y);
                skullArrow.transform.position = new Vector3(transform.position.x - 0.8f, transform.position.y);
                skullArrow.transform.eulerAngles = new Vector3(0, 0, 90);
                anim.SetFloat("XInput", -1);
                for (float i = -spawnWidth; i < spawnWidth; i++)
                {
                    warningPositions.Add(map.WorldToCell(new Vector3(i, targetCropPos.y)));
                }
                direction = new Vector3(-1, 0);
                break;
            case 2: // South
                transform.position = new Vector3(targetCropPos.x + 0.5f, -spawnHeight - 0.5f);
                skull.transform.position = new Vector3(transform.position.x, transform.position.y + 1.4f);
                skullArrow.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f);
                anim.SetFloat("YInput", 1);
                for (float i = -spawnHeight; i < spawnHeight; i++)
                {
                    warningPositions.Add(map.WorldToCell(new Vector3(targetCropPos.x, i)));
                }
                direction = new Vector3(0, 1);
                break;
            case 3: // West
                transform.position = new Vector3(-spawnWidth - 0.5f, targetCropPos.y + 0.5f);
                skull.transform.position = new Vector3(transform.position.x + 1.4f, transform.position.y);
                skullArrow.transform.position = new Vector3(transform.position.x + 0.8f, transform.position.y);
                skullArrow.transform.eulerAngles = new Vector3(0, 0, -90);
                anim.SetFloat("XInput", 1);
                for (float i = -spawnWidth; i < spawnWidth; i++)
                {
                    warningPositions.Add(map.WorldToCell(new Vector3(i, targetCropPos.y)));
                }
                direction = new Vector3(1, 0);
                break;
        }
        StartCoroutine(doWarning(warningPositions));
    }

    private void Move()
    {
        if (rebound) return;
        CropBehavior crop = farmManager.getCropAt(Vector3Int.FloorToInt(transform.position));
        if (crop)
        {
            if (crop.getState() == Farm.GROWING || crop.getState() == Farm.DONE) 
                crop.decrease(100);
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.smoothDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fence")
        {
            collision.gameObject.GetComponent<FenceBehavior>().decrease(100);
            rebound = true;
            StartCoroutine(Rebound());
        }
    }

    private IEnumerator Rebound()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3Int.FloorToInt(transform.position - direction) + new Vector3(0.5f, 0.5f) , speed * Time.smoothDeltaTime);
        yield return new WaitForSeconds(2f);
        rebound = false;
    }
}
