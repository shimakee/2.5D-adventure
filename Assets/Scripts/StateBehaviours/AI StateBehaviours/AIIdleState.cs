using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIIdleState : StateMachineBehaviour
{
    ICharacterStateMachine _playerStateMachine;
    IDirectionMoverComponent _mover;
    GetAngleBetweenCamCharFaceDirectionAnimatorComponent _angleToCam;
    IFieldOfView _fieldOfView;

    float _distance;
    float _minDistance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _fieldOfView = animator.GetComponent<IFieldOfView>();
        _playerStateMachine = animator.GetComponent<ICharacterStateMachine>();
        _mover = animator.GetComponent<IDirectionMoverComponent>();
        _angleToCam = animator.GetComponent<GetAngleBetweenCamCharFaceDirectionAnimatorComponent>();


        _minDistance = _playerStateMachine.DistanceThreshold + _playerStateMachine.ArrivingDistance;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("AngleToCamera", _angleToCam.AngledSigned);

        FindTarget();
        _distance = Vector3.Distance(_playerStateMachine.TargetLocation, _mover.CurrentPosition);


        if (_distance > _minDistance)
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

    private void FindTarget()
    {
        if (_fieldOfView.GameObjectsInView.Count > 0)
        {
            var target = _fieldOfView.GameObjectsInView.Where(x => x.tag == "Player").FirstOrDefault();

            if (target != null)
            {
                if (_playerStateMachine.TargetObject != target)
                    _playerStateMachine.SetTargetObject(target);
                else
                    _playerStateMachine.SetTargetLocation(target.transform.position);
            }
        }
    }
}
