using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayermovementBehaviour : MonoBehaviour
{

    private PlayerInput _input;

    [Header("Player Moverment")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _sprintMultiplier;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;// this is a empty game object within player parent to check
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckDistance;

    private CharacterController _characterController;

    private Vector3 _playerVelocity;

    public bool _isGrounded { get; private set; }
    private float _moveMultiplier = 1f; // this is like shift for add speed

    // Start is called before the first frame update
    void Start()
    {
        _input = PlayerInput.GetInstance();// why can we just get instance like this?

        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MovePlayer();
    }

    void MovePlayer()
    {
        _moveMultiplier = _input._sprintHeld ? _sprintMultiplier : 1f; //jia su pao
        
        _characterController.Move((transform.forward * _input._vertical + transform.right * _input._horizontal) * _moveSpeed * Time.deltaTime * _moveMultiplier);

        // ground check
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundMask);
    }

    public void SetVelocity(float value) {
        _playerVelocity.y = value;
    }

    public float GetForwardSpeed()
    {
        return _input._vertical * _moveSpeed * _moveMultiplier;
    }

}
