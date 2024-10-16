using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Execute this before all other scripts
[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public float _horizontal { get; private set; }
    public float _vertical { get; private set; }
    public float _mouseX { get; private set; }
    public float _mouseY { get; private set; }

    public bool _sprintHeld { get; private set; }
    public bool _jumpPressed { get; private set; }
    public bool _interactPressed { get; private set; }

    public bool _pickUpPressed { get; private set; }
    public bool _primaryShootPressed { get; private set; }
    public bool _secondaryShootPressed { get; private set; }

    public bool _weapon1Pressed { get; private set; }
    public bool _weapon2Pressed { get; private set; }

    public bool _commandPressed { get; private set; }

    //for NPC dialog
    public bool _dialogPressed { get; private set; }

    private bool _clear;


    /// <summary>
    /// Singleton Pattern
    /// </summary>
    private static PlayerInput _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
            return;
        }

        _instance = this;
    }

    public static PlayerInput GetInstance() { return _instance; }

    /// <summary>
    ///  End of Singleton Pattern
    /// </summary>


    // Update is called once per frame
    void Update()
    {
        ClearInput();
        ProcessInputs();
    }

    void ProcessInputs()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _sprintHeld = _sprintHeld || Input.GetButton("Fire3");
        _jumpPressed = _jumpPressed || Input.GetButtonDown("Jump");
        _interactPressed = _interactPressed || Input.GetKeyDown(KeyCode.E);
        _pickUpPressed = _pickUpPressed || Input.GetKeyDown(KeyCode.F);

        _primaryShootPressed = _primaryShootPressed || Input.GetButtonDown("Fire1");
        //if (_primaryShootPressed) {
        //    Debug.Log("shoot fire 1");
        //}

        _secondaryShootPressed = _secondaryShootPressed || Input.GetButtonDown("Fire2");
        //if (_primaryShootPressed) {
        //    Debug.Log("shoot fire 2");
        //}

        _weapon1Pressed = _weapon1Pressed || Input.GetKeyDown(KeyCode.Alpha1);
        _weapon2Pressed = _weapon2Pressed || Input.GetKeyDown(KeyCode.Alpha2);

        _commandPressed = _commandPressed || Input.GetKeyDown(KeyCode.G);

        _dialogPressed = _dialogPressed || Input.GetKeyDown(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        _clear = true;
    }

    void ClearInput()
    {
        if (!_clear)
            return;

        _horizontal = 0;
        _vertical = 0;
        _mouseX = 0;
        _mouseY = 0;

        _sprintHeld = false;
        _jumpPressed = false;
        _interactPressed = false;
        _pickUpPressed = false;

        _primaryShootPressed = false;
        _secondaryShootPressed = false;

        _weapon1Pressed = false;
        _weapon2Pressed = false;
        _commandPressed = false;

        _dialogPressed = false;
    }
}
