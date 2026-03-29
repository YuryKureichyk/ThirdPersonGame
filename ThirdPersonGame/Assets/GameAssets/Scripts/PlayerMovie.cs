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
    private Animator _animator;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        _inputServes.Jump += OnJump;
    }

    private void OnDisable()
    {
        _inputServes.Jump -= OnJump;
    }

    private void Start()
    {
        _moveDirectionY = -2f;
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
            _moveDirectionY = 10;

            _animator.SetTrigger("Jump");
        }
    }

    private void MovePlayer()
    {
        if (_characterController.isGrounded && _moveDirectionY < 0)
        {
            _moveDirectionY = -2f;
        }
        else
        {
            _moveDirectionY += _gravity * Time.deltaTime;
        }


        Vector3 inputDir = new Vector3(_inputServes.MoveDirection.x, 0, _inputServes.MoveDirection.y);
        if (inputDir.sqrMagnitude > 1f) inputDir.Normalize();


        _animator.SetFloat("Speed", inputDir.magnitude);


        Vector3 velocity = (inputDir * _moveSpeed);
        velocity.y = _moveDirectionY;

        _characterController.Move(velocity * Time.deltaTime);
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