using Assets.Data;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class ExitDoor : MonoBehaviour
    {
        private Animator _animator;
        private BoxCollider2D _boxCollider;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();

            CharacterEvents.OpenDoor += OnOpenDoor;
        }

        private void OnOpenDoor()
        {
            _animator.SetTrigger(AnimationStrings.IsOpen);
        }

        public void DoorOpened()
        {
            _boxCollider.enabled = false;
        }

        public void CloseDoor(Collider2D collision)
        {
            _animator.SetTrigger(AnimationStrings.IsClosed);
            _boxCollider.enabled = true;
        }
    }
}