using UnityEngine;

public class PlayerMovement
{
    private readonly Rigidbody _rb;
    private readonly Transform _body;
    private readonly CameraFollow _cameraFollow;
    private readonly PlayerConfig _config;
    private Vector3 _moveDirection;
    private Vector2 _rotationDirection;
    private Vector2 _rotation;

    public PlayerMovement(Rigidbody rb, Transform body, InputSystem inputSystem, CameraFollow cameraFollow, PlayerConfig config)
    {
        _rb = rb;
        _body = body;
        _cameraFollow = cameraFollow;
        _config = config;
        inputSystem.OnMoveDirectionChanged.AddListener(MoveDirectionChanged);
        inputSystem.OnRotationDirectionChanged.AddListener(RotationDirectionChanged);
        _rotation = Vector2.zero;
    }

    public void CustomFixedUpdate()
    {
        Move();
        Rotation();
    }
    
    private void Move()
    {
        if (_moveDirection == Vector3.zero)
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            return;
        }
        var moveDirection = (_body.forward * _moveDirection.z + _body.right * _moveDirection.x).normalized;
        var translation = moveDirection * (_config.MoveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + translation);
    }
    
    private void Rotation()
    {
        if (_rotationDirection == Vector2.zero)
            return;
        var moveVector = _rotationDirection * (_config.Sensitivity * Time.fixedDeltaTime);
        _rotation.x -= moveVector.y;
        _rotation.x = Mathf.Clamp(_rotation.x, _config.TopClamp, _config.BottomClamp);
        _rotation.y += moveVector.x;
        _body.localRotation = Quaternion.Euler(0, _rotation.y, 0);
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