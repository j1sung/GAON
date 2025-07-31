using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public WeaponData[] allWeapons;  // 등록된 모든 무기 데이터
    public PlayerAttack playerAttack; // 🔑 PlayerAttack 참조 추가

    private List<ItemData> collectedItems = new List<ItemData>();
    private enum ItemCategory { None, Blueprint, Part }

    private int currentSlot = 0;
    private const int maxSlots = 4;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private ItemCategory GetItemCategory(ItemData item)
    {
        foreach (WeaponData weapon in allWeapons)
        {
            if (item == weapon.blueprint)
                return ItemCategory.Blueprint;
            if (weapon.parts != null && System.Array.Exists(weapon.parts, p => p == item))
                return ItemCategory.Part;
        }
        return ItemCategory.None;
    }

    public bool PickupItem(ItemData itemData)
    {
        int blueprintCount = 0;
        int partCount = 0;

        foreach (var item in collectedItems)
        {
            var category = GetItemCategory(item);
            if (category == ItemCategory.Blueprint)
                blueprintCount++;
            else if (category == ItemCategory.Part)
                partCount++;
        }

        var acquiredItem = GetItemCategory(itemData);

        if (acquiredItem == ItemCategory.Blueprint && blueprintCount >= 1)
        {
            Debug.Log("이미 설계도를 가지고 있음.");
            return false;
        }
        if (acquiredItem == ItemCategory.Part && partCount >= 2)
        {
            Debug.Log("부품은 최대 2개까지만 습득 가능.");
            return false;
        }

        collectedItems.Add(itemData);
        Debug.Log(itemData.itemName + "를 인벤토리에 추가");
        TryCombineWeapon();
        return true;
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

                // 🔑 HUD 아이콘 업데이트
                HUDManager.Instance.UpdateWeaponSlot(currentSlot, weapon.weaponIcon);

                // 🔑 PlayerAttack에 무기 스킬 추가
                if (playerAttack != null)
                {
                    playerAttack.AddSkillWeapon(weapon);
                    Debug.Log($"{weapon.weaponName} 무기가 스킬 슬롯에 추가됨");
                }
                else
                {
                    Debug.LogError("Inventory: PlayerAttack 참조가 설정되지 않음!");
                }

                // 조합된 아이템 제거
                collectedItems.Remove(weapon.blueprint);
                foreach (ItemData part in weapon.parts)
                    collectedItems.Remove(part);

                break; // 한 번에 하나만 조합
            }
        }
    }

    public bool HasWeapon() 
    {
        return currentSlot > 0;  
    }
}