using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(PlayermovementBehaviour))]
public class PlayerJumpBehaviour : Interactor
{
    [Header("Jump")]
    [SerializeField] private float _jumpVelocity;

    private PlayermovementBehaviour _moveBehaviour;
    public override void Interact()
    {
        if (_moveBehaviour == null)
        {
            _moveBehaviour = GetComponent<PlayermovementBehaviour>();
        }

        if (_input._jumpPressed && _moveBehaviour._isGrounded)
        {
            _moveBehaviour.SetVelocity(_jumpVelocity);// set y velocity
        }
    }
}
