using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;   // 아이템 이름
    public Sprite icon;       // HUD나 드랍 아이콘
}