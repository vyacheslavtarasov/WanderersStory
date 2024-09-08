using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMoney : MonoBehaviour
{
    private GameSession _session;
    public int AmountToAdd = 0;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        
    }

    public void Add()
    {
        _session.Data.Money += AmountToAdd;
    }
}
