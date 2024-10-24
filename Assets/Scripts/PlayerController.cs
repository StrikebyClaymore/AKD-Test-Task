using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _body;
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _sensitivity = 200;
    [SerializeField] private float _topClamp = -90f;
    [SerializeField] private float _bottomClamp = 90f;
    [SerializeField] private Vector2 _rotation;
    [SerializeField] private Transform _hands;
    private InputSystem _inputSystem;
    private CameraFollow _cameraFollow;
    private Camera _camera;
    private Vector3 _moveDirection;
    private Vector2 _rotationDirection;
    [SerializeField] private float _raycastInterval = 0.2f;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _raycastLayer;
    private float _raycastTimer = 0f;
    private Item _itemInHands;
    private ISelectable _selectedObject;

    public void Initialize(InputSystem inputSystem, CameraFollow cameraFollow)
    {
        _inputSystem = inputSystem;
        _cameraFollow = cameraFollow;
        _camera = cameraFollow.GetComponent<Camera>();
        _inputSystem.OnMoveDirectionChanged.AddListener(MoveDirectionChanged);
        _inputSystem.OnRotationDirectionChanged.AddListener(RotationDirectionChanged);
        _inputSystem.OnMouseLeftClick.AddListener(LeftClickAction);
        _rotation = Vector2.zero;
    }

    public void CustomUpdate()
    {
        RaycastDetection();
    }

    public void CustomFixedUpdate()
    {
        Move();
        Rotation();
    }

    private void RaycastDetection()
    {
        _raycastTimer += Time.deltaTime;
        if (_raycastTimer >= _raycastInterval)
        {
            _raycastTimer = 0f;
            Vector3 rayOrigin = _camera.transform.position;
            Vector3 rayDirection = _camera.transform.forward;
            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, _raycastDistance, _raycastLayer))
            {
                SelectObject(hit.collider.gameObject);
            }
            else
            {
                DeselectObject();
            }
        }
    }

    private void SelectObject(GameObject obj)
    {
        if (obj.TryGetComponent<ISelectable>(out var selectable))
        {
            DeselectObject();
            _selectedObject = selectable;
            _selectedObject.Select();
        }
    }

    private void DeselectObject()
    {
        if (_selectedObject != null)
            _selectedObject.Deselect();
    }
    
    private void LeftClickAction()
    {
        switch (_selectedObject)
        {
            case Car car:
                _itemInHands = car.TryPutItem(_itemInHands);
                break;
            case Item item:
                PickupItem(item);
                break;
        }
    }
    
    private void PickupItem(Item item)
    {
        _itemInHands = item.Pickup(_hands, _hands.localPosition);
    }

    private void Move()
    {
        if (_moveDirection == Vector3.zero)
            return;
        var moveDirection = (_body.forward * _moveDirection.z + _body.right * _moveDirection.x).normalized;
        var translation = moveDirection * (_moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + translation);
    }
    
    private void Rotation()
    {
        if (_rotationDirection == Vector2.zero)
            return;
        var moveVector = _rotationDirection * (_sensitivity * Time.fixedDeltaTime);
        _rotation.x -= moveVector.y;
        _rotation.x = Mathf.Clamp(_rotation.x, _topClamp, _bottomClamp);
        _rotation.y += moveVector.x;
        _body.localRotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);
        _cameraFollow.SetRotation(Quaternion.Euler(_rotation.x, _rotation.y, 0));
    }
    
    private void MoveDirectionChanged(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            _moveDirection = Vector3.zero;
            return;
        }
        _moveDirection = new Vector3(direction.x, 0, direction.y).normalized;
    }
    
    private void RotationDirectionChanged(Vector2 direction)
    {
        _rotationDirection = new Vector2(direction.x, direction.y);
    }
}
