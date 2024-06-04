using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyConfigData _config;

    [SerializeField]
    private enum State
    {
        Idle, Patrol, Attack, Dead
    }

    private State _currentState;

    private void Start()
    {
        ChangeState(State.Idle);
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Idle:
                break;
            case State.Patrol:
                break;
            case State.Attack:
                break;
            case State.Dead:
                break;
            default:
                break;
        }
    }

    private void ChangeState(State state)
    {
        _currentState = state;
    }
}
