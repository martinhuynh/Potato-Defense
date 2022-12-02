using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    private EnemySystem enemySystem;
    private WinLoseSystem winLoseSystem;
    private WaveProgressBarBehavior waveProgBar;
    private PlayerStats playerStats;
    private FarmManager farmManager;

    private ArrayList waves;

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject charger;
    [SerializeField]
    private GameObject rusher;
    private GameObject readyButton;

    private int curWave = 0;
    private bool inWave = false;
    private bool gracePeriodReady = false;

    private int curTarget, curLives;

    // Start is called before the first frame update
    void Start()
    {
        enemySystem = GameObject.Find("EnemySystem").GetComponent<EnemySystem>();
        waveProgBar = GameObject.Find("WaveProgressBar").GetComponent<WaveProgressBarBehavior>();
        winLoseSystem = GameObject.Find("WinLoseSystem").GetComponent<WinLoseSystem>();
        playerStats = GameObject.Find("PlayerStatsObject").GetComponent<PlayerStats>();
        farmManager = GameObject.Find("FarmManager").GetComponent<FarmManager>();
        readyButton = GameObject.Find("ReadyButton");

        waves = new ArrayList();

        // Wave 1
        waves.Add(new Wave(6, 5, 45));
        ((Wave)waves[0]).getEnemies().Add(enemy, 100f);
        // Wave 2
        waves.Add(new Wave(10, 5, 25));
        ((Wave)waves[1]).getEnemies().Add(enemy, 100f);
        // Wave 3
        waves.Add(new Wave(15, 5, 20));
        ((Wave)waves[2]).getEnemies().Add(rusher, 20f);
        ((Wave)waves[2]).getEnemies().Add(enemy, 100f);
        // Wave 4
        waves.Add(new Wave(20, 5, 15));
        ((Wave)waves[3]).getEnemies().Add(rusher, 40f);
        ((Wave)waves[3]).getEnemies().Add(enemy, 100f);
        // Wave 5
        waves.Add(new Wave(30, 5, 100));
        ((Wave)waves[4]).getEnemies().Add(charger, 10f);
        ((Wave)waves[4]).getEnemies().Add(rusher, 40f);
        ((Wave)waves[4]).getEnemies().Add(enemy, 100f);

        curTarget = ((Wave)waves[curWave]).getTarget();
        curLives = ((Wave)waves[curWave]).getLives();
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
            if (gracePeriodReady)
            {
                StartWave();
            }
        }
        else
        {
            if (curLives <= 0)
            {
                Lose();
            }
            else if (curTarget <= 0)
            {
                WinWave();
            }
        }
    }

    private void StartWave()
    {
        inWave = true;
        gracePeriodReady = false;
        readyButton.SetActive(false);

        waveProgBar.startWaveProgBar();

        enemySystem.setSpawnParameters((Wave)waves[curWave]);
        enemySystem.startSpawn();

        farmManager.toggleGrowth(true);
    }

    private void StopWave()
    {
        inWave = false;
        readyButton.SetActive(true);
        waveProgBar.resetWaveProgBar();
        enemySystem.stopSpawn();
        curWave++;
        if (curWave == waves.Count) return;
        curTarget = ((Wave)waves[curWave]).getTarget();
        curLives = ((Wave)waves[curWave]).getLives();
        farmManager.toggleGrowth(false);
    }

    private void WinWave()
    {
        StopWave();
        playerStats.addSkillPoints(3);
        if (curWave == waves.Count) WinGame();
    }

    private void WinGame()
    {
        winLoseSystem.Win();
        Destroy(this.gameObject);
    }

    private void Lose()
    {
        StopWave();
        winLoseSystem.Lose();
        Destroy(this.gameObject);
    }

    public void decreaseLives()
    {
        curLives--;
    }

    public void decreaseTarget()
    {
        curTarget--;
    }

    public void readyUp()
    {
        gracePeriodReady = true;
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

    public int getWavesLeft()
    {
        return waves.Count - curWave;
    }

    public int getCurWave()
    {
        return curWave;
    }

    public int getTotalWaves()
    {
        return waves.Count;
    }
}
