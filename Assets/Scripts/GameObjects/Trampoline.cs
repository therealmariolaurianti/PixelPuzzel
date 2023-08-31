using Assets.Data;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class Trampoline : MonoBehaviour
    {
        private Animator _animator;

        public float JumpHeight;
        public GameObject Player;
        public AudioSource SpringJumpSound;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _animator.SetTrigger(AnimationStrings.PlayerLanded);

            var rigidBody = Player.GetComponent<Rigidbody2D>();
            rigidBody.velocity = new Vector2(0, JumpHeight);
            SpringJumpSound.Play(); 
        }
    }
}