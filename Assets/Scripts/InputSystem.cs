using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSystem
{
    private readonly Dictionary<KeyCode, bool> _keyStates = new()
    {
        { KeyCode.W, false },
        { KeyCode.S, false },
        { KeyCode.D, false },
        { KeyCode.A, false },
    };
    private Vector2 _moveDirection = Vector2.zero;
    private Vector2 _rotationDirection = Vector2.zero;
    private bool _enabled = false;
    public readonly UnityEvent<Vector2> OnMoveDirectionChanged = new ();
    public readonly UnityEvent<Vector2> OnRotationDirectionChanged = new ();
    public readonly UnityEvent OnMouseLeftClick = new ();

    public void LateInitialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _enabled = true;
    }
    
    public void CustomUpdate()
    {
        if(!_enabled)
            return;
        GetInputMove();
        GetInputRotation();
        GetInputMouse();
    }

    private void GetInputMouse()
    {
        if(Input.GetMouseButtonDown(0))
            OnMouseLeftClick?.Invoke();
    }

    private void GetInputMove()
    {
        UpdateKeyState(KeyCode.W);
        UpdateKeyState(KeyCode.S);
        UpdateKeyState(KeyCode.D);
        UpdateKeyState(KeyCode.A);
        RecalculateMoveDirection();
        OnMoveDirectionChanged?.Invoke(_moveDirection);
    }

    private void GetInputRotation()
    {
        _rotationDirection = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"));
        OnRotationDirectionChanged?.Invoke(_rotationDirection);
    }
    
    private void UpdateKeyState(KeyCode key)
    {
        _keyStates[key] = Input.GetKey(key);
    }

    private void RecalculateMoveDirection()
    {
        _moveDirection = Vector2.zero;
        
        if (_keyStates[KeyCode.W])
        {
            _moveDirection.y += 1;
        }
        if (_keyStates[KeyCode.S])
        {
            _moveDirection.y -= 1;
        }
        if (_keyStates[KeyCode.D])
        {
            _moveDirection.x += 1;
        }
        if (_keyStates[KeyCode.A])
        {
            _moveDirection.x -= 1;
        }

        if (_moveDirection.magnitude > 1)
        {
            _moveDirection = _moveDirection.normalized;
        }
    }
}