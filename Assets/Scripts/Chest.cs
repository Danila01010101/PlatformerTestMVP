using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject _lootWindowPrefab;
    private Canvas _canvas;
    private bool _isChestOpen = false;
    private GameObject _inventory;
    [SerializeField]
    private Vector2 _lootPanelPlace = new Vector2(960,500);
    [SerializeField]
    public List<ScriptableObject> _itemsInChest = new List<ScriptableObject>
        { };
    
    private void Start()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void Interact()
    {
        if (!_isChestOpen)
        {
            _isChestOpen = true;
            OpenChest();
        }
        else
        {
            _isChestOpen = false;
            CloseChest();
        }
    }


    
    private void OpenChest()
    {
        _inventory = Instantiate(_lootWindowPrefab);
        _inventory.transform.SetParent(_canvas.transform);
        _inventory.transform.position = _lootPanelPlace;
        int _amountOfItems = _itemsInChest.Count;
        Debug.Log("Opened");
    }

    private void CloseChest()
    {
        Destroy(_inventory);
        Debug.Log("Closed");
    }
}