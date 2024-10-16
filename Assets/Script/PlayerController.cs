using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//old controller file, should not use
public class PlayerController : MonoBehaviour
{
    [Header("Player Moverment")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _sprintMultiplier;
    [SerializeField] private float _turnSpeed; // CameraMovementBehaviour
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private bool _invertMouse; // CameraMovementBehaviour
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _gravity = -9.81f;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;// this is a empty game object within player parent to check
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckDistance;

    [Header("Shooting")]
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private float _shootForce;
    [SerializeField] private Transform _shootPoint;

    [Header("Interactable")]
    [SerializeField] private LayerMask _buttonLayer;
    [SerializeField] private float _rayCastDistance;
    private RaycastHit _rayCastHit;
    private ISelectable _selectable;
    private Camera _cam;


    [Header("Pick and Drop")]
    [SerializeField] private LayerMask _pickupLayer;
    [SerializeField] private float _pickupDistance = 5f;
    [SerializeField] private Transform _attachTransform;
    bool isPicked;
    IPickable _pickable;


    [Header("Gun Movement")]
    [SerializeField] private float _maxGunRotationAngle = 10f;
    [SerializeField] private float _rotationSpeed = 5f;

    private CharacterController _characterController;

    private float _horizontal, _vertical;
    private float _mouseX, _mouseY;
    private float _camXRotation; // CameraMovementBehaviour
    private float _moveMultiplier = 1f; // this is like shift for add speed
    private Vector3 _playerVelocity;
    private bool _isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _cam = Camera.main;

        //hide the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MovePlayer();
        RotatePlayer();
        GroundCheck();
        JumpCheck();

        ShootBullet();
        Interact();
        PickAndDrop();
    }

    void GetInput() { // private by default
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _moveMultiplier = Input.GetButton("Fire3") ? _sprintMultiplier : 1;
    }

    void MovePlayer()
    {
        _characterController.Move((transform.forward * _vertical + transform.right * _horizontal) * _moveSpeed * Time.deltaTime * _moveMultiplier);

        // ground check
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    void RotatePlayer()
    {
        //turning player
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _mouseX);

        // camera up and down
        _camXRotation += Time.deltaTime * _mouseY * _turnSpeed * (_invertMouse ? 1 : -1);
        _camXRotation = Mathf.Clamp(_camXRotation, -85f, 85f);

        _cameraTransform.localRotation = Quaternion.Euler(_camXRotation, 0, 0);
    }

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundMask);
    }

    void JumpCheck() {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _playerVelocity.y = _jumpVelocity;

        }
    }

    void ShootBullet() {
        if (Input.GetButtonDown("Fire1")) {
            Rigidbody bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
            bullet.AddForce(_shootPoint.forward * _shootForce);
            Destroy(bullet.gameObject, 5.0f);
        }
    }

    private void Interact()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray,out _rayCastHit, _rayCastDistance, _buttonLayer)) {
            // temp hold last ray cast
            _selectable = _rayCastHit.transform.GetComponent<ISelectable>();
            if (_selectable != null)
            {
                _selectable.OnHoverEnter();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _selectable.OnSelect();
                }
            }
        }

        if (_rayCastHit.transform == null && _selectable != null) {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    }

    private void PickAndDrop() { 
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray,out _rayCastHit, _rayCastDistance, _pickupLayer))
        {
            if (Input.GetKeyDown(KeyCode.F) && !isPicked)
            {
                _pickable = _rayCastHit.transform.GetComponent<IPickable>();
                if (_pickable == null) return;

                _pickable.OnPicked(_attachTransform);
                isPicked = true;
                return;
            }

        }

        if (Input.GetKeyDown(KeyCode.F) && _pickable != null)
        {
            _pickable.OnDropped();
            isPicked = false;
           
        }
    }

}
