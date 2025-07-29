using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePoint;        // 총알 발사 위치
    public float fireInterval = 1f;    // 공격 간격 (초 단위)

    private float fireTimer = 0f;
    private Camera mainCam;
    private Plane groundPlane;

    void Start()
    {
        mainCam = Camera.main;
        groundPlane = new Plane(Vector3.up, Vector3.zero); // x-z 평면
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            FireAtMouse();
            fireTimer = 0f;
        }
    }

    void FireAtMouse()
{
    Ray ray = mainCam.ScreenPointToRay(Input.mousePosition); // 메인 카메라에서 마우스 위치로 광선을 쏜다.
    if (groundPlane.Raycast(ray, out float enter))
    {
        Vector3 targetPoint = ray.GetPoint(enter);
        Vector3 direction = (targetPoint - firePoint.position).normalized;
        direction.y = 0f;

        GameObject bullet = BulletPool.Instance.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.right = direction; 
        bullet.SetActive(true);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = direction * 10f;
        rb.angularVelocity = Vector3.zero;
    }
}
}