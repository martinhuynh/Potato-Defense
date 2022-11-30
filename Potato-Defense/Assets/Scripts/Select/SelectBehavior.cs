using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectBehavior : MonoBehaviour
{
    [SerializeField]
    private Tilemap ground;
    [SerializeField]
    private HotbarManager hotbarManager;
    [SerializeField]
    private ItemManager itemManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = transform.position;
        Vector3Int gridPos = ground.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        gridPos.z = (int)current.z;
        transform.position = ground.GetCellCenterWorld(gridPos);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pressed();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            released();
        }
    }

    void pressed()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        if (hotbarManager.getSelected() == ItemEnum.DELETE)
        {
            itemManager.delete(transform.position);
        }
    }

    void released()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
    }
}
