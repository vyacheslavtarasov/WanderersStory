using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private Text _moneyAvailable;
    private GameSession _session;

    public void Awake()
    {
        _session = FindObjectOfType<GameSession>();
        _session.Data.OnMoneyChangedEvent += MoneyAmountChanged;
    }

    private void MoneyAmountChanged(int amount)
    {
        _moneyAvailable.text = _session.Data.Money.ToString();
    }

    private void Start()
    {
        MoneyAmountChanged(_session.Data.Money);
    }

    private void OnDestroy()
    {
        _session.Data.OnMoneyChangedEvent -= MoneyAmountChanged;
    }
}
