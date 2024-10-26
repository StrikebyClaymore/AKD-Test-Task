using UnityEngine;

public class PlayerMovement
{
    private readonly Rigidbody _rb;
    private readonly Transform _body;
    private readonly CameraFollow _cameraFollow;
    private readonly PlayerConfig _config;
    private Vector3 _moveDirection;
    private Vector2 _rotationDirection;

    public PlayerMovement(Rigidbody rb, Transform body, InputSystem inputSystem, CameraFollow cameraFollow, PlayerConfig config)
    {
        _rb = rb;
        _body = body;
        _cameraFollow = cameraFollow;
        _config = config;
        inputSystem.OnMoveDirectionChanged.AddListener(MoveDirectionChanged);
        inputSystem.OnRotationDirectionChanged.AddListener(RotationDirectionChanged);
    }

    public void CustomUpdate()
    {
        Rotation();
    }
    
    public void CustomFixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        if (_moveDirection == Vector3.zero)
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            return;
        }
        var direction = (_body.forward * _moveDirection.z + _body.right * _moveDirection.x).normalized;
        var velocity = direction * _config.MoveSpeed;
        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;
    }
    
    private void Rotation()
    {
        if (_rotationDirection == Vector2.zero)
        {
            _rb.angularVelocity = Vector3.zero;
            return;
        }
        var velocity = new Vector3(0, _rotationDirection.x * _config.RotationSpeed, 0);
        _rb.angularVelocity = velocity;
        var moveVector = _rotationDirection * (_config.Sensitivity * Time.fixedDeltaTime);
        var rotation = new Vector3();
        rotation.x -= moveVector.y;
        rotation.x = Mathf.Clamp(rotation.x, _config.TopClamp, _config.BottomClamp);
        _cameraFollow.SetRotation(rotation);
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