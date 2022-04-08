using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpComponent : MonoBehaviour
{
    [SerializeField]
    ItemScript pickUpItem;

    [Tooltip("Manual Override for drop amount, if left at -1 it will use the amount from the scriptable object")]
    [SerializeField]
    int amount = -1;

    [SerializeField] MeshRenderer propMeshRenderer;
    [SerializeField] MeshFilter porpMeshFilter;

    ItemScript itemInstance;
    // Start is called before the first frame update
    void Start()
    {
        InstatiateTime();
    }

    private void InstatiateTime()
    {
        itemInstance = Instantiate(pickUpItem);
        if(amount > 0)
        {
            itemInstance.SetAmount(amount);
        }
        ApplyMesh();
    }

    void ApplyMesh()
    {
        if (porpMeshFilter) porpMeshFilter.mesh = pickUpItem.itemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;

        if (propMeshRenderer) propMeshRenderer.material = pickUpItem.itemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        InventoryComponent playerInventory = other.GetComponent<InventoryComponent>();

        if (playerInventory)
        {
            playerInventory.AddItem(itemInstance, amount);
        }

        Destroy(gameObject);
    }
}
