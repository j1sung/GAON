using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
    float moveX = Input.GetAxisRaw("Horizontal");
    float moveZ = Input.GetAxisRaw("Vertical");
    movement = new Vector3(moveX, 0f, moveZ).normalized;

    Vector3 scale = transform.localScale;

    if (moveX > 0)
        scale.x = Mathf.Abs(scale.x);
    else if (moveX < 0)
        scale.x = -Mathf.Abs(scale.x);

    transform.localScale = scale;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}