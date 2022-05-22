using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Boat
{
    [SerializeField] private float minIdleTime = 2f;
    [SerializeField] private float maxIdleTime = 4f;

    private bool canMove = true;

    protected override void UpdatePosition()
    {
        if (canMove)
        {
            int prevIndex = moveIndex;
            while (prevIndex == moveIndex)
            {
                moveIndex = Random.Range(0, movePositions.Length);
            }
            targetPos = movePositions[moveIndex].position;
            StartCoroutine(IdleTimer());
        }
    }

    private IEnumerator IdleTimer()
    {
        canMove = false;
        float idleTime = moveTime + Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        canMove = true;
    }
}
