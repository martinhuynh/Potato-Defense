using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    private Camera cam;

    private float spawnHeight, spawnWidth;
    private bool inWave = true;

    private float spawnSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawn());
        cam = Camera.main;
        spawnHeight = cam.orthographicSize;
        spawnWidth = spawnHeight * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            inWave = !inWave;
            print(inWave);
            if (inWave) StartCoroutine(EnemySpawn());
        }
    }

    IEnumerator EnemySpawn()
    {
        while (inWave)
        {
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];

            Vector3 spawnLocation = generateSpawnLocation();
            Instantiate(enemy, spawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(spawnSpeed);
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
}
