using UnityEngine;
using System.Linq;

public class SlotsHolder : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;
    public bool HasFreeSpace => _slots.Count(x => x.IsEmpty) > 0;

    private void Awake()
    {
        foreach (var slot in _slots)
        {
            slot.Initialize();
        }
    }

    public void AddItem(Item item)
    {
        var slot = _slots.First(x => x.IsEmpty);
        slot.AddItem(item);
    }
}