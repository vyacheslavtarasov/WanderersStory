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
        Debug.Log("removing");
        GameObject initiator = GameObject.Find(_gameObjectName);
        Debug.Log(initiator);
        if (initiator != null && CheckObjectWithName)
        {
            Debug.Log(initiator);
            var inventoryComponent = initiator.GetComponent<Inventory>();
            if (inventoryComponent != null)
            {
                Debug.Log("rem");
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

}
