using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;
using System;



public class RemoveInventoryItems : MonoBehaviour
{
    [Header("Required Item")]
    [InventoryNameAttribute] public string Name = "InventoryExpander";
    public int Amount = 1;

    [Space]
    public InteractionEventWithSender Done;

    public bool CheckObjectWithName = false;

    [ShowIf("CheckObjectWithName")]
    [SerializeField] private string _gameObjectName = "";

    public void Remove()
    {
        GameObject initiator = GameObject.Find(_gameObjectName);
        if (initiator != null && CheckObjectWithName)
        {
            var inventoryComponent = initiator.GetComponent<Inventory>();
            if (inventoryComponent != null)
            {
                inventoryComponent.Remove(Name.ToString(), Amount);
                Done?.Invoke(initiator, null);
            }
        }
    }

    public void Remove(GameObject initiator = null, GameObject source = null)
    {
        var inventoryComponent = initiator.GetComponent<Inventory>();
        if (inventoryComponent != null)
        {
            if (inventoryComponent != null)
            {
                inventoryComponent.Remove(Name.ToString(), Amount);
                Done?.Invoke(initiator, null);
            }
        }

    }

    public void Remove(GameObject initiator = null)
    {
        var inventoryComponent = initiator.GetComponent<Inventory>();
        if (inventoryComponent != null)
        {
            if (inventoryComponent != null)
            {
                inventoryComponent.Remove(Name.ToString(), Amount);
                Done?.Invoke(initiator, null);
            }
        }

    }

}
