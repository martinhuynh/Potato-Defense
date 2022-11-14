using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    private Dictionary<GameObject, float> enemies;

    private Camera cam;
    private WaveProgressBarBehavior waveBar;

    private float spawnHeight, spawnWidth;
    private bool inWave = false;

    private float spawnChance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        waveBar = this.transform.parent.gameObject.GetComponentInChildren<WaveProgressBarBehavior>(true);
        spawnHeight = cam.orthographicSize;
        spawnWidth = spawnHeight * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator EnemySpawn()
    {
        while (inWave)
        {
            if (Random.Range(0, 100) < generateSpawnChance())
            {
                float weight = Random.Range(0, 100);
                foreach (KeyValuePair<GameObject, float> enemy in enemies)
                {
                    if (weight <= enemy.Value)
                    {
                        Vector3 spawnLocation = generateSpawnLocation();
                        Instantiate(enemy.Key, spawnLocation, Quaternion.identity);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(1f);
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

    private float generateSpawnChance()
    {
        if (waveBar.slider.value < 50f)
        {
            return spawnChance + waveBar.slider.value / 2;
        }
        if (waveBar.slider.value < 75f)
        {
            return (spawnChance + waveBar.slider.value / 2) * 1.5f;
        }
        return (spawnChance + waveBar.slider.value / 2) * 2f;
    }

    public void setSpawnParameters(Wave wave)
    {
        spawnChance = wave.getSpawnChance();
        enemies = wave.getEnemies();
    }

    public void startSpawn()
    {
        inWave = true;
        StartCoroutine(EnemySpawn());
    }

    public void stopSpawn()
    {
        StopCoroutine(EnemySpawn());
        inWave = false;
        GameObject[] enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemiesLeft)
        {
            enemy.GetComponent<EnemyBehavior>().Die();
        }
    }
}
