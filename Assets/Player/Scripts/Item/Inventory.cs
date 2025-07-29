using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public WeaponData[] allWeapons;  // 등록된 모든 무기 데이터
    private List<ItemData> collectedItems = new List<ItemData>();

    private int currentSlot = 0;
    private const int maxSlots = 4;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public bool PickupItem(ItemData itemData)
{
    int blueprintCount = 0;
    int partCount = 0;

    // 🔑 단순히 아이템 타입만 세기
    foreach (var item in collectedItems)
    {
        if (IsBlueprint(item))
            blueprintCount++;
        else if (IsPart(item))
            partCount++;
    }

    bool isBlueprint = IsBlueprint(itemData);
    bool isPart = IsPart(itemData);

    if (isBlueprint && blueprintCount >= 1)
    {
        Debug.Log("이미 설계도를 가지고 있음.");
        return false;
    }
    if (isPart && partCount >= 2)
    {
        Debug.Log("부품은 최대 2개까지만 습득 가능.");
        return false;
    }

    collectedItems.Add(itemData);
    Debug.Log(itemData.itemName + "을(를) 인벤토리에 추가");
    TryCombineWeapon();
    return true;
}

private bool IsBlueprint(ItemData item)
{
    foreach (WeaponData weapon in allWeapons)
    {
        if (item == weapon.blueprint)
            return true;
    }
    return false;
}

private bool IsPart(ItemData item)
{
    foreach (WeaponData weapon in allWeapons)
    {
        if (System.Array.Exists(weapon.parts, p => p == item))
            return true;
    }
    return false;
}

    private void TryCombineWeapon()
    {
        foreach (WeaponData weapon in allWeapons)
        {
            bool hasBlueprint = collectedItems.Contains(weapon.blueprint);
            int partCount = 0;

            foreach (ItemData part in weapon.parts)
            {
                if (collectedItems.Contains(part))
                    partCount++;
            }

            if (hasBlueprint && partCount >= weapon.parts.Length && currentSlot < maxSlots)
            {
                currentSlot++;
                HUDManager.Instance.UpdateWeaponSlot(currentSlot, weapon.weaponIcon);

                // 조합된 아이템 제거
                collectedItems.Remove(weapon.blueprint);
                foreach (ItemData part in weapon.parts)
                    collectedItems.Remove(part);

                break; // 한 번에 하나만 조합
            }
        }
    }
}