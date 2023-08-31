using UnityEngine;

namespace Assets.Scripts
{
    public class SetBoolBehavior : StateMachineBehaviour
    {
        public bool IsUpdateOnState;
        public bool IsUpdateOnStateMachine;
        public string PropertyName;
        public bool ValueOnEnter;
        public bool ValueOnExit;

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (IsUpdateOnStateMachine)
                animator.SetBool(PropertyName, ValueOnEnter);
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (IsUpdateOnStateMachine)
                animator.SetBool(PropertyName, ValueOnExit);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (IsUpdateOnState)
                animator.SetBool(PropertyName, ValueOnEnter);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (IsUpdateOnState)
                animator.SetBool(PropertyName, ValueOnExit);
        }
    }
}