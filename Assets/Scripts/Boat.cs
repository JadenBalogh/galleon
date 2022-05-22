using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boat : MonoBehaviour
{
    [SerializeField] protected Transform[] movePositions;
    [SerializeField] protected float moveTime = 0.5f;

    protected int moveIndex = 1;
    protected Vector2 targetPos = Vector2.zero;
    private Vector2 currVel;

    protected virtual void Update()
    {
        UpdatePosition();
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref currVel, moveTime);
    }

    protected abstract void UpdatePosition();
}
