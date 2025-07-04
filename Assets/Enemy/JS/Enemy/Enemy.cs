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

    // Ȱ��ȭ �� �ʱ�ȭ
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

        // �� -> �÷��̾� ���� = ��ġ���� ����ȭ
        Vector3 dirVec = target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // ���� ����: fixedDeltaTime -> ������ ������ ���� �ʰ�
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;

    }

    private void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x; // �� �¿� ��ȯ
        
    }
}
