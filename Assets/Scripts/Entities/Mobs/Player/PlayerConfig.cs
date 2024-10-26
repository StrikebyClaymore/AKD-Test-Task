using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PlayerConfig", fileName = "PlayerConfig", order = 51)]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5;
    [field: SerializeField] public float Sensitivity { get; private set; } = 400;
    [field: SerializeField] public float TopClamp { get; private set; } = -90f;
    [field: SerializeField] public float BottomClamp { get; private set; } = 90f;
    [field: SerializeField] public float RaycastInterval { get; private set; } = 0.2f;
    [field: SerializeField] public float RaycastDistance { get; private set; } = 2f;
    [field: SerializeField] public LayerMask RaycastLayer { get; private set; }
}