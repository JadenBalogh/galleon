using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactable
{
    [SerializeField] private int healing = 1;

    public override void OnUsed()
    {
        Player.Instance.AddHealth(healing);
        Destroy(gameObject);
    }
}
