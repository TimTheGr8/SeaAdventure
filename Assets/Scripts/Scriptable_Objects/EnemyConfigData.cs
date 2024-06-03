using UnityEngine;
[CreateAssetMenu(fileName = "New EnemyConfigData", menuName = "ScriptableObjects/EnemyConfigData")]
public class EnemyConfigData : ScriptableObject
{
    public float _speed, _attackRange, _fireRange, _fireCooldown;
}
