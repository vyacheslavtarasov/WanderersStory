using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;



public class InventoryItemPresenseCondition : MonoBehaviour
{
    [Header("Required Item")]
    [InventoryNameAttribute] public string Name = "InventoryExpander";
    public int Amount = 1;

    [Space]
    public InteractionEventWithSender ItemFoundEvent;
    public InteractionEventWithSender ItemNotFoundEvent;

    public bool CheckObjectWithName = false;

    [ShowIf("CheckObjectWithName")]
    [SerializeField] private string _gameObjectName = "";

    public void CheckItemInInventory()
    {
        GameObject initiator = GameObject.Find(_gameObjectName);
        Debug.Log(initiator);
        if (initiator != null && CheckObjectWithName)
        {
            Debug.Log("here");
            var inventoryComponent = initiator.GetComponent<Inventory>();
            Debug.Log(inventoryComponent);
            if (inventoryComponent != null)
            {
                Debug.Log("here2");
                if (inventoryComponent.Count(Name) >= Amount)
                {
                    ItemFoundEvent?.Invoke(initiator, null);
                }
                else
                {
                    ItemNotFoundEvent?.Invoke(initiator, null);
                }
            }
        }
    }

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

    public void CheckItemInInventory(GameObject initiator = null)
    {
        var inventoryComponent = initiator.GetComponent<Inventory>();
        if (inventoryComponent != null)
        {
            if (inventoryComponent.Count(Name) >= Amount)
            {
                ItemFoundEvent?.Invoke(initiator, null);
            }
            else
            {
                ItemNotFoundEvent?.Invoke(initiator, null);
            }
        }

    }

}
