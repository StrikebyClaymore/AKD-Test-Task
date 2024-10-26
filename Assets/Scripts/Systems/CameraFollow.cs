using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [field: SerializeField] public Camera Camera { get; private set; }
    [SerializeField] private Vector3 _offset;
    private Transform _target;
    private Quaternion _rotation;
    
    public void Initialize(PlayerController target)
    {
        _target = target.transform;
        Camera.transform.localPosition = _offset;
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
        transform.position = _target.position;
        transform.localRotation = _rotation;
    }
}
