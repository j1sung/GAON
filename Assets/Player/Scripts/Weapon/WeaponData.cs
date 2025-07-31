using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("아이템 기본 정보")]
    public string weaponName;
    public Sprite weaponIcon;
    public ItemData blueprint;
    public ItemData[] parts;

    [Header("공격 설정")]
    public GameObject bulletPrefab;   // 발사할 총알
    public float attackInterval = 3f; // 자동 발사 간격
    public float damage = 100f;        // 데미지
    public float speed = 10f;         // 총알 속도
    public Color bulletColor = Color.red; // 총알 색상
}