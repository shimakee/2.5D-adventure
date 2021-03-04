using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStateMachine : MonoBehaviour, ICharacterStateMachine
{
    [Header("Character details:")]
    [SerializeField] float characterHeight;

    [Header("Arriving details:")]
    [SerializeField] float arrivingDistance;
    [SerializeField] float distanceThreshold;
    [SerializeField] float attackDistance;
    [SerializeField] float attackSpeed;


    public float ArrivingDistance { get { return arrivingDistance; } set { arrivingDistance = value; } }
    public float DistanceThreshold { get { return distanceThreshold; } set { distanceThreshold = value; } }
    public float AttackDistance { get { return attackDistance; } set { attackDistance = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }


    public CharacterStates CurrentState { get; set; }
    public GameObject TargetObject { get; private set; }
    public Vector3 TargetLocation { get; private set; }

    Rigidbody _rb;

    private void Awake()
    {
        var position = this.transform.position;
        position.y = position.y - characterHeight;
        SetTargetLocation(position);
    }


    public void SetTargetLocation(Vector3 target)
    {
        target.y = target.y + characterHeight;
        TargetLocation = target;

    }

    public void SetTargetObject(GameObject gameObject)
    {

        if (gameObject == null)
            throw new NullReferenceException("game object cannot be null.");

        TargetObject = gameObject;
        SetTargetLocation(gameObject.transform.position);
    }
}

public enum CharacterStates
{
    idle,
    move,
    attack
}
