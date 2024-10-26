using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [field: SerializeField] public Camera Camera { get; private set; }
    [SerializeField] private Vector3 _offset;
    private Transform _target;
    private Vector3 _rotation;
    
    public void Initialize(PlayerController target)
    {
        _target = target.transform;
        Camera.transform.localPosition = _offset;
        _rotation = Vector3.zero;
    }

    public void CustomLateUpdate()
    {
        UpdatePositionAndRotation();
    }

    public void SetRotation(Vector3 rotation)
    {
        _rotation += rotation;
    }
    
    private void UpdatePositionAndRotation()
    {
        transform.rotation = Quaternion.Euler(_rotation.x, _target.eulerAngles.y, 0);
        transform.position = _target.position;
    }
}
