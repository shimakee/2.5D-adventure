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
                //TODO: probably trigger hurt animation on threshold of pain? so that you dont stutter every hit but only the high damage ones.
                targetAnimator.SetTrigger("Hurt");
            }
        }
    }
}
