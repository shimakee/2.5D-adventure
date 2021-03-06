using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToDestinationCheckBehaviour : StateMachineBehaviour
{
    ICharacterStateMachine _playerStateMachine;
    IDirectionMoverComponent _mover;

    float _distance;
    float _minDistance;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerStateMachine = animator.GetComponent<ICharacterStateMachine>();
        _mover = animator.GetComponent<IDirectionMoverComponent>();

        _minDistance = _playerStateMachine.DistanceThreshold + _playerStateMachine.ArrivingDistance;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _distance = Vector3.Distance(_playerStateMachine.TargetLocation, _mover.CurrentPosition);

        bool isClose = _distance <= _minDistance;
        if (isClose)
        {
            animator.SetInteger("State", (int)CharacterStates.idle);
            _playerStateMachine.CurrentState = CharacterStates.idle;
        }
        else
        {
            animator.SetInteger("State", (int)CharacterStates.move);
            _playerStateMachine.CurrentState = CharacterStates.move;
        }
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
