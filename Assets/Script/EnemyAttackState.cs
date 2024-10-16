using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float _distanceToPlayer;
    Health _playerHealth;
    float _damagePerSecond = 10f;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
        _playerHealth = enemy._player.GetComponent<Health>();// go to base enemy component then health
    }
    public override void OnStateEnter()
    {

        Debug.Log("Enemy Idle Entered");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy Idle Exit");
    }

    public override void OnStateUpdate()
    {
        Attack();

        if (_enemy._player != null)
        {
            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);

            if (_distanceToPlayer > _enemy._aggroDistance)
            {
                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }

            _enemy._agent.destination = _enemy._player.position;

        }
        else
        {
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }

    }

    void Attack()
    {
        if (_playerHealth != null )
        {
            _playerHealth.DeductHealth(_damagePerSecond * Time.deltaTime);
        }

    }

}
