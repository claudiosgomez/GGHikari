using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 20;
    [SerializeField] float pushForce = 1;
    [SerializeField] float damage = 1;
    public float GetForce()
    {
        return pushForce;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void TakeDamage(float playerDamage)
    {
        health -= playerDamage;
        Debug.Log("Damaged!");
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("I See you " + collision.name.ToString());
    }
}
