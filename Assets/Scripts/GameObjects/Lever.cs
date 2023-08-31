using Assets.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.GameObjects
{
    public class Lever : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            _animator.SetTrigger(AnimationStrings.LeverOn);
        }
    }
}
