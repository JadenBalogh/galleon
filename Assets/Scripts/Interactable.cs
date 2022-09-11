using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private float minMoveSpeed = 1f;
    [SerializeField] private float maxMoveSpeed = 3f;

    private float moveSpeed;

    private void Awake()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    private void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    public abstract void OnUsed();
}
