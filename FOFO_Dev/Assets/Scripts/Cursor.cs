using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;
using Pathfinding;
using TMPro;

public class Cursor : MonoBehaviour
{
	public enum States 
	{
		exploration,
		combat
	}

	public Camera mainCamera;

	public States currentState;

	public GameObject[] Covers;
	public GameObject parentCursor;
	public GameObject mainCursor;
	public GameObject prefabCursor;

	[SerializeField] private List<GameObject> cursorPosition = new List<GameObject>();

	public List<AIDestinationSetter> player;
	public int currentPlayer;

	private List<GraphNode> reachableNodes;
	public GraphNode node;

	[SerializeField] private bool inputDelay = true;
	RaycastHit hit;
	Ray ray;

	[SerializeField] Transform temp;

	public TurnManager turnManager;
	public CameraZoomAndRotation cameraZoomAndRotation;
	public GridCombatSystem gridCombatSystem;

	private bool canSetRotation = true;
	public bool cursorIsOnScreen = true;

	private bool characterSetRotation;

	private float lookAtSpeed;

	[SerializeField]
	private TMP_Text costText;
	private void Start()
	{


	}


	private void Update()
    {
        /*	ray = mainCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				Transform objectHit = hit.transform;

				// Do something with the object that was hit by the raycast.

				Vector3 cursorposition = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
				node = AstarPath.active.GetNearest(Input.mousePosition).node;
				mainCursor.transform.position = cursorposition;
				Debug.Log("node: " + cursorposition);



			}*/


        if (Input.GetKeyDown("joystick button 2"))
        {
			Vector3 pos = player[currentPlayer].transform.position;
			Vector3 dir = (player[currentPlayer + 1].transform.position - player[currentPlayer].transform.position).normalized;
			Debug.DrawLine(pos, pos + dir * 10, Color.red, Mathf.Infinity);

			Vector3 temp = (Vector3)AstarPath.active.GetNearest(player[currentPlayer + 1].transform.position).node.position - dir;

			GraphNode tempNode = AstarPath.active.GetNearest(temp).node;
			

			if (tempNode.Tag == 1)
            {
				Debug.Log("Tag 1");
				GameObject clone;
				clone = Instantiate(prefabCursor, (Vector3)tempNode.position, transform.rotation);
			}
		}
	
		switch (currentState)
			{
				case States.exploration:

				/*RaycastHit hit;
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					Vector3 tempPosition = hit.point;

					node = AstarPath.active.GetNearest(tempPosition).node;
                    if (node.Walkable == false)
                    {
						mainCursor.SetActive(false);
					}
					if (node.Walkable)
					{
						mainCursor.transform.position = (Vector3)node.position;
						mainCursor.SetActive(true);
					}
					
				
				}*/


				if (node == null)
				{
					node = AstarPath.active.GetNearest(player[currentPlayer].transform.position).node;
				}

				int x = (int)Input.GetAxis("Horizontal");
				int z = (int)Input.GetAxis("Vertical");
                switch (cameraZoomAndRotation.currentDirection)
                {
                    case CameraZoomAndRotation.CameraRotation.North:
                        break;
                    case CameraZoomAndRotation.CameraRotation.East:
						int tempX = -x;
						int tempZ = z;
						z = tempX;
						x = tempZ;
                        break;
                    case CameraZoomAndRotation.CameraRotation.South:
						tempX = -x;
						tempZ = -z;
						x = tempX;
						z = tempZ;
						break;
                    case CameraZoomAndRotation.CameraRotation.West:
						
						tempX = x;
						tempZ = -z;
						z = tempX;
						x = tempZ;
						break;
                    default:
                        break;
                }

				if (x == 1 || x == -1 || z == 1 || z == -1)
				{
					if (node.Walkable && inputDelay == true)
					{
						CursorDirection(node, x, z, 0.1f);
						inputDelay = false;
                        if (characterSetRotation == true)
                        {
							characterSetRotation = false;
						}
						if (gridCombatSystem.rangedAttackStarted == true)
						{
							int cost = (node.position - AstarPath.active.GetNearest(gridCombatSystem.currentPlayer.transform.position).node.position).costMagnitude;
							cost = cost / 1000;
                            if (cost <= gridCombatSystem.rangedAttack.rangedSkillDistance)
                            {
								gridCombatSystem.movedCursor = true;
							}
						}

						int currentAP = player[currentPlayer].GetComponent<CharController>().currentAP;
						int movimentCost = GetMovimentCost();
						UpdateAPCostText(movimentCost, currentAP);
					}
				}

				if (Input.GetButtonDown("Cancel"))
				{
					node = AstarPath.active.GetNearest(player[currentPlayer].transform.position).node;
					CursorDirection(node, 0, 0, 0.5f);
					inputDelay = false;
					GameObject myEventSystem = GameObject.Find("EventSystem");
					myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
					cursorIsOnScreen = true;
					characterSetRotation = false;
				}

				if (Input.GetButtonDown("Submit"))
				{
                    if (characterSetRotation == false)
                    {
						SetTowardsRotation(player[0].GetComponent<CharController>(), mainCursor);
						
                    }
                    if (characterSetRotation == true)
                    {
						if (node.Walkable)
						{
							bool canWalk = MovimentCost();
							//ResetMatrix();
							//ShowMatrix(node);
							if (canWalk == true)
							{
								//if (hit.transform.gameObject.tag == "Cursor")
								{
									Transform temp = hit.transform;
									player[currentPlayer].target = temp;

								}

								player[currentPlayer].target = mainCursor.transform;
								player[currentPlayer].SendMessage("SetCharacterState", false);
							}

						}
					}

					characterSetRotation = !characterSetRotation;
				}
				if (Input.GetButtonUp("Submit"))
				{
					player[currentPlayer].target = null;
				}

				

			/*	if (Input.GetButtonDown("Fire1"))
				{
					Debug.Log("Node Tag: " + node.Tag);
                }*/

				break;

				case States.combat:



					break;

				default:
					break;
			}


		
		if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Left Bumper"))
        {
			/*Debug.Log("realocar isso");
		
			*///SetCurrentPlayer(-1);
		}

		if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Right Bumper"))
		{
			//SetCurrentPlayer(1);
			//Debug.Log("realocar isso");
		}

        if (player[0].name != turnManager.currentTarget.name)
        {
			player[0] = turnManager.currentTarget.GetComponent<AIDestinationSetter>();
		}

	}

	public void UpdateAPCostText(int movimentCost, int currentAP)
    {
		if (movimentCost == 0)
		{
			costText.text = "";
		}
		else if (currentAP >= movimentCost)
		{
			costText.text = movimentCost.ToString(); 
		}
		else
		{
			costText.text = "<color=#ff0000>" + movimentCost.ToString() + "</color>";
		}
	}
	public void SetCurrentPlayer(int changePlayer) 
	{
		currentPlayer += changePlayer;

        if ( currentPlayer > player.Count - 1)
        {
			currentPlayer = 0;
        }
		if (currentPlayer < 0)
		{
			currentPlayer = player.Count - 1;
		}
	}


	public void ResetCursor(GameObject selectedPlayer) 
	{
		node = AstarPath.active.GetNearest(selectedPlayer.transform.position).node;
		CursorDirection(node, 0, 0, 0.5f);
		inputDelay = false;
		GameObject myEventSystem = GameObject.Find("EventSystem");
		myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
	}

	private bool MovimentCost()
	{
		int cost = (node.position - AstarPath.active.GetNearest(player[currentPlayer].transform.position).node.position).costMagnitude;
		cost = cost / 1000;
		bool haveApToWalk = gridCombatSystem.CombatMoviment(cost);
		return haveApToWalk;
	}

	private int GetMovimentCost()
	{
		int cost = (node.position - AstarPath.active.GetNearest(player[currentPlayer].transform.position).node.position).costMagnitude;
		cost = cost / 1000;
		return cost;
	}

	private void ResetMatrix()
	{

		if (cursorPosition.Count > 0)
		{

			for (int i = cursorPosition.Count - 1; i >= 0; i--)
			{
				Destroy(cursorPosition[i]);
				cursorPosition.Remove(cursorPosition[i]);
			}
		}
	}

	void ShowMatrix(GraphNode selectedNode)
	{
		selectedNode.GetConnections(otherNode => {
			GameObject clone;
			clone = Instantiate(prefabCursor, (Vector3)otherNode.position, transform.rotation, parentCursor.transform);
			cursorPosition.Add(clone);
		});
	}

	public void WalkButton()
	{
		if (reachableNodes != null)
		{
			ResetWalkMatrix();
		}
		node = AstarPath.active.GetNearest(player[currentPlayer].transform.position).node;
		reachableNodes = PathUtilities.GetReachableNodes(node);

		for (int i = 0; i < reachableNodes.Count; i++)
		{
			GameObject clone;
			clone = Instantiate(prefabCursor, (Vector3)reachableNodes[i].position, transform.rotation, parentCursor.transform);
			cursorPosition.Add(clone);
		}

	}
	private void CursorDirection(GraphNode currentNode, int offsetX, int offsetZ, float delayTime)
	{
		GraphNode nextNode = currentNode;
		Vector3 tempCursorVectorPosition = new Vector3(offsetX, 0.5f, offsetZ);
		Vector3 tempVector = (Vector3)nextNode.position + tempCursorVectorPosition;

		nextNode = AstarPath.active.GetNearest(tempVector).node;
        if (nextNode.Walkable)
        {
			mainCursor.transform.position = (Vector3)nextNode.position;
			mainCursor.SetActive(true);
			CursorIsOnScreen();
			node = nextNode;
		}
		
        if (nextNode.Walkable == false)
        {
			//mainCursor.SetActive(false);
			node = currentNode;
		}

		Invoke("ResetInputDelay", delayTime);
	}

	private void ResetInputDelay() 
	{
		inputDelay = true;
	}

	private void ResetWalkMatrix()
	{
		for (int i = reachableNodes.Count; i >= 0; i--)
		{
			Destroy(cursorPosition[i]);
			cursorPosition.Remove(cursorPosition[i]);
		}
	}

	public void SetTowardsRotation(CharController attacker, GameObject target)
	{
        if (canSetRotation == true)
        {
			attacker.target = target;
			attacker.rotate = true;
			
			Invoke(nameof(ResetRotation), 0.1f);
			canSetRotation = false;
		}
	}

    private void ResetRotation()
    {
		CancelInvoke(nameof(ResetRotation));
		canSetRotation = true;
	}

	private void CursorIsOnScreen() 
	{
		int cost = (node.position - AstarPath.active.GetNearest(player[currentPlayer].transform.position).node.position).costMagnitude;
		cost = cost / 1000;
        if (cost <= 9)
        {
			cursorIsOnScreen = true;
		}
		else
        {
			cursorIsOnScreen = false;
		}
	}
}
