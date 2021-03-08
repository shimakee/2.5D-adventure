
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WanderStateBehaviourComponent : IAiState
{
    [Header("Wandering details:")]
    [Range(-360, 360)] [SerializeField] public float maxDirectionAngleRange;
    [Range(.1f, 20f)] [SerializeField] float radius;
    [Space(10)]


    [Header("Wander time info:")]
    [Range(0, 20f)] [SerializeField] float minWanderTimeInterval;
    [Range(0, 20f)] [SerializeField] float maxWanderTimeInterval;
    [Range(0, 20f)] [SerializeField] float minDistanceAhead;
    [Range(0, 20f)] [SerializeField] float maxDistanceAhead;
    [Space(10)]

    [Header("SpawnPoin details:")]
    [SerializeField] Vector3 spawnPoint;
    [Range(0, 20)] [SerializeField] float maxDistanceFromSpawnPoint;



    private float _time;
    private Vector3 _wanderPoint;
    private float _wanderIntervalTime;
    private float _distanceAhead;
    private float _distanceFromSpawnPoint;

    public IAiState Execute(IAiStateMachine stateMachine, float deltaTime)
    {
        _time += deltaTime;

        if (FindEnemy(stateMachine))
            Debug.Log("Enemy Found.");
            //return stateMachine.EngagedState;
        Wander(stateMachine, _wanderIntervalTime);
        StayNearSpawnPoint(stateMachine);

        return this;
    }

    #region Wander

    private void Wander(IAiStateMachine stateMachine, float interval)
    {
        if (_time > interval)
        {
            _time = 0;

            _wanderIntervalTime = UnityEngine.Random.Range(minWanderTimeInterval, maxWanderTimeInterval);
            _distanceAhead = UnityEngine.Random.Range(minDistanceAhead, maxDistanceAhead);
            _wanderPoint = PickAPointInFront(stateMachine.MoverComponent, _distanceAhead, radius, maxDirectionAngleRange);

            stateMachine.CharacterStateMachine.SetTargetLocation(_wanderPoint);
        }
    }

    private void StayNearSpawnPoint(IAiStateMachine stateMachine)
    {
        _distanceFromSpawnPoint = Vector3.Distance(spawnPoint, stateMachine.MoverComponent.CurrentPosition);

        if(_distanceFromSpawnPoint > maxDistanceFromSpawnPoint)
        {
            _wanderPoint = GetPointWithinACircle(spawnPoint, UnityEngine.Random.Range(0, radius), 360);
            stateMachine.CharacterStateMachine.SetTargetLocation(_wanderPoint);
        }
    }

    private Vector3 PickAPointInFront(IMoverComponent mover, float distanceAhead, float radius, float angle)
    {
        var centerPoint = mover.CurrentPosition + (mover.LastDirectionFacing.normalized * distanceAhead);
        var destination = GetPointWithinACircle(centerPoint, radius, angle);

        return destination;
    }

    private Vector3 GetPointWithinACircle(Vector3 center, float radius, float maxAngle)
    {
        float randomAngle = UnityEngine.Random.Range(-360, maxAngle);

        float x = center.x + Mathf.Cos(randomAngle * Mathf.Deg2Rad) * radius;
        float z = center.z + Mathf.Sin(randomAngle * Mathf.Deg2Rad) * radius;

        return new Vector3(x, center.y, z);
    }
    #endregion

    #region Check For Enemies

    private bool FindEnemy(IAiStateMachine stateMachine)
    {
        //visibility Check
        var objectsInView = stateMachine.FieldOfViewComponent.GameObjectsInView;

        //check surroundings for enemy target or friendlies
        if (stateMachine.EnemyTags.Length > 0 && objectsInView.Count > 0)
        {

            string[] tags = stateMachine.EnemyTags;
            GameObject[] enemies = objectsInView.Where(enemy => tags.Contains(enemy.tag)).ToArray();

            if (enemies.Length <= 0)
                return false;

            var priorityEnemy = FindPriorityEnemy(enemies);

            if (priorityEnemy != stateMachine.GameObject && priorityEnemy != stateMachine.CharacterStateMachine.TargetObject)
            {
                stateMachine.CharacterStateMachine.SetTargetObject(priorityEnemy);
                return true;
            }
        }

        return false;
    }

    private GameObject FindPriorityEnemy(GameObject[] enemies)
    {
        if (enemies == null)
            throw new ArgumentNullException("enemies should not be null.");
        if (enemies.Length <= 0)
            throw new ArgumentOutOfRangeException("enemies should be more than 0.");

        //first enemy
        //closest enemy
        //lowest health enemy
        return enemies[0];
    }
    #endregion
}
