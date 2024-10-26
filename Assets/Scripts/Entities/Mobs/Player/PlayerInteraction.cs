using UnityEngine;

public class PlayerInteraction
{
    private readonly PlayerConfig _config;
    private readonly Transform _hands;
    private readonly Camera _camera;
    private float _raycastTimer = 0f;
    private Item _itemInHands;
    private ISelectable _selectedObject;

    public PlayerInteraction(InputSystem inputSystem, CameraFollow cameraFollow, PlayerConfig config, Transform hands)
    {
        _camera = cameraFollow.Camera;
        _config = config;
        _hands = hands;
        inputSystem.OnMouseLeftClick.AddListener(LeftClickAction);
    }

    public void CustomUpdate()
    {
        RaycastDetection();
    }
    
    private void LeftClickAction()
    {
        switch (_selectedObject)
        {
            case Car car:
                _itemInHands = car.TryPutItem(_itemInHands);
                break;
            case Item item:
                _itemInHands = item.Pickup(_hands, _hands.localPosition);
                break;
        }
    }
    
    private void RaycastDetection()
    {
        _raycastTimer += Time.deltaTime;
        if (_raycastTimer >= _config.RaycastInterval)
        {
            _raycastTimer = 0f;
            Vector3 rayOrigin = _camera.transform.position;
            Vector3 rayDirection = _camera.transform.forward;
            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, _config.RaycastDistance, _config.RaycastLayer))
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
}