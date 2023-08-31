using Assets.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Actions
{
    public class PlayerDamage : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rigidBody;

        public AudioSource DeathSound;
        public GameObject Player;
        public Transform RespawnPoint;

        private static string TrapTag => "Trap";

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TrapTag) && IsAlive) 
                Kill();
        }

        public bool IsAlive => _animator.GetBool(AnimationStrings.IsAlive);

        public void Kill()
        {
            _animator.SetBool(AnimationStrings.IsAlive, false);
            _animator.SetBool(AnimationStrings.CanMove, false);
            _animator.SetTrigger(AnimationStrings.Death);
            _rigidBody.bodyType = RigidbodyType2D.Static;
            

            DeathSound.Play();
            CharacterEvents.PlayerDeath.Invoke();
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Respawn()
        {
            Player.transform.position = RespawnPoint.position;

            Player.SetActive(false);
            Player.SetActive(true);

            _animator.SetBool(AnimationStrings.IsSpawned, true);
            _animator.SetBool(AnimationStrings.IsAlive, true);
            _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}