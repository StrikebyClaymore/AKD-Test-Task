using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private Garage _garage;
    private InputSystem _inputSystem;

    private void Awake()
    {
        _inputSystem = new InputSystem();
        _player.Initialize(_inputSystem, _cameraFollow);
        _cameraFollow.Initialize(_player);
    }

    private void Start()
    {
        StartCoroutine(LateInitialize());
    }
    
    private IEnumerator LateInitialize()
    {
        yield return new WaitForSeconds(1);
        _inputSystem.LateInitialize();
        _garage.OpenDoors();
    }

    private void Update()
    {
        _inputSystem.CustomUpdate();
        _player.CustomUpdate();
    }

    private void FixedUpdate()
    {
        _player.CustomFixedUpdate();
    }
    
    private void LateUpdate()
    {
        _cameraFollow.CustomLateUpdate();
    }
}
