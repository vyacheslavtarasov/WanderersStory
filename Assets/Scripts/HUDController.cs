using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private GameSession _session;
    public Image fillingImage;
    void Awake()
    {
        _session = FindObjectOfType<GameSession>();
        _session.Data.OnChanged += SetHealth;
    }

    private void OnDestroy()
    {
        _session.Data.OnChanged -= SetHealth;
    }

    public void SetHealth(float newValue)
    {
        fillingImage.fillAmount = newValue / 50;
    }
}
