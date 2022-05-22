using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Boat
{
    protected override void UpdatePosition()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            int moveDir = Camera.main.ScreenToViewportPoint(Input.mousePosition).x > 0.5f ? 1 : -1;
            moveIndex = Mathf.Clamp(moveIndex + moveDir, 0, movePositions.Length - 1);
            targetPos = movePositions[moveIndex].position;
        }
    }
}
