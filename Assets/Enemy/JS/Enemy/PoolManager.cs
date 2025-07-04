using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // ������� �����ϴ� ����
    public GameObject[] prefabs;

    // Ǯ ��� ����Ʈ��
    List<GameObject>[] pools;

    private void Awake()
    {
        // ��� ������Ʈ Ǯ ����Ʈ �ʱ�ȭ
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� ��Ȱ��ȭ �� ���� ������Ʈ ����
        foreach (GameObject item in pools[index])
        {
            // �߰��ϸ� select ������ �Ҵ�
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // �� ã�Ҵٸ�?
        if (!select)
        {
            // ���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
