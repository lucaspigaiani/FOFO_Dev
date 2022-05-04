using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter_Controller : BaseCharacter
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject mainCursor;
    private RaycastHit hit;
    private Ray ray;

    private bool canMoveCursor = true;
    private bool reachedInvoke = true;

    void Start()
    {
        
    }

    void Update()
    {

        if (canMoveCursor)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 tempPosition = hit.point;

                targetNode = AstarPath.active.GetNearest(tempPosition).node;
                if (targetNode.Walkable == false)
                {
                    mainCursor.SetActive(false);
                }
                if (targetNode.Walkable)
                {
                    mainCursor.transform.position = (Vector3)targetNode.position;
                    mainCursor.SetActive(true);
                }
            }
        }

        if (aiPathCharacter.reachedDestination && reachedInvoke == true)
        {
            reachedInvoke = false;
         //   Debug.Log("chegou");
            Invoke(nameof(Idle), 0.5f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            canMoveCursor = false;
            Move(mainCursor.transform);
        }
        if (Input.GetMouseButtonUp(0))
        {
        }

    }

    protected override void Idle()
    {
        base.Idle();
        
        canMoveCursor = true;
        reachedInvoke = true;
    }

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Coin"))
        {
            CoinCollect();
        }
        if (other.tag.Equals("Cursor"))
        {
            Debug.Log("cursor");
            character.target = null;
            aiPathCharacter.
        }
    }
}
