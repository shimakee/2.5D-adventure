using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : StateMachineBehaviour
{
    //ICharacterStateMachine _playerStateMachine;
    //IDirectionMoverComponent _mover;
    GetAngleBetweenCamCharFaceDirectionAnimatorComponent _angleToCam;



    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //_playerStateMachine = animator.GetComponent<ICharacterStateMachine>();
        //_mover = animator.GetComponent<IDirectionMoverComponent>();
        _angleToCam = animator.GetComponent<GetAngleBetweenCamCharFaceDirectionAnimatorComponent>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("AngleToCamera", _angleToCam.AngledSigned);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("State", 0);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
