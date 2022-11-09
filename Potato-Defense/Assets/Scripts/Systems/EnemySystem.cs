using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    private Camera cam;
    private WaveProgressBarBehavior waveBar;

    private float spawnHeight, spawnWidth;
    private bool inWave = true;

    private float spawnSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        waveBar = GameObject.Find("WaveProgressBar").GetComponent<WaveProgressBarBehavior>();
        spawnHeight = cam.orthographicSize;
        spawnWidth = spawnHeight * cam.aspect;
        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawn()
    {
        while (inWave)
        {
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];

            Vector3 spawnLocation = generateSpawnLocation();
            Instantiate(enemy, spawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(generateWaitTime());
        }
    }

    private Vector3 generateSpawnLocation()
    {
        int spawnSide = Random.Range(0, 4); // North, East, South, West
        if (spawnSide == 0)
        {
            return new Vector3(Mathf.Floor(Random.Range(-spawnWidth, spawnWidth)) + 0.5f, Mathf.Floor(spawnHeight + 1) + 0.5f, 0);
        }
        if (spawnSide == 1)
        {
            return new Vector3(Mathf.Floor(spawnWidth + 1) + 0.5f, Mathf.Floor(Random.Range(-spawnHeight, spawnHeight)) + 0.5f, 0);
        }
        if (spawnSide == 2)
        {
            return new Vector3(Mathf.Floor(Random.Range(-spawnWidth, spawnWidth)) + 0.5f, Mathf.Floor(-spawnHeight - 1) - 0.5f, 0);
        }
        if (spawnSide == 3)
        {
            return new Vector3(Mathf.Floor(-spawnWidth - 1) - 0.5f, Mathf.Floor(Random.Range(-spawnHeight, spawnHeight)) + 0.5f, 0);
        }
        return new Vector3(0, 0, 0);
    }

    private float generateWaitTime()
    {
        if (waveBar.slider.value < 50f)
        {
            return spawnSpeed - waveBar.slider.value / 25f;
        }
        if (waveBar.slider.value < 75f)
        {
            return spawnSpeed - waveBar.slider.value / 25f * 1.5f;
        }
        return spawnSpeed - waveBar.slider.value / 25f * 2f;
    }
}
