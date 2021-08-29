using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory:MonoBehaviour
{
    private List<Item> _itemList;
    public GameObject _inventoryPrefab;
    public GameObject _inventory;
    private Canvas _canvas;
    [SerializeField]
    public Vector2 _inventoryPlace;
    public bool _isInventoryOpen = false;
    private void Start()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    public Inventory()
    {
        _itemList = new List<Item>();
    }
    public void AddItem(Item _item)
    {
        _itemList.Add(_item);
    }

    public void InteractWithInventory()
    {
        if (!_isInventoryOpen)
        {
            _isInventoryOpen = true;
            OpenUIElemventOnCanvas(_inventoryPrefab, _inventory, _inventoryPlace);
        }
        else
        {
            _isInventoryOpen = false;
            CloseUIObject(_inventory);
        }
    }
    public void OpenUIElemventOnCanvas(GameObject Prefab, GameObject element, Vector2 place)
    {
        element = Instantiate(Prefab);
        element.transform.SetParent(_canvas.transform);
        element.transform.position = _inventoryPlace;
    }

    public void CloseUIObject(GameObject _object)
    {
        Destroy(_object);
    }
}
