using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;     // 완성 무기 이름
    public Sprite weaponIcon;     // 완성 무기 아이콘
    public ItemData blueprint;    // 필요한 설계도
    public ItemData[] parts;      // 필요한 부품들 (2개 이상 가능)
}