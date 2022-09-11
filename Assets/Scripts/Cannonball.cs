using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag(gameObject.tag))
        {
            if (col.TryGetComponent<Boat>(out Boat boat))
            {
                boat.TakeDamage(damage);
                Destroy(gameObject);
            }

            if (col.TryGetComponent<Interactable>(out Interactable interactable))
            {
                interactable.OnUsed();
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Fire(string sourceTag, Vector2 velocity)
    {
        gameObject.tag = sourceTag;
        rigidbody2D.velocity = velocity;
    }
}
