using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int _targetFps = 60;

    private void OnValidate()
    {
        Application.targetFrameRate = _targetFps;
    }
}