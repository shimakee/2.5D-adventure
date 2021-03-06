using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComputation : MonoBehaviour
{
    ICharacterStateMachine _characterStateMachine;

    private void Awake()
    {
        _characterStateMachine = GetComponent<ICharacterStateMachine>();
    }
    public void Attack(AnimationEvent animationEvent)
    {
        float weight = animationEvent.animatorClipInfo.weight;

        if(weight >= .5f)
        {
            var target = _characterStateMachine.TargetObject;
            Debug.Log($"Attacked {target.name}", this);

            var targetStateMachine = target.GetComponent<ICharacterStateMachine>();
            var targetAnimator = target.GetComponent<Animator>();

            if (targetStateMachine != null)
            {
                target.GetComponent<IDirectionMoverComponent>().MoveDirection(Vector3.zero);
                targetAnimator.SetTrigger("Hurt");
                target.GetComponent<IDirectionMoverComponent>().MoveDirection(Vector3.zero);
            }
        }
    }
}
