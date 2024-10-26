using System.Collections;
using UnityEngine;

public class Garage : MonoBehaviour
{
    [SerializeField] private Transform _doorLeft;
    [SerializeField] private Transform _doorRight;
    [SerializeField] private Vector3 _doorLeftOpenRotation;
    [SerializeField] private Vector3 _doorRightOpenRotation;
    [SerializeField] private float _doorsOpenDuration = 3f;
        
    public void OpenDoors()
    {
        StartCoroutine(OpenDoorsCycle());
    }

    private IEnumerator OpenDoorsCycle()
    {
        var progress = 0f;
        var rotationLeft = _doorLeft.transform.localEulerAngles;
        var rotationRight = _doorRight.transform.localEulerAngles;
        while (progress < 1f)
        {
            progress += Time.unscaledDeltaTime * (1.0f / _doorsOpenDuration);
            _doorLeft.transform.localRotation = Quaternion.Euler(
                Vector3.Lerp(rotationLeft, _doorLeftOpenRotation, progress));
            _doorRight.transform.localRotation = Quaternion.Euler(
                Vector3.Lerp(rotationRight, _doorRightOpenRotation, progress));
            yield return null;
        }
    }
}