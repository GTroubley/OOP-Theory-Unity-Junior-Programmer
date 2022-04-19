using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _gravityModifier = 3f;
    [SerializeField] private float _jumpForce = 15f;

    private PlayerControls _playerControls;
    private Rigidbody _rigidbody;
    private Vector2 _movementInput;
    private Vector3 _moveDirection;
    private bool _jumpInput;
    private bool _isGrounded;
    private bool _isPlayerUnderEffect;

    // ENCAPSULATION
    #region Properties
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    
    public float JumpForce
    {
        get => _jumpForce;
        set => _jumpForce = value;
    }
    
    public bool IsPlayerUnderEffect
    {
        get => _isPlayerUnderEffect;
        set => _isPlayerUnderEffect = value;
    }

    #endregion

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= _gravityModifier;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void OnEnable()
    {
        _playerControls.PlayerMovement.Movement.performed += OnMovement;
        _playerControls.PlayerMovement.Movement.canceled += OnMovement;
        _playerControls.PlayerActions.Jump.performed += OnJump;
        _playerControls.PlayerActions.Jump.canceled += OnJump;
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.PlayerMovement.Movement.performed -= OnMovement;
        _playerControls.PlayerMovement.Movement.canceled -= OnMovement;
        _playerControls.PlayerActions.Jump.performed -= OnJump;
        _playerControls.PlayerActions.Jump.canceled -= OnJump;
        _playerControls.Disable();
    }

    private void OnMovement(InputAction.CallbackContext obj)
    {
        _movementInput = obj.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        _jumpInput = obj.ReadValueAsButton();
        if (_jumpInput && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }

    // Method that handles the player movement
    private void HandleMovement()
    {
        var horizontalMovement = Vector3.right * _movementInput.x * _speed;
        var verticalMovement = Vector3.forward * _movementInput.y * _speed;
        _moveDirection = horizontalMovement + verticalMovement;
        _moveDirection.y = _rigidbody.velocity.y;
        _rigidbody.velocity = _moveDirection;
    }

    // When rigidbody collides with the floor player is grounded
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
            _isGrounded = true;
    }

    // When entering a powerup trigger start the effect
    private void OnTriggerEnter(Collider other)
    {
        if (!_isPlayerUnderEffect && other.gameObject.CompareTag("Powerup"))
        {
            var powerup = other.GetComponent<BasePowerup>();
            powerup.StartEffect();
        }
    }
}