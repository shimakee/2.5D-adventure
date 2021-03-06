using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComputation : MonoBehaviour
{
    public void Attack(AnimationEvent animationEvent)
    {
        float weight = animationEvent.animatorClipInfo.weight;

        if(weight >= .5f)
            Debug.Log("Attack", this);
    }
}
