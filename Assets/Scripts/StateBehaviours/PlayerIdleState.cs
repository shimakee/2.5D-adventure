using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : StateMachineBehaviour
{

    ICharacterStateMachine _playerStateMachine;
    IDirectionMoverComponent _mover;
    GetAngleBetweenCamCharFaceDirectionAnimatorComponent _angleToCam;

    float _distance;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerStateMachine = animator.GetComponent<ICharacterStateMachine>();
        _mover = animator.GetComponent<IDirectionMoverComponent>();
        _angleToCam = animator.GetComponent<GetAngleBetweenCamCharFaceDirectionAnimatorComponent>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("AngleToCamera", _angleToCam.AngledSigned);

        _distance = Vector3.Distance(_playerStateMachine.TargetLocation, _mover.CurrentPosition);

        var distanceWant = _playerStateMachine.DistanceThreshold + _playerStateMachine.ArrivingDistance;

        if (_distance > distanceWant)
        {
            animator.SetInteger("State", (int)CharacterStates.move);
            _playerStateMachine.CurrentState = CharacterStates.move;
        }
        else
            return;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
