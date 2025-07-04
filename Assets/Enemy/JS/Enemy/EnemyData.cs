using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Spawn/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float spawnTime;

    public int spriteType;
    public string enemyName;
    public int health;
    public float speed;
}

[CreateAssetMenu(fileName = "BossData", menuName = "Spawn/BossData")]
public class BossData : EnemyData
{
    public int phaseCount;
    public float ultimateCooldown;
}
