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
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Animator characterController;

    protected GraphNode currentNode;
    protected GraphNode targetNode;

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
        characterController.SetTrigger("Walk");
    }

    protected virtual void Idle() 
    {
        character.target = null;
        currentState = States.Idle;
        characterController.SetTrigger("Idle");
    }

    protected virtual void CoinCollect() 
    {
        Debug.Log("chamar evento do coin controller");
    }

    /* 
        bool rotate = true;

        if (rotate == true)
        {
            Quaternion rotTarget = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, lookAtSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            Invoke(nameof(ResetRotation), 0.5f); //TODO: Otimizar linha de código
        }*/
}
