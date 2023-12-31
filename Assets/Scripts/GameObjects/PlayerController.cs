using Assets.Data;
using Assets.Scripts.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        public AudioSource JumpSound;

        public float SprintSpeed = 10f;
        public float WalkSpeed = 8f;

        private readonly float _accelerationRate = 10f;
        private readonly float _coyoteTime = 0.02f;
        private readonly float _decelerationRate = 10f;
        private readonly float _jumpCutMultiplier = .5f;

        private Animator _animator;

        private bool _bufferJump;
        private float _coyoteTimeCounter;
        private bool _isFacingRight = true;
        private bool _isJumping;
        private bool _isMoving;
        private bool _isSprinting;
        private Vector2 _moveInput;
        private Rigidbody2D _rigidBody;
        private bool _spawned;
        private TouchingDirections _touchingDirections;
        public float JumpImpulse => 9f;
        public float IdleSpeed => 0f;
        public float AirSpeed => 9f;

        public bool IsFacingRight
        {
            get => _isFacingRight;
            set
            {
                if (_isFacingRight != value)
                    transform.localScale *= new Vector2(-1, 1);

                _isFacingRight = value;
            }
        }

        public bool CanMove => _animator.GetBool(AnimationStrings.CanMove);

        public bool IsMoving
        {
            get => _isMoving;
            private set
            {
                _isMoving = value;
                _animator.SetBool(AnimationStrings.IsMoving, value);
            }
        }

        public bool IsSprinting
        {
            get => _isSprinting;
            private set
            {
                _isSprinting = value;
                _animator.SetBool(AnimationStrings.IsSprinting, value);
            }
        }

        public float MoveSpeed
        {
            get
            {
                if (!CanMove)
                    return IdleSpeed;
                if (!_touchingDirections.IsGrounded && !_touchingDirections.IsOnWall && IsMoving)
                    return AirSpeed;
                if (IsMoving && _touchingDirections.IsOnWall)
                    return IdleSpeed;
                if (IsMoving && !IsSprinting)
                    return WalkSpeed;
                if (IsMoving && IsSprinting)
                    return SprintSpeed;
                return IdleSpeed;
            }
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _touchingDirections = GetComponent<TouchingDirections>();
        }

        private void Update()
        {
            if (_spawned)
                return;

            _spawned = true;
            _animator.SetTrigger(AnimationStrings.IsSpawned);
        }

        private void FixedUpdate()
        {
            if (_rigidBody.bodyType == RigidbodyType2D.Static)
                return;

            _rigidBody.velocity = new Vector2(_moveInput.x * MoveSpeed, _rigidBody.velocity.y);
            _animator.SetFloat(AnimationStrings.YVelocity, _rigidBody.velocity.y);

            UpdateGravityScale();
            ApplyMovementForce();
        }

        private void ApplyMovementForce()
        {
            var targetSpeed = _moveInput * MoveSpeed;
            var speedDifference = targetSpeed.x - _rigidBody.velocity.x;
            var accelerationRate = Mathf.Abs(SprintSpeed) > 0.01f ? _accelerationRate : _decelerationRate;
            var movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, 1f) * Mathf.Sign(speedDifference);

            _rigidBody.AddForce(movement * Vector2.right);
        }

        private void UpdateGravityScale()
        {
            if (_rigidBody.velocity.y < 0)
                _rigidBody.gravityScale = 1.8f;
            else
                _rigidBody.gravityScale = 1;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
            IsMoving = _moveInput != Vector2.zero;

            SetDirection();
        }

        public void OnGroundDetected(Collider2D collision)
        {
            _isJumping = false;

            if (_bufferJump)
            {
                _bufferJump = false;
                DoJump();
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.action.IsPressed())
                if (_isJumping)
                {
                    _rigidBody.AddForce(Vector2.down * _rigidBody.velocity.y * (1 - _jumpCutMultiplier),
                        ForceMode2D.Impulse);
                    return;
                }

            _coyoteTimeCounter = _coyoteTime;

            if (context.started && _spawned)
            {
                if (!_touchingDirections.IsGrounded && !_isJumping)
                {
                    _coyoteTimeCounter -= Time.deltaTime;
                    if (_coyoteTimeCounter > 0)
                        DoJump();
                }
                else if (!_touchingDirections.IsGrounded && _isJumping)
                {
                    _bufferJump = true;
                }
                else if (_touchingDirections.IsGrounded)
                {
                    DoJump();
                }
            }

            if (context.canceled)
                _animator.SetBool(AnimationStrings.IsJumping, false);

            _coyoteTimeCounter = 0f;
        }

        private void DoJump()
        {
            _animator.SetBool(AnimationStrings.IsJumping, true);

            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpImpulse) * 1.2f;

            JumpSound.Play();
            _isJumping = true;
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            // if (context.started)
            //     IsSprinting = true;
            // else if (context.canceled)
            //     IsSprinting = false;
        }

        private void SetDirection()
        {
            IsFacingRight = _moveInput.x switch
            {
                > 0 when !IsFacingRight => true,
                < 0 when IsFacingRight => false,
                _ => IsFacingRight
            };
        }

        public void StartPlayer()
        {
            _animator.SetBool(AnimationStrings.CanMove, true);
        }
    }
}