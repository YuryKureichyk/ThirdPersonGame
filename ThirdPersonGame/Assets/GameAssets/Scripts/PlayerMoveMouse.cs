using UnityEngine;

public class PlayerMoveMouse : MonoBehaviour
{
    [SerializeField] private InputServes _inputServes;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _mouseSensitivity = 2f;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private float _moveDirectionY;
    private float _rotationY = 0f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        RotateWithMouse();
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
        if (_characterController.isGrounded && _moveDirectionY < 0)
            _moveDirectionY = -2f;

        _moveDirectionY += _gravity * Time.deltaTime;

        Vector3 move = (transform.right * _inputServes.MoveDirection.x) +
                       (transform.forward * _inputServes.MoveDirection.y);

        Vector3 velocity = move * _moveSpeed;
        velocity.y = _moveDirectionY;

        _characterController.Move(velocity * Time.deltaTime);
    }

    private void RotateWithMouse()
    {
        float mouseX = _inputServes.LookDelta.x * _mouseSensitivity;

        if (mouseX != 0)
        {
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}