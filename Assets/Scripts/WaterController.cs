using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    [SerializeField] private Transform[] tilemaps;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float snapThreshold = 10f;
    [SerializeField] private float startPosX = 10f;

    private void Update()
    {
        foreach (Transform tilemap in tilemaps)
        {
            tilemap.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (tilemap.position.x >= snapThreshold)
            {
                tilemap.position = Vector3.right * startPosX;
            }
        }
    }
}
