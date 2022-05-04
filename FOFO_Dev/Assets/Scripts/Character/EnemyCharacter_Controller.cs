using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter_Controller : BaseCharacter
{


    [SerializeField] private float followDelay;
    [SerializeField] private Coin_Controller coin;

    void Start()
    {
        StartCoroutine(nameof(Followcoin));
    }

    void Update()
    {
        ReachedDestination();
    }

    IEnumerator Followcoin() 
    {
        targetNode = AstarPath.active.GetNearest(coin.transform.position).node;
        Move(coin.transform);
        yield return new WaitForSeconds(followDelay);
        StartCoroutine(nameof(Followcoin));
        reachedInvoke = true;
    }
}
