using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Coin_Controller : MonoBehaviour
{
    [SerializeField] private Score_Controller score_Controller;

    [SerializeField] private float minPositionX;
    [SerializeField] private float maxPositionX;
    [SerializeField] private float minPositionZ;
    [SerializeField] private float maxPositionZ;


    [SerializeField] private Transform coinParent;

    private void RandomPosition() 
    {
        float x = Random.Range(minPositionX, maxPositionX);
        float z = Random.Range(minPositionZ, maxPositionZ);

        Vector3 spawnPosition = new Vector3(x, 0, z);
        GraphNode node = AstarPath.active.GetNearest(spawnPosition).node;
        if (node.Walkable)
        {
            coinParent.localPosition = (Vector3)node.position;
        }
        else
        {
            RandomPosition();
        }
    }

    private void AddScore() 
    {
        score_Controller.AddScore.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            RandomPosition();
            AddScore();
        }
        if (other.tag.Equals("Enemy"))
        {
            RandomPosition();
        }
    }
}
