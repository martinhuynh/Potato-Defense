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

    private float spawnChanceDecrease = 25f;
    private float spawnCooldownRangeLow = 7f;
    private float spawnCooldownRangeHigh = 15f;

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
            float spawnChance = 100f;
            Queue<GameObject> enemiesToSpawn = new Queue<GameObject>() { };
            int spawnSide = Random.Range(0, 4); // North, East, South, West
            while (Random.Range(0, 100) < spawnChance)
            {
                float weight = Random.Range(0, 100);
                foreach (KeyValuePair<GameObject, float> enemy in enemies)
                {
                    if (weight <= enemy.Value)
                    {
                        enemiesToSpawn.Enqueue(enemy.Key);
                        break;
                    }
                }
                spawnChance -= generateSpawnChanceDecrease();
            }
            StartCoroutine(Spawn(spawnSide, enemiesToSpawn));
            yield return new WaitForSeconds(Random.Range(spawnCooldownRangeLow, spawnCooldownRangeHigh));
        }
    }

    public IEnumerator Spawn(int spawnSide, Queue<GameObject> enemiesToSpawn)
    {
        while(enemiesToSpawn.Count != 0)
        {
            Instantiate(enemiesToSpawn.Dequeue(), generateSpawnLocation(spawnSide), transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private Vector3 generateSpawnLocation(int spawnSide)
    {
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

    private float generateSpawnChanceDecrease()
    {
        if (waveBar.slider.value < 50f)
        {
            return spawnChanceDecrease;
        }
        if (waveBar.slider.value < 75f)
        {
            return spawnChanceDecrease / 2f;
        }
        return spawnChanceDecrease / 4f;
    }

    public void setSpawnParameters(Wave wave)
    {
        spawnChanceDecrease = wave.getSpawnChanceDecrease();
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
