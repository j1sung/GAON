using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public EnemyData[] enemyData; // 적들의 데이터를 가져옴

    float Time;
    [SerializeField] float spawnTime; // 스폰되는 시간

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        // 스폰 쿨타임이 돌면 스폰함
        Time += UnityEngine.Time.deltaTime;
        if(Time > enemyData[0].spawnTime) // 적마다 다른 스폰 타임 설정 가능
        {
            Time = 0f;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.Instance.pool.Get(0); // 일단은 스테이지 enemy만 스폰시킴 index=0 : Stage Enemy Prefab / index=1 : Boss Prefab
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // 적의 소환 위치 랜덤 포인트 지정... Range 0은 Spawner 클래스 위치라 뺌
        enemy.GetComponent<Enemy>().Init(enemyData[0]);
    }
}
