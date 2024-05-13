using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkShopView : MonoBehaviour
{
    private PlayerPerkController _playerPerkController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Text _description;
    [SerializeField] private Text _price;
    [SerializeField] private GameObject _perkContainer;

    [SerializeField] private PerkShopPerk _chosenPerkShopPerk;

    private PerkShopController _perkShopController;

    [ContextMenu("redraw")]
    public void Redraw(List<PerkShopPerk> _perks = null)
    {
        PerkWidget[] perkWidgets = _perkContainer.GetComponentsInChildren<PerkWidget>();
        
        int index = 0;
        int count = _perkShopController.AvailablePerks.Count;
        _playerPerkController = FindObjectOfType<PlayerPerkController>();
        foreach (PerkWidget perkWidget in perkWidgets)
        {
            perkWidget.SetData(_perkShopController.AvailablePerks.ToArray()[index], _playerPerkController.GetItem(_perkShopController.AvailablePerks.ToArray()[index].Name).Active);
            perkWidget.RemoveSubscriptions();
            perkWidget.OnChanged += SetNameOfChosenPerk;

            index += 1;
            if (index >= count) break;
        }
        PerkDef itemDefinition = DefsFacade.I.Perks.Get(_chosenPerkShopPerk.Name);
        _description.text = itemDefinition.Description;
        _price.text = _chosenPerkShopPerk.Price.ToString();

        if (_playerPerkController.GetItem(_chosenPerkShopPerk.Name).IsVoid)
        {
            _useButton.interactable = false;
            _buyButton.interactable = true;
        }
        else
        {
            _useButton.interactable = true;
            _buyButton.interactable = false;
            if (_playerPerkController.GetItem(_chosenPerkShopPerk.Name).Active)
            {
                _useButton.interactable = false;
            }
        }

        

    }

    public void SetNameOfChosenPerk(PerkShopPerk _perk)
    {
        _chosenPerkShopPerk = _perk;
        Redraw();
    }

    public void SetController(PerkShopController controller)
    {
        _perkShopController = controller;
    }

    private void OnEnable()
    {
        _animator.SetTrigger("show");
        Redraw();
    }

    private void OnDisable()
    {
        _animator.SetTrigger("hide");
    }

    public void BuyPerk()
    {
        _playerPerkController.Add(_chosenPerkShopPerk.Name);
        Redraw();
    }

    public void ActivatePerk()
    {
        Debug.Log("activate " + _chosenPerkShopPerk.Name);
        _playerPerkController.ActivatePerk(_chosenPerkShopPerk.Name);
        Redraw();
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
