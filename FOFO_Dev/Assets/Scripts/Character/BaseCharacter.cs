using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseCharacter : MonoBehaviour
{
    protected enum States { Idle, Walk }
    [SerializeField] protected States currentState;

    [SerializeField] protected AIDestinationSetter character;
    [SerializeField] protected AIPath aiPathCharacter;
    [SerializeField] protected Animator characterController;

    protected GraphNode currentNode;
    protected GraphNode targetNode;

    [SerializeField] protected bool reachedInvoke = true;

    private void Start()
    {
        StartCurrentNode();
    }

    protected void StartCurrentNode() 
    {
        currentNode = AstarPath.active.GetNearest(this.transform.position).node;
    }

    protected virtual void Move(Transform targetTransform) 
    {
        character.target = targetTransform;
        currentState = States.Walk;
        characterController.SetBool("ChangeAction", true);
    }

    protected virtual void Idle() 
    {
        character.target = null;
        currentState = States.Idle;
        characterController.SetBool("ChangeAction", false);
    }

    protected virtual void ReachedDestination()
    {
        if (aiPathCharacter.reachedDestination && reachedInvoke == true)
        {
            reachedInvoke = false;
            Idle();
        }
    }
}
