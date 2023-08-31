using Assets.Data;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class TouchingDirections : MonoBehaviour
    {
        private readonly RaycastHit2D[] _ceilingHits = new RaycastHit2D[5];
        private readonly RaycastHit2D[] _groundHits = new RaycastHit2D[5];
        private readonly RaycastHit2D[] _wallHits = new RaycastHit2D[5];

        private Animator _animator;
        private bool _isGrounded;
        private bool _isOnCeiling;
        private bool _isOnWall;
        private Rigidbody2D _rigidBody;
        private Collider2D _touchingCollider;
        public float CeilingDistance = 0.05f;

        public ContactFilter2D ContactFilter;
        public float GroundDistance = 0.05f;
        public float WallDistance = 0.2f;

        [SerializeField]
        public bool IsGrounded
        {
            get => _isGrounded;
            private set
            {
                _isGrounded = value;
                _animator.SetBool(AnimationStrings.IsGrounded, value);
            }
        }

        public bool IsOnCeiling
        {
            get => _isOnCeiling;
            set
            {
                _isOnCeiling = value;
                _animator.SetBool(AnimationStrings.IsOnCeiling, value);
            }
        }

        public bool IsOnWall
        {
            get => _isOnWall;
            set
            {
                _isOnWall = value;
                _animator.SetBool(AnimationStrings.IsOnWall, value);
            }
        }

        private Vector2 WallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            SetCollider();
        }

        private void SetCollider()
        {   
            Collider2D x = GetComponent<CapsuleCollider2D>();
            if (x == null)
                x = GetComponent<BoxCollider2D>();

            _touchingCollider = x;
        }

        private void FixedUpdate()
        {
            var x = gameObject.GetComponentsInChildren<Collider2D>();

            IsGrounded = _touchingCollider.Cast(Vector2.down, ContactFilter, _groundHits, GroundDistance) > 0;
            IsOnWall = _touchingCollider.Cast(WallCheckDirection, ContactFilter, _wallHits, WallDistance) > 0;
            IsOnCeiling = _touchingCollider.Cast(Vector2.up, ContactFilter, _ceilingHits, CeilingDistance) > 0;
        }
    }
}