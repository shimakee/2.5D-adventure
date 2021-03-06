﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISteeringBehaviour
{
    Vector3 CalculateSteeringForce(IMoverComponent mover, IFieldOfView fieldOfView);
}

public interface IFieldOfView
{
    float Radius { get; }
    float Angle { get; }
    List<GameObject> GameObjectsInView { get; }
    List<GameObject> GameObjectsInSurroundings { get; }
}

public interface ICharacterStateMachine
{
    //float ArrivingDistance { get; }
    //Vector3 Direction { get; set; }
    CharacterStates CurrentState { get; set; }
    GameObject TargetObject { get; }
    Vector3 TargetLocation { get;}

    void SetTargetObject(GameObject gameObject);
    void SetTargetLocation(Vector3 target);


    //TODO
    //CharacterInfo
    float ArrivingDistance { get; set; }
    float DistanceThreshold { get; set; }
    float AttackDistance { get; set; }
    float AttackSpeed { get; set; }
    //attackSpd
    //attackDamage
    //attackRange
    //movespeed
}


public interface IAiBehaviour
{
    float minIdleTime { get; set; }
    float maxIdleTime { get; set; }
}

public interface IAiStateMachine
{
    IAiState CurrentState { get; }
    GameObject GameObject { get; }
    ICharacterStateMachine CharacterStateMachine { get; }
    IDirectionMoverComponent MoverComponent { get; }
    IFieldOfView FieldOfViewComponent { get; }

    IAiState AttackState { get; }
    IAiState WanderState { get; }
    //IAiState IdleState { get; }
    IAiState EngagedState { get; }
    IAiState FleeState { get; }

    string[] EnemyTags { get; }
    string[] NeutralTags { get; }
    string[] FriendlyTags { get; }
}

public interface IAiState
{
    IAiState Execute(IAiStateMachine stateMachine, float deltaTime);
}

