using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public ItemData itemData;
    private bool isPlayerNearby = false;

    void Update()
{
    if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
    {
        bool success = Inventory.Instance.PickupItem(itemData);
        if (success)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("습득 실패 - 조건 미충족");
        }
    }
}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = false;
    }
}