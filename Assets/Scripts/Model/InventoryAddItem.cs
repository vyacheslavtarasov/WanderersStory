using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAddItem : MonoBehaviour
{
    [InventoryNameAttribute] public string Name;
    public int Amount = 1;
    public void Add(GameObject targetObject)
    {
        var inventoryComponent = targetObject?.GetComponent<Inventory>();
        if (inventoryComponent != null)
        {
            inventoryComponent.Add(Name, Amount);
        }
    }
}
