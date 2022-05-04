using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter_Controller : BaseCharacter
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject mainCursor;
    private RaycastHit hit;

    private bool canMoveCursor = true;

    void Update()
    {
        CursorManager();
        ReachedDestination();

        if (Input.GetMouseButtonDown(0) && canMoveCursor == true)
        {
            canMoveCursor = false;
            reachedInvoke = true;

            Move(mainCursor.transform);
        }
    }

    private void CursorManager()
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
    }

    protected override void Idle()
    {
        base.Idle();
        
        canMoveCursor = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Cursor"))
        {
            character.target = null;
        }
    }
}
