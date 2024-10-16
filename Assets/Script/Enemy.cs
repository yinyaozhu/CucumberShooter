using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _targetPoints;

    [SerializeField] private Transform _player;
    [SerializeField] private Health _playerHealth;

    [SerializeField] private Transform _enemy;
    [SerializeField] private float _playerCheckDistance = 10f;
    [SerializeField] private float _aggroDistance = 5f;
    [SerializeField] private float _checkRadius = 0.4f;

    int _currentTarget = 0;

    public bool isIdle = true;
    public bool isPlayerFound;
    public bool isCloseToPlayer;

    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Awake()// somehow put it in start will error
    {
        _agent = GetComponent<NavMeshAgent>();
        //_agent.destination = _targetPoints[_currentTarget].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle) {
            Idle();
        }else if (isPlayerFound)
        {
            if (isCloseToPlayer) { 
                AttackPlayer(); 
            } else { 
                FollowPlayer();
            }
        }
    }

    void FollowPlayer()
    {
        if (_player != null)
        {
            if (Vector3.Distance(transform.position, _player.position) > _playerCheckDistance)
            {
                isPlayerFound = false;
                isIdle = true;
            }

            // attack
            if (Vector3.Distance(transform.position, _player.position) < _playerCheckDistance / 5)
            {
                isCloseToPlayer = true;
            }
            else
            {
                isCloseToPlayer = false;
            }

            _agent.destination = _player.position;

        }
        else {
            isPlayerFound = false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }

    void Idle() {
        if (_agent.remainingDistance < 0.1f) {
            _currentTarget++;
            if (_currentTarget >= _targetPoints.Length)
            {
                _currentTarget = 0;
            }

            //_agent.destination = _targetPoints[_currentTarget].position;
        }

        if (Physics.SphereCast(_enemy.position, _checkRadius, transform.forward, out RaycastHit hit, _aggroDistance))
        {
            if (hit.transform.CompareTag("Player")) {
                Debug.Log("Player Found");
                isIdle = false;
                isPlayerFound = true;
                _player = hit.transform;
                _agent.destination = _player.position;
            }
        }
    }

    private void AttackPlayer()
    {
        //Debug.Log("Attacking Player");
        _playerHealth.DeductHealth(1);
        if (Vector3.Distance(transform.position, _player.position) > _playerCheckDistance / 5) {
            isCloseToPlayer = false;
        }
    }

}
