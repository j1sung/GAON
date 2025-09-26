/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider coll;

    private void Awake()
    {
        coll = GetComponent<Collider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Area"))
            return;

        Vector3 playerDir = GameManagerJS.Instance.player.Movement;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Enemy":
                if (coll.enabled) // ���� ���� �ݶ��̴� ����
                {
                    // ���� Area��ŭ �־����� �÷��̾� �ֺ����� �� ��ġ ����
                    transform.Translate(playerDir * 8 + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f))); 

                    // ���� ������Ʈ Ǯ�� ȸ�� & ���� �ʱ�ȭ ���� ����
                }
                break;
        }
    }
}
*/