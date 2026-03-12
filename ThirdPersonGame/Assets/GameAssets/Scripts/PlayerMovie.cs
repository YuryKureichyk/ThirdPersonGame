using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovie : MonoBehaviour
{
    [SerializeField] private InputServes _inputServes;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;

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
        _inputServes.Jump += OnJump;
    }


    private void Update()
    {
        _moveDirectionY = Mathf.Max(_gravity, _moveDirectionY + _gravity * Time.deltaTime);
        var moveDirection = new Vector3(_inputServes.MoveDirection.x, _moveDirectionY, _inputServes.MoveDirection.y);
        _characterController.Move(moveDirection * (Time.deltaTime * _moveSpeed));
    }

    private void OnJump()
    {
        if (_characterController.isGrounded)
        {
            _moveDirectionY = 1;
        }

        Debug.Log("Jump");
    }
}