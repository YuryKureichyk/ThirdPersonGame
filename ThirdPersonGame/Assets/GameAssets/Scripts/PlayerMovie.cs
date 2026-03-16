using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovie : MonoBehaviour
{
    [SerializeField] private InputServes _inputServes;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _rotationSpeed = 10f;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private float _moveDirectionY;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _inputServes.Jump += OnJump;
    }

    private void OnDisable()
    {
        _inputServes.Jump -= OnJump;
    }


    private void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void OnJump()
    {
        if (_characterController.isGrounded)
        {
            _moveDirectionY = 1;
        }
        
    }

    private void MovePlayer()
    {
        _moveDirectionY = Mathf.Max(_gravity, _moveDirectionY + _gravity * Time.deltaTime);
        var moveDirection = new Vector3(_inputServes.MoveDirection.x, _moveDirectionY, _inputServes.MoveDirection.y);
        _characterController.Move(moveDirection * (Time.deltaTime * _moveSpeed));
    }

    private void RotatePlayer()
    {
        Vector3 direction = new Vector3(_inputServes.MoveDirection.x, 0, _inputServes.MoveDirection.y);
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}