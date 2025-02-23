using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyConfigData _config;

    public enum State
    {
        Idle, Patrol, Attack, Dead
    }

    private State _currentState;
    private float _timeStateStart;
    private Rigidbody2D _rb;
    private Vector2 _movementDirection;
    private IBehaviorPatrolWaypoints _behaviorPatrol;
    private GameObject _player;

    private void Awake()
    {
        if(TryGetComponent<IBehaviorPatrolWaypoints>(out _behaviorPatrol))
        {
            _behaviorPatrol.Init(_rb, _movementDirection, _config._acceleration, _config._speedMax);
        }
        _player = GameObject.FindWithTag(Tags.Player);
    }

    private void Start()
    {
        ChangeState(State.Idle);
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Idle:
                if(IsPlayerInRange(_config._attackRange))
                {
                    ChangeState(State.Attack);
                }
                else if (Time.time - _timeStateStart >= _config._timePatrol)
                    ChangeState(State.Patrol);
                break;
            case State.Patrol:
                if (IsPlayerInRange(_config._attackRange))
                {
                    ChangeState(State.Attack);
                }
                else if (Time.time - _timeStateStart >= _config._timePatrol)
                    ChangeState(State.Idle);
                break;
            case State.Attack:
                if(!IsPlayerInRange(_config._attackRange))
                    ChangeState(State.Patrol);
                break;
            case State.Dead:
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        if(_currentState == State.Patrol)
            _behaviorPatrol?.TickPhysics();
        else
            _rb.velocity = Vector2.zero;
    }

    public void ChangeState(State state)
    {
        _currentState = state;
        _timeStateStart = Time.time;
    }

    private bool IsPlayerInRange(float rangeAttack)
    {
        var distance = Vector2.Distance(transform.position, _player.transform.position);
        
        return distance <= rangeAttack;
    }
}
