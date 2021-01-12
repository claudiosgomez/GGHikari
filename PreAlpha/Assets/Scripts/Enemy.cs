using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy Variables
    Rigidbody2D rigidbody2D;

    //Player variables
    [SerializeField] Transform player;
    
    //Attack variables
    [SerializeField] float health = 20;
    [SerializeField] float damage = 1;
    [SerializeField] float pushForce = 1;

    //Receive damage variables
    [SerializeField] float agroRange;
    [SerializeField] float moveSpeed;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

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
        //Distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        //Check if it is within the agro area
        if(distanceToPlayer < agroRange)
        {
            //Chase player
            ChasePlayer();
        }
        else
        {
            //Stop chasing player
            StopChasingPlayer();
        }


        //Is dead?
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void StopChasingPlayer()
    {
       
    }

    private void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            //enemy is to the left side to the player, so move right
            rigidbody2D.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            //enemy is to the right side to the player, so move left
            rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
        }
    }
}
