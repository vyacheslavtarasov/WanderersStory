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
    [SerializeField] private Text _actionText2; 
    [SerializeField] private Text _moneyAvailable;
    [SerializeField] private Text _nameToDisplay;

    public string LocalizationLanguage = "";
    private StringPersistentProperty _model;

    private GameSession _session;

    public override void Awake()
    {
        base.Awake();
        _session = FindObjectOfType<GameSession>();
        _session.Data.OnMoneyChangedEvent += MoneyAmountChanged;

        _model = GameSettings.I.Locale;
        _model.OnChanged += OnValueChanged;
        OnValueChanged(_model.Value, _model.Value);
    }

    private void OnValueChanged(string newValue, string oldValue)
    {
        LocalizationLanguage = newValue;
    }

    private void MoneyAmountChanged(int amount)
    {
        Redraw();
    }


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
        PerkDef itemDefinition = DefsFacade.I.Perks.Get(_chosenPerkShopPerk.Name);

        if (itemDefinition._nameAndDescription) { 

        _description.text = string.Join(" ", itemDefinition._nameAndDescription.GetLocalizedData(LocalizationLanguage).Sentences);
        _nameToDisplay.text = itemDefinition._nameAndDescription.GetLocalizedData(LocalizationLanguage).SpeakerName;
        }

        _price.text = _chosenPerkShopPerk.Price.ToString();
        _moneyAvailable.text = _session.Data.Money.ToString();

        if (_playerPerkController.GetItem(_chosenPerkShopPerk.Name).IsVoid)
        {
            _useButton.interactable = false;
            _buyButton.interactable = true;
            _actionText.enabled = false;
            _actionText2.enabled = true;
        }
        else
        {
            _useButton.interactable = true;
            _buyButton.interactable = false;
            if (_playerPerkController.GetItem(_chosenPerkShopPerk.Name).Active)
            {
                _useButton.interactable = false;
            }
            _actionText.enabled = true;
            _actionText2.enabled = false;
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
        _animator.SetTrigger("show");
        
        
        DefaultButton.GetComponent<Button>().Select();
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
            if (_playerPerkController.TryBuy(_chosenPerkShopPerk.Name, _chosenPerkShopPerk.Price) == 1)
            {
                // Debug.Log("Not enought money!");
                return;
            }
        }
        else
        {
            // _playerPerkController.ActivatePerk(_chosenPerkShopPerk.Name);
            ActivatePerk();
        }
        
        Redraw();
    }

    public void ActivatePerk()
    {

        if (_playerPerkController.GetItem(_chosenPerkShopPerk.Name).Active)
        {
            _playerPerkController.DeactivatePerk(_chosenPerkShopPerk.Name);
        }
        else
        {
            if (_playerPerkController.GetActivePerks().Count == 0)
            {
                _playerPerkController.ActivatePerk(_chosenPerkShopPerk.Name);
                Redraw();
                return;
            }

            if (!_session.Data.GetInventoryItem("PerksEquipEnhancer").IsVoid)
            {
                if (_session.Data.GetInventoryItem("PerksEquipEnhancer").Amount + 1 > _playerPerkController.GetActivePerks().Count)
                {
                    _playerPerkController.ActivatePerk(_chosenPerkShopPerk.Name);
                }
            }
        }
        
        Redraw();
    }


 
}
