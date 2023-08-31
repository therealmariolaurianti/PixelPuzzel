using System;
using Assets.Data;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class AutomaticWalking : MonoBehaviour
    {
        private Rigidbody2D _rigidBody;
        private TouchingDirections _touchingDirections;
        private WalkDirection _walkDirection;
        
        private Vector2 _walkDirectionVector = Vector2.right;

        public float MaxSpeed = 4f;
        public float WalkAcceleration = 10f;
        public float WalkStopRate = 0.05f;

        public WalkDirection WalkDirection
        {
            get => _walkDirection;
            private set
            {
                if (_walkDirection != value)
                {
                    _walkDirectionVector = value switch
                    {
                        WalkDirection.Right => Vector2.right,
                        WalkDirection.Left => Vector2.left,
                        _ => throw new Exception()
                    };
                }

                _walkDirection = value;
            }
        }

        private void Awake()
        {
            _touchingDirections = GetComponent<TouchingDirections>();
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_touchingDirections.IsOnWall && _touchingDirections.IsGrounded) 
                FlipDirection();

            var xVelocity = Mathf.Clamp(_rigidBody.velocity.x + WalkAcceleration * _walkDirectionVector.x, -MaxSpeed,
                MaxSpeed);

            _rigidBody.velocity = _touchingDirections.IsGrounded
                ? new Vector2(xVelocity, _rigidBody.velocity.y)
                : new Vector2(Mathf.Lerp(_rigidBody.velocity.x, 0, WalkStopRate), _rigidBody.velocity.y);
        }

        private void FlipDirection()
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1,
                gameObject.transform.localScale.y);

            WalkDirection = WalkDirection switch
            {
                WalkDirection.Right => WalkDirection.Left,
                WalkDirection.Left => WalkDirection.Right,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void OnCliffDetected(Collider2D collision)
        {
            if (_touchingDirections.IsGrounded)
                FlipDirection();
        }
    }
}