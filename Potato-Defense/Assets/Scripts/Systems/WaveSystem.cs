using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    private EnemySystem enemySystem;
    private WaveProgressBarBehavior waveProgBar;

    private ArrayList waves;

    [SerializeField]
    private GameObject enemy;

    private int curWave = -1;
    private bool inWave = false;
    private float gracePeriod = 1f, gracePeriodEnd;

    private int curTarget, curLives;

    // Start is called before the first frame update
    void Start()
    {
        enemySystem = GameObject.Find("EnemySystem").GetComponent<EnemySystem>();
        waveProgBar = GameObject.Find("WaveProgressBar").GetComponent<WaveProgressBarBehavior>();

        waves = new ArrayList();
        gracePeriodEnd = gracePeriod;
        
        // Wave 1
        waves.Add(new Wave(10, 5, 7));
        ((Wave) waves[0]).getEnemies().Add(enemy, 100f);
        // Wave 2
        waves.Add(new Wave(15, 5, 15));
        ((Wave)waves[0]).getEnemies().Add(enemy, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        //Test
        if (Input.GetKeyDown(KeyCode.T)) curTarget--;
    }

    private void FixedUpdate()
    {
        if (!inWave)
        {
            if (Time.time >= gracePeriodEnd)
            {
                StartWave();
            }
        } else
        {
            if (curLives <= 0)
            {
                Lose();
            } else if (curTarget <= 0)
            {
                WinWave();
            }
        }
    }

    private void StartWave()
    {
        curWave++;
        inWave = true;

        curTarget = ((Wave)waves[curWave]).getTarget();
        curLives = ((Wave)waves[curWave]).getLives();
        waveProgBar.startWaveProgBar();
        
        enemySystem.setSpawnParameters((Wave)waves[curWave]);
        enemySystem.startSpawn();
    }

    private void StopWave()
    {
        inWave = false;
        gracePeriodEnd = gracePeriod + Time.time;
        waveProgBar.resetWaveProgBar();
        enemySystem.stopSpawn();
    }

    private void WinWave()
    {
        StopWave();
        if (curWave == waves.Count - 1) WinGame();
    }

    private void WinGame()
    {
        print("WIN!");
    }

    private void Lose()
    {
        StopWave();
    }

    public void decreaseLives()
    {
        curLives--;
    }

    public void decreaseTarget()
    {
        curTarget--;
    }

    // Getters

    public int getLives()
    {
        return curLives;
    }

    public int getTarget()
    {
        return curTarget;
    }

    public bool isInWave()
    {
        return inWave;
    }
}
