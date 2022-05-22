using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform[] movePositions;
    [SerializeField] private float moveTime = 0.5f;

    private int moveIndex = 1;
    private Vector2 targetPos = Vector2.zero;
    private Vector2 currVel;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            int moveDir = Camera.main.ScreenToViewportPoint(Input.mousePosition).x > 0.5f ? 1 : -1;
            moveIndex = Mathf.Clamp(moveIndex + moveDir, 0, movePositions.Length - 1);
            targetPos = movePositions[moveIndex].position;
        }

        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref currVel, moveTime);
    }
}
