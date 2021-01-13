using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy Variables
    new Rigidbody2D rigidbody2D;

    //Player variables
    [SerializeField] public Transform player;

    //Attack variables
    [SerializeField] float health = 20;
    [SerializeField] float damage = 1;
    [SerializeField] float pushForce = 1;

    //Receive damage variables
    [SerializeField] float agroRange = 2f;
    [SerializeField] float moveSpeed = 5f;

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
        Debug.Log(distanceToPlayer.ToString());

        //Check if it is within the agro area
        if (distanceToPlayer <= agroRange)
        {
            //Chase player
            ChasePlayer();
            Debug.Log("Chase");
        }
        else
        {
            //Stop chasing player
            StopChasingPlayer();
            Debug.Log("Stop!");
        }


        //Is dead?
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void StopChasingPlayer()
    {
        rigidbody2D.velocity = new Vector2(0, 0);
    }

    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            //enemy is to the left side to the player, so move right
            rigidbody2D.velocity = new Vector2(moveSpeed, rigidbody2D.velocity.y);
            Debug.Log("To the right!");
        }
        else
        {
            //enemy is to the right side to the player, so move left
            rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
            Debug.Log("To the left!");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Spike")
        {
            health = 0f;
        }
    }
}
