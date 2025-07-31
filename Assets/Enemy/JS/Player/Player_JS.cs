using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_JS : MonoBehaviour
{

    public Vector3 inputVec;
    [SerializeField] float moveSpeed;

    Rigidbody rb;
    SpriteRenderer spriter;

    void Awake()
    {
        spriter = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");

        if (inputVec.x != 0)
            spriter.flipX = inputVec.x > 0; // �÷��̾� �¿� ȸ��
    }
    private void FixedUpdate()
    {
        Vector3 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }
}
