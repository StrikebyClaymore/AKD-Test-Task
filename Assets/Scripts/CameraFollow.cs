using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    private Transform _target;
    private Quaternion _rotation;
    
    public void Initialize(PlayerController target)
    {
        _target = target.transform;
    }

    public void CustomUpdate()
    {
        UpdatePositionAndRotation();
    }
    
    public void CustomLateUpdate()
    {
        UpdatePositionAndRotation();
    }

    public void SetRotation(Quaternion rotation)
    {
        _rotation = rotation;
    }
    
    private void UpdatePositionAndRotation()
    {
        transform.position = _target.position + _offset;
        transform.localRotation = _rotation;
    }
}
