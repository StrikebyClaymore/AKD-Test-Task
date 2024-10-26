using UnityEngine;

public interface IPickable<T>
{
    T Pickup(Transform parent, Vector3 position);
    void Drop(Transform parent = null);
}