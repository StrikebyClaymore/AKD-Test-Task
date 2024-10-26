using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _hands;
    [SerializeField] private PlayerConfig _config;
    private PlayerMovement _movement;
    private PlayerInteraction _interaction;

    public void Initialize(InputSystem inputSystem, CameraFollow cameraFollow)
    {
        _movement = new PlayerMovement(_rb, _body, inputSystem, cameraFollow, _config);
        _interaction = new PlayerInteraction(inputSystem, cameraFollow, _config, _hands);
    }

    public void CustomUpdate()
    {
        _interaction?.CustomUpdate();
        _movement?.CustomUpdate();
    }

    public void CustomFixedUpdate()
    {
        _movement?.CustomFixedUpdate();
    }
}
