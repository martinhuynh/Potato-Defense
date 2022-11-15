using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    private Dictionary<GameObject, float> enemies;
    private int target, lives;
    private float spawnChance;

    public Wave(int target, int lives, float spawnChance)
    {
        this.target = target;
        this.lives = lives;
        this.spawnChance = spawnChance;
        enemies = new Dictionary<GameObject, float>();
    }

    public Dictionary<GameObject, float> getEnemies() { return enemies; }
    public int getTarget() { return target; }
    public int getLives() { return lives; }
    public float getSpawnChance() { return spawnChance; }
}
