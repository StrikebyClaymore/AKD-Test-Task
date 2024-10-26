using _Core.Utility;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, ISelectable, IPickable<Item>
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Outline _outline;
    public readonly UnityEvent OnPickup = new();

    public void Select()
    {
        _outline.enabled = true;
    }

    public void Deselect()
    {
        _outline.enabled = false;
    }

    public Item Pickup(Transform parent, Vector3 position)
    {
        _collider.enabled = false;
        transform.SetParent(parent);
        transform.localPosition = position;
        return this;
    }

    public void Drop(Transform parent = null)
    {
        _collider.enabled = true;
        transform.SetParent(parent);
    }
}