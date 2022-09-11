using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Interactable
{
    [SerializeField] private int score = 100;

    public override void OnUsed()
    {
        GameManager.AddScore(score);
        Destroy(gameObject);
    }
}
