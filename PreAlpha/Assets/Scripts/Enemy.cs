using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy Variables
    new Rigidbody2D rigidbody2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

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
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
        animator.SetTrigger("IsHit");
        health -= playerDamage;

        StopChasingPlayer();
        Vector2 myTransform = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = new Vector2(player.position.x,player.position.y) - myTransform;
        dir = -dir.normalized;
        rigidbody2D.AddForce(dir * 1000f);
        

    }

    private void Update()
    {
        //Distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        //Check if it is within the agro area
        if (distanceToPlayer <= agroRange)
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
        animator.SetBool("Run", false);
        rigidbody2D.velocity = new Vector2(0, 0);
    }

    private void ChasePlayer()
    {
        animator.SetBool("Run", true);
        if (transform.position.x < player.position.x)
        {
            //enemy is to the left side to the player, so move right
            spriteRenderer.flipX = false;
            rigidbody2D.velocity = new Vector2(moveSpeed, rigidbody2D.velocity.y);
        }
        else
        {
            //enemy is to the right side to the player, so move left
            spriteRenderer.flipX = true;
            rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
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
