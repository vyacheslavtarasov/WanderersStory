using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PerkShopView : AnimatedWindow
{
    private PlayerPerkController _playerPerkController;
    // [SerializeField] private Animator _animator;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Text _description;
    [SerializeField] private Text _price;
    [SerializeField] private GameObject _perkContainer;
    [SerializeField] private Text _actionText;

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
            if (index >= count)
            {
                perkWidget.gameObject.SetActive(false);
            }
            else
            {
                perkWidget.gameObject.SetActive(true);
                perkWidget.SetData(_perkShopController.AvailablePerks.ToArray()[index], _playerPerkController.GetItem(_perkShopController.AvailablePerks.ToArray()[index].Name).Active);
                perkWidget.RemoveSubscriptions();
                perkWidget.OnChanged += SetNameOfChosenPerk;
            }
            

            index += 1;
            
        }
        Debug.Log(index);
        PerkDef itemDefinition = DefsFacade.I.Perks.Get(_chosenPerkShopPerk.Name);
        _description.text = itemDefinition.Description;
        _price.text = _chosenPerkShopPerk.Price.ToString();

        if (_playerPerkController.GetItem(_chosenPerkShopPerk.Name).IsVoid)
        {
            _useButton.interactable = false;
            _buyButton.interactable = true;
            _actionText.text = "ÑKÑÖÑÅÑyÑÑÑé";
        }
        else
        {
            _useButton.interactable = true;
            _buyButton.interactable = false;
            if (_playerPerkController.GetItem(_chosenPerkShopPerk.Name).Active)
            {
                _useButton.interactable = false;
            }
            _actionText.text = "Ñ^Ñ{ÑyÑÅÑyÑÇÑÄÑrÑpÑÑÑé";
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


    // public InputActionAsset InputActionAsset;

    // public GameObject DefaultButton;


    /*private void Awake()
    {
        InputActionAsset = Resources.Load<InputActionAsset>("HeroInputActions");
    }*/


    protected override void OnEnable()
    {
        Redraw();
        base.OnEnable();
        /*_animator.SetTrigger("show");
        foreach (InputActionMap localActionMap in InputActionAsset.actionMaps)
        {
            if (localActionMap.name == "UI")
            {
                Debug.Log("enabling UI animated window controller");
                localActionMap.Enable();
            }
            else
            {
                localActionMap.Disable();
            }
        }
        
        DefaultButton.GetComponent<Button>().Select();*/
        
    }

    public override void OnCloseAnimationComplete()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _animator.SetTrigger("hide");
    }

    public void BuyPerk()
    {
        if (_playerPerkController.GetItem(_chosenPerkShopPerk.Name).IsVoid)
        {
            _playerPerkController.Add(_chosenPerkShopPerk.Name);
        }
        else
        {
            _playerPerkController.ActivatePerk(_chosenPerkShopPerk.Name);
        }
        
        Redraw();
    }

    public void ActivatePerk()
    {
        Debug.Log("activate " + _chosenPerkShopPerk.Name);
        _playerPerkController.ActivatePerk(_chosenPerkShopPerk.Name);
        Redraw();
    }
 
}
