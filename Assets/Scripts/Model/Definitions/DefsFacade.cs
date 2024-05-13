using UnityEngine;


[CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
public class DefsFacade : ScriptableObject
{
    [SerializeField] private InventoryItemsDef _items;
    [SerializeField] private PerkRepositoryDef _perks;

    public PerkRepositoryDef Perks => _perks;

    public InventoryItemsDef Items => _items;

    private static DefsFacade _instance;
    public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

    private static DefsFacade LoadDefs()
    {
        return _instance = Resources.Load<DefsFacade>("DefsFacade");
    }
}
