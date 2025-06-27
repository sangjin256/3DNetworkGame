using UnityEngine;

public class PlayerMovement : PlayerAbility
{
    private CharacterController _controller;
    private Animator _animator;

    private const float GRAVITY = -9.8f;
    private Vector3 _direction;
    [SerializeField] private float _yVelocity;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        _animator.SetFloat("Vertical", vertical);
        _animator.SetFloat("Horizontal", horizontal);

        _direction = transform.TransformDirection(new Vector3(horizontal, 0, vertical));
        _direction.y = -0.01f;
        if (Input.GetKeyDown(KeyCode.Space) && _controller.isGrounded)
        {
            _yVelocity = _owner.Stat.JumpPower;
            _animator.SetTrigger("JumpStart");
        }

        Debug.Log(_controller.collisionFlags);

        if (_controller.isGrounded == false)
        {
            _yVelocity += GRAVITY * Time.deltaTime;
        }
        else
        {
            if(_yVelocity != -0.01f) _animator.SetTrigger("JumpEnd");
        }

        _direction.y = _yVelocity;
        _controller.Move(_direction * _owner.Stat.MoveSpeed * Time.deltaTime);
    }
}
