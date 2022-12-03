using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

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
            opacity -= 6 * Time.fixedDeltaTime;
            opacity %= 360;
            foreach (SpriteRenderer renderer in GetComponent<CanvasRenderer>().GetComponentsInChildren<SpriteRenderer>()) {
                if (renderer.isVisible) renderer.color = new Color(255, 255, 255, (1 - Mathf.Cos(opacity)) * 0.3f);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
