using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    public RuntimeAnimatorController[] animCon;
    
    bool isLive;

    Rigidbody target;
    Rigidbody rigid;
    Animator animator;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        spriter = GetComponentInChildren<SpriteRenderer>();
    }

    // 활성화 시 초기화
    private void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(EnemyData data)
    {
        animator.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        // 적 -> 플레이어 방향 = 위치차이 정규화
        Vector3 dirVec = target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 벡터: fixedDeltaTime -> 프레임 영향을 받지 않게
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;

    }

    private void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x; // 적 좌우 변환
        
    }
}
