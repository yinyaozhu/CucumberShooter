using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UIElements;

public class EnemyIdleState : EnemyState
{
    int _currentTarget = 0;

    public EnemyIdleState(EnemyController enemy) :base(enemy){ 
    
    }

    public override void OnStateEnter()
    {
        _enemy._agent.destination = _enemy._targetPoints[_currentTarget].position;
        Debug.Log("Enemy Idle Entered");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy Idle Exit");
    }

    public override void OnStateUpdate()
    {
        if (_enemy._agent.remainingDistance < 0.1f)
        {
            _currentTarget++;
            if (_currentTarget >= _enemy._targetPoints.Length)
            {
                _currentTarget = 0;
            }
            _enemy._agent.destination = _enemy._targetPoints[_currentTarget].position;
        }

        if (Physics.SphereCast(_enemy.transform.position, _enemy._checkRadius, _enemy.transform.forward, out RaycastHit hit, _enemy._aggroDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player Found");
                _enemy._player = hit.transform;
                _enemy._agent.destination = _enemy._player.position;

                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }
        }
    }
}
