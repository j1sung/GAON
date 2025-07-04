using System.Collections;
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

        Vector3 playerDir = GameManager.Instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Enemy":
                if (coll.enabled) // 죽은 적은 콜라이더 꺼짐
                {
                    // 적과 Area만큼 멀어지면 플레이어 주변으로 적 위치 스폰
                    transform.Translate(playerDir * 8 + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f))); 

                    // 추후 오브젝트 풀링 회수 & 스텟 초기화 구문 변경
                }
                break;
        }
    }
}
