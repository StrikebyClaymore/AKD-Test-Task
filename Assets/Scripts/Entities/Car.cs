using _Core.Utility;
using UnityEngine;

public class Car : MonoBehaviour, ISelectable
{
    [SerializeField] private Outline _outline;
    [SerializeField] private SlotsHolder _slotsHolder;
    
    public void Select()
    {
        _outline.enabled = true;
    }

    public void Deselect()
    {
        _outline.enabled = false;
    }

    public Item TryPutItem(Item item)
    {
        if(item == null)
            return null;
        if (!_slotsHolder.HasFreeSpace)
            return item;
        _slotsHolder.AddItem(item);
        return null;
    }
}