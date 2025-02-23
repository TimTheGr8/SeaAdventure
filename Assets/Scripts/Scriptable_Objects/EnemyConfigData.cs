using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "New EnemyConfigData", menuName = "ScriptableObjects/EnemyConfigData")]
public class EnemyConfigData : ScriptableObject
{
    public float _acceleration, _speedMax;
    public float _attackRange, _fireRange, _fireCooldown;

    [Header("Behavior Properties")]
    public float _timeIdle = 5f;
    public float _timePatrol = 15f;


}
