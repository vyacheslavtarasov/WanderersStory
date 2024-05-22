using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuickInventoryController : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private InventoryItemWidget _prefab;

    private List<InventoryItemWidget> _instantiatedObjects = new List<InventoryItemWidget>();

    private GameSession _session;

    private void Start()
    {




        _session = FindObjectOfType<GameSession>();
        if (_session != null)
        {
            _session.Data.OnInventoryChanged += UpdateInventoryUI;
        }

        Rebuild();
    }

    private void UpdateInventoryUI()
    {
        Debug.Log("redrawing quick ui");
        Rebuild();
    }

    private void Rebuild()
    {
        if (_session == null)
        {
            return;
        }
        var list = _session.Data.Inventory.Where(a => a.QuickMenuIndex >= 0).OrderBy(a => a.QuickMenuIndex);

        foreach (InventoryItemWidget a in _instantiatedObjects)
        {
            if (a)
            {
                Destroy(a.gameObject);
            }
        }


        for (int i = 0; i < 3; i++)
        {
            var a = list.Where(a => a.QuickMenuIndex == i);

            var item = Instantiate(_prefab, _container);
            if (a.Count() > 0)
            {
                item.SetData(a.First());
            }
            item.gameObject.SetActive(true);
            if (i == _session.Data.QuickInventoryIndex)
            {
                item.SetSelected();
            }
            _instantiatedObjects.Add(item);
        }
    }
}
