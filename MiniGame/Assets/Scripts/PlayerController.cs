using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private InputActionReference movementControl;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 4f;
    
    private CharacterController _characterController;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    private Transform _cameraMainTransform;

    private void OnEnable()
    {
        movementControl.action.Enable();
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
    }

    private void Start()
    {
        _characterController = gameObject.GetComponent<CharacterController>();
        if (!(Camera.main is null)) _cameraMainTransform = Camera.main.transform;
    }

    private void Update()
    {
        _groundedPlayer = _characterController.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        var movement = movementControl.action.ReadValue<Vector2>();
        var move = new Vector3(movement.x, 0, movement.y);
        move = _cameraMainTransform.forward * move.z + _cameraMainTransform.right * move.x;
        move.y = 0f;
        _characterController.Move(move * (Time.deltaTime * playerSpeed));

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);

        if (movement != Vector2.zero)
        {
            var targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + _cameraMainTransform.eulerAngles.y;
            var playerRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
