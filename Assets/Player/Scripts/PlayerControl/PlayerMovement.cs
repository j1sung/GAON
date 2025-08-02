using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 movement;
    public Vector3 Movement => movement;

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

        if (moveX != 0)
        {
            spriteRenderer.flipX = (moveX > 0);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        
        if (movement == Vector3.zero) // 멈출 때 잔여 속도 제거
        {
        rb.velocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero;
        }
    }
}