using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryItemPresenseCondition : MonoBehaviour
{
    [Header("Required Item")]
    [InventoryNameAttribute] public string Name;
    public int Amount = 1;

    [Space]
    public InteractionEventWithSender ItemFoundEvent;
    public InteractionEventWithSender ItemNotFoundEvent;


    public void CheckItemInInventory(GameObject initiator = null, GameObject source = null)
    {
        var inventoryComponent = initiator.GetComponent<Inventory>();
        if (inventoryComponent != null)
        {
            if (inventoryComponent.Count(Name) >= Amount)
            {
                ItemFoundEvent?.Invoke(initiator, source);
            }
            else
            {
                ItemNotFoundEvent?.Invoke(initiator, source);
            }
        }
        
    }

}
