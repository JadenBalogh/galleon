using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boat : MonoBehaviour
{
    [SerializeField] protected Cannonball cannonballPrefab;
    [SerializeField] protected float cannonballSpeed = 5f;
    [SerializeField] protected Transform topCannon;
    [SerializeField] protected Transform bottomCannon;
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

    protected void FireCannon(int direction)
    {
        Vector2 cannonPos = direction > 0 ? topCannon.position : bottomCannon.position;
        Vector2 cannonVel = Vector2.up * direction * cannonballSpeed;

        Cannonball cannonball = Instantiate(cannonballPrefab, cannonPos, Quaternion.identity);
        cannonball.Fire(gameObject.tag, cannonVel);
    }

    public void Sink()
    {
        Destroy(gameObject);
    }

    protected abstract void UpdatePosition();
}
