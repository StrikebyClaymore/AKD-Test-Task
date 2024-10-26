using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private Item _item;
    public Item Item => _item;
    [SerializeField] private Vector3 _offset;
    public bool IsEmpty => _item == null;

    public void Initialize()
    {
        if (_item)
        {
            _item.OnPickup.AddListener(RemoveItem);
            _item.transform.localPosition = _offset;
        }
    }

    public void AddItem(Item item)
    {
        _item = item;
        item.Drop(transform);
        _item.transform.localPosition = _offset; 
        _item.transform.rotation = Quaternion.identity; 
        _item.OnPickup.AddListener(RemoveItem);
    }
    
    private void RemoveItem()
    {
        _item.OnPickup.RemoveListener(RemoveItem);
        _item = null;
    }

    private void OnValidate()
    {
        _item = transform.GetComponentInChildren<Item>();
    }
}