using UnityEngine;

public class WeaponCombiner : MonoBehaviour
{
    public PlayerAttack playerAttack;

    // 무기 조합 완료 시 호출
    public void CombineWeapon(WeaponData weaponData)
    {
        if (weaponData == null)
        {
            Debug.LogError("WeaponCombiner: weaponData is null!");
            return;
        }

        // HUD 업데이트는 Inventory에서 이미 처리 중
        playerAttack.AddSkillWeapon(weaponData);

        Debug.Log($"{weaponData.weaponName} 무기가 조합되어 스킬 슬롯에 추가됨");
    }
}