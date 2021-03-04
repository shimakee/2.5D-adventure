using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(IDirectionMoverComponent))]
public class PlayerMoveState : StateMachineBehaviour
{
    ICharacterStateMachine _playerStateMachine;
    IDirectionMoverComponent _mover;
    GetAngleBetweenCamCharFaceDirectionAnimatorComponent _angleToCam;

    float _distance;
    Vector3 _direction;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerStateMachine = animator.GetComponent<ICharacterStateMachine>();
        _mover = animator.GetComponent<IDirectionMoverComponent>();
        _angleToCam = animator.GetComponent<GetAngleBetweenCamCharFaceDirectionAnimatorComponent>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("AngleToCamera", _angleToCam.AngledSigned);

        _distance = Vector3.Distance(_playerStateMachine.TargetLocation, _mover.CurrentPosition);

        if(_playerStateMachine.TargetObject != null)
        {
            if (_playerStateMachine.TargetObject.tag == "Enemy")
                _playerStateMachine.SetTargetLocation(_playerStateMachine.TargetObject.transform.position);
        }


        _direction += SteeringBehaviour.Seek(_playerStateMachine.TargetLocation, _mover.CurrentVelocity, _mover) * Time.deltaTime;
        //_direction = SteeringBehaviour.Arriving(_mover, _direction, _playerStateMachine.TargetLocation, _playerStateMachine.ArrivingDistance, _playerStateMachine.DistanceThreshold);


        if(_playerStateMachine.TargetObject != null)
            if (_playerStateMachine.TargetObject.tag == "Enemy" && _distance <= _playerStateMachine.AttackDistance)
            {
                _direction = Vector3.zero;
                animator.SetInteger("State", (int)CharacterStates.attack);
                _playerStateMachine.CurrentState = CharacterStates.attack;
            }



        if (_distance <= _playerStateMachine.ArrivingDistance + _playerStateMachine.DistanceThreshold)
        {
            _direction = Vector3.zero;
            animator.SetInteger("State", (int)CharacterStates.idle);
            _playerStateMachine.CurrentState = CharacterStates.idle;
        }

        _mover.MoveDirection(_direction);

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
