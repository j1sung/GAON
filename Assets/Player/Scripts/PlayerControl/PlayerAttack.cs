using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("기본 공격")]
    public Transform firePoint;        // 총알 발사 위치
    public float fireInterval = 1f;    // 기본 공격 간격
    private float fireTimer = 0f;

    [Header("스킬 무기 슬롯")]
    public List<WeaponSkillSlot> skillSlots = new List<WeaponSkillSlot>(4); // 여러 스킬 관리

    private Camera mainCam;
    private Plane groundPlane;

    void Start()
    {
        mainCam = Camera.main;
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        HandleBasicAttack();   // 기본 공격
        HandleSkillAttacks();  // 스킬 공격
    }

    // ---------------- 기본 공격 ----------------
    void HandleBasicAttack()
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
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
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

            // 플레이어와 충돌 무시
            Collider bulletCollider = bullet.GetComponent<Collider>();
            Collider playerCollider = GetComponent<Collider>();
            if (bulletCollider != null && playerCollider != null)
            {
                Physics.IgnoreCollision(bulletCollider, playerCollider);
            }
        }
    }

    // ---------------- 스킬 무기 ----------------
    void HandleSkillAttacks()
{
    foreach (var slot in skillSlots)
    {
        if (slot.weaponData == null) continue;

        slot.fireTimer += Time.deltaTime;
        if (slot.fireTimer >= slot.weaponData.attackInterval)
        {
            Debug.Log($"스킬 발사: {slot.weaponData.weaponName}");
            FireSkill(slot.weaponData);
            slot.fireTimer = 0f;
        }
    }
}

   void FireSkill(WeaponData weapon)
{
    GameObject bullet = BulletPool.Instance.GetBullet();
    bullet.transform.position = firePoint.position + firePoint.forward * 1f;
    bullet.transform.rotation = Quaternion.identity;
    bullet.SetActive(true);

    Bullet bulletScript = bullet.GetComponent<Bullet>();
    if (bulletScript != null)
        bulletScript.damage = weapon.damage;

    // ✅ 하위 SpriteRenderer 색상 변경
    Transform spriteChild = bullet.transform.Find("bulletSprite");
    if (spriteChild != null)
    {
        SpriteRenderer sr = spriteChild.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.red;
    }

    // ✅ 마우스 방향으로 발사
    Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
    Vector3 direction = firePoint.forward;

    if (groundPlane.Raycast(ray, out float enter))
    {
        Vector3 targetPoint = ray.GetPoint(enter);
        direction = (targetPoint - firePoint.position).normalized;
        direction.y = 0f;
    }

    Rigidbody rb = bullet.GetComponent<Rigidbody>();
    if (rb != null)
        rb.velocity = direction * weapon.speed;

    Collider bulletCollider = bullet.GetComponent<Collider>();
    Collider playerCollider = GetComponent<Collider>();
    if (bulletCollider != null && playerCollider != null)
        Physics.IgnoreCollision(bulletCollider, playerCollider);
}

    // 외부에서 무기 조합 완료 시 호출
    public void AddSkillWeapon(WeaponData newWeapon)
{
    Debug.Log($"스킬 무기 등록 시도: {newWeapon.weaponName}");

    for (int i = 0; i < skillSlots.Count; i++)
    {
        if (skillSlots[i].weaponData == null)
        {
            skillSlots[i].weaponData = newWeapon;
            skillSlots[i].fireTimer = 0f;
            Debug.Log($"스킬 슬롯 {i}에 {newWeapon.weaponName} 등록됨");
            return;
        }
    }
    Debug.LogWarning("빈 스킬 슬롯이 없어 무기를 추가할 수 없음");
}
}