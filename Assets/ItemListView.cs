using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ItemListView : AnimatedWindow
{
    private PlayerPerkController _playerPerkController;
    // [SerializeField] private Animator _animator;

    [SerializeField] private Text _description;
    [SerializeField] private Text _nameToDisplay;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private GameObject _itemsContainer;

    public string LocalizationLanguage = "";
    private StringPersistentProperty _model;



    private GameSession _session;

    List<GameObject> instances = new List<GameObject>();

    public override void Awake()
    {
        base.Awake();
        _session = FindObjectOfType<GameSession>();
        // _session.Data.OnInventoryChanged += InventoryChanged;

        _model = GameSettings.I.Locale;
        _model.OnChanged += OnValueChanged;
        OnValueChanged(_model.Value, _model.Value);
    }

    private void OnValueChanged(string newValue, string oldValue)
    {
        LocalizationLanguage = newValue;
        Debug.Log(LocalizationLanguage);
    }

    /*private void InventoryChanged()
    {
        Redraw();
    }*/


    [SerializeField] private string _chosenItemName;

    public void SelectItem(string name)
    {
        _chosenItemName = name;

        ItemDef itemDefinition = DefsFacade.I.Items.Get(name);

        // _description.text = itemDefinition._description.Data.Sentences[0];
        // _nameToDisplay.text = itemDefinition._name2Display.Data.Sentences[0];

        _nameToDisplay.text = itemDefinition._name2Display.GetLocalizedData(LocalizationLanguage).Sentences[0];
        _description.text = itemDefinition._description.GetLocalizedData(LocalizationLanguage).Sentences[0];
        _icon.sprite = itemDefinition.Icon;



        // Redraw(name);
        Debug.Log(name);
        /*if (instances.Count > 0)
        {
            DefaultButton = instances[^1];
            Debug.Log("selection default button0");
            Debug.Log(instances.Count);
            DefaultButton.GetComponent<Button>().Select();

        }*/

    }

    // private PerkShopController _perkShopController;

    [ContextMenu("redraw")]
    public void Redraw(string _selectedName = "")
    {

        List<InventoryItemData> items = _session.Data.Inventory;
        foreach (GameObject obj in instances)
        {
            DestroyImmediate(obj);
        }
        instances.Clear();

        foreach (InventoryItemData item in items)
        {
            string itemName = item.Name;
            ItemDef itemDefinition = DefsFacade.I.Items.Get(itemName);
            // Debug.Log(itemName);

            GameObject instance = Instantiate(_itemPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            instance.transform.parent = _itemsContainer.transform;

            instance.GetComponent<ItemWidget>().SetData(item, LocalizationLanguage);

            instance.GetComponent<ItemWidget>().OnSelected += SelectItem;

            instances.Add(instance);

            if (item.Name == _selectedName)
            {
                DefaultButton = instance;
            }


        }

        if (instances.Count > 0 || name == "")
        {
            DefaultButton = instances[^1];
            Debug.Log("selection default button1");
            Debug.Log(DefaultButton.name);
            // DefaultButton.GetComponent<Button>().Select();

        }


        if (DefaultButton != null)
        {

                DefaultButton.GetComponent<Button>().Select();

            
        }
        


        /*PerkWidget[] perkWidgets = _perkContainer.GetComponentsInChildren<PerkWidget>();

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

        if (itemDefinition._nameAndDescription)
        {

            _description.text = string.Join(" ", itemDefinition._nameAndDescription.GetLocalizedData(LocalizationLanguage).Sentences);
            _nameToDisplay.text = itemDefinition._nameAndDescription.GetLocalizedData(LocalizationLanguage).SpeakerName;
        }


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
        }*/
    }


    /*public void SetController(PerkShopController controller)
    {
        _perkShopController = controller;
    }*/




    protected override void OnEnable()
    {
        Redraw();

        // FindObjectOfType<DialogBoxController>(true).gameObject.SetActive(false);
        FindObjectOfType<DialogBoxController>(true)._nextButton.enabled = false;
        
        base.OnEnable();
        // _animator.SetTrigger("show");
        /*if (instances.Count > 0)
        {
            DefaultButton = instances[^1];
            Debug.Log("selection default button1");
            Debug.Log(DefaultButton.name);
            DefaultButton.GetComponent<Button>().Select();




        }*/

        // DefaultButton.GetComponent<Button>().Select(); // set first button in a list if it is
    }

    public override void OnCloseAnimationComplete()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        // FindObjectOfType<DialogBoxController>(true).gameObject.SetActive(true);
        FindObjectOfType<DialogBoxController>(true)._nextButton.enabled = true;
        _animator.SetTrigger("hide");
    }

    
}
