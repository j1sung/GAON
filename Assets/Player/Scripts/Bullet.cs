using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    private float timer = 0f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            gameObject.SetActive(false); // 파괴 대신 비활성화
        }
    }

    public void Fire(Vector3 direction)
    {
        direction.y = 0f;
        direction.Normalize();
        rb.velocity = direction * speed;
    }
}