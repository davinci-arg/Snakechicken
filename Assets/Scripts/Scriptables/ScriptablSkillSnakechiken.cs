using UnityEngine;

[CreateAssetMenu(fileName = "Skill Settings")]
public class ScriptablSkillSnakechiken : ScriptableObject
{
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _speedFalling;

    public float MoveDistance => _moveDistance;
    public float SpeedMovement => _speedMovement;
    public float SpeedFalling => _speedFalling;
}
