using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(blink());
    }

    private void Awake()
    {
        StartCoroutine(blink());
    }

    private void OnDisable()
    {
        StopCoroutine(blink());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator blink()
    {
        float opacity = 360f;
        bool toggle = false;
        while (true)
        {
            opacity -= 6 * Time.fixedDeltaTime;
            opacity %= 360;
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1 - Mathf.Cos(opacity));
            yield return new WaitForFixedUpdate();
        }
    }
}
