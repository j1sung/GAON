using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public EnemyData[] enemyData; // ������ �����͸� ������

    float Time;
    [SerializeField] float spawnTime; // �����Ǵ� �ð�

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        // ���� ��Ÿ���� ���� ������
        Time += UnityEngine.Time.deltaTime;
        if(Time > enemyData[0].spawnTime) // ������ �ٸ� ���� Ÿ�� ���� ����
        {
            Time = 0f;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.Instance.pool.Get(0); // �ϴ��� �������� enemy�� ������Ŵ index=0 : Stage Enemy Prefab / index=1 : Boss Prefab
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // ���� ��ȯ ��ġ ���� ����Ʈ ����... Range 0�� Spawner Ŭ���� ��ġ�� ��
        enemy.GetComponent<Enemy>().Init(enemyData[0]);
    }
}
