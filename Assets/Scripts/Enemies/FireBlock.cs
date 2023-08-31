using Assets.Data;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class FireBlock : MonoBehaviour
    {
        private Animator _animator;
        public AudioSource FireSound;
        public BoxCollider2D KillZone;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnCollisionDetected(Collider2D collision)
        {
            if (collision.gameObject.tag == GameObjectStrings.Player)
            {
                _animator.SetBool(AnimationStrings.HasPlayerInZone, true);
                KillZone.enabled = true;
                FireSound.Play();
                //AudioSource.PlayClipAtPoint(FireSound.clip, gameObject.transform.position, FireSound.volume);
            }
        }

        public void OnCollisionLeave(Collider2D collision)
        {
            if (collision.gameObject.tag == GameObjectStrings.Player)
            {
                _animator.SetBool(AnimationStrings.HasPlayerInZone, false);
                KillZone.enabled = false;
                StartCoroutine(AudioFadeOut.FadeOut(FireSound, .3f));
            }
        }
    }
}