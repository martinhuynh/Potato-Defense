using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoHarvested : MonoBehaviour
{
    GameObject target;
    private float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("TopRightUI");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed *= 1.1f;
        float step = speed * Time.fixedDeltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        if (transform.position == target.transform.position) Destroy(this.gameObject);
    }
}
