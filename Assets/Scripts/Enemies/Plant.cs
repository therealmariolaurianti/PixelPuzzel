using System.Linq;
using Assets.Data;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Plant : MonoBehaviour
    {
        private Animator _animator;
        public DetectionZone KillDetectionZone;
        private bool _hasTarget;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            HasTarget = KillDetectionZone.DetectedColliders.Any(dc => dc.CompareTag(GameObjectStrings.Player));
        }

        public bool HasTarget
        {
            get => _hasTarget;
            set
            {
                _hasTarget = value;
                _animator.SetBool(AnimationStrings.HasTarget, value);
            }
        }

        public void OnPlayerDetected(Collider2D collision)
        {   
            if(collision.CompareTag(GameObjectStrings.Player))
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, 1);
        }
    }
}