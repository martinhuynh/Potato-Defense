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

    private void OnEnable()
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
        while (true)
        {
            if (!PlayerInventory.isAvailable(ItemEnum.FENCE))
            {
                GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0f);
                while (!PlayerInventory.isAvailable(ItemEnum.FENCE)) yield return new WaitForFixedUpdate();
            }
            opacity -= 6 * Time.fixedDeltaTime;
            opacity %= 360;
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, (1 - Mathf.Cos(opacity)) * 0.3f);
            yield return new WaitForFixedUpdate();
        }
    }
}
