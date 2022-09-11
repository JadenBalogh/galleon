using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Interactable
{
    [SerializeField] private int damage = 1;

    public override void OnUsed()
    {
        Player.Instance.TakeDamage(damage);
        Destroy(gameObject);
    }
}
