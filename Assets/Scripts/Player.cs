using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Boat
{
    private const float VIEWPORT_FIRE_VERTICAL_OFFSET = 0.2f;

    private void Start()
    {
        ChannelIndex = -1;
    }

    protected override void UpdatePosition()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 viewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (Mathf.Abs(viewportPos.y - 0.5f) > VIEWPORT_FIRE_VERTICAL_OFFSET)
            {
                FireCannon(viewportPos.y > 0.5f ? 1 : -1);
            }
            else
            {
                int moveDir = viewportPos.x > 0.5f ? 1 : -1;
                moveIndex = Mathf.Clamp(moveIndex + moveDir, 0, movePositions.Length - 1);
                targetPos = movePositions[moveIndex].position;
            }
        }
    }
}
