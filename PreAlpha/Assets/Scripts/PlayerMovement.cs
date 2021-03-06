﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement Variables
    [SerializeField] float walkspeed = 2f;
    [SerializeField] float jumpheight = 2f;
    bool canJump = false;
    private float jumpTimeCounter = 0.3f;
    [SerializeField] float jumpTime = 1f;
    private bool isJumping;
    [SerializeField] public Transform Feet;
    public float checkRadius = 5f;
    

    //Player Variables 
    Animator animator;
    SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody2D;
    [SerializeField] float health = 5f;


    //Game Variables
    GameController gameController;
    [SerializeField] public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
    }
    void Update()
    {
        Move();
        Jump();
        Die();
        canJump = Physics2D.OverlapCircle(Feet.position, checkRadius, whatIsGround);
    }

    private  void OnCollisionEnter2D(Collision2D other)
        {
        if (other.transform.tag == "Ground")
        {
            animator.SetBool("Jump", false);
        }
        if (other.transform.tag == "Spike")
        {
            health = 0f;
        }
        else if (other.transform.tag == "Enemy")
        {
            float pushForce = other.gameObject.GetComponent<Enemy>().GetForce();
            float damageReceived = other.gameObject.GetComponent<Enemy>().GetDamage();
            Vector2 myTransform = new Vector2(transform.position.x, transform.position.y);
            Vector2 dir = other.GetContact(0).point - myTransform;
            dir = -dir.normalized;
            rigidbody2D.AddForce(dir*pushForce);
            health -= damageReceived;
        }
        }
    

    private void Move()
    {
        if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            if (!animator.GetBool("Jump")) { animator.SetBool("Run", true); }
            transform.Translate(Vector2.left * walkspeed * Time.deltaTime);
        }
        else if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            if (!animator.GetBool("Jump")) { animator.SetBool("Run", true); }
            transform.Translate(Vector2.right * walkspeed * Time.deltaTime);
        }
        else if (!Input.GetKey("right") && !Input.GetKey("left"))
        {
            animator.SetBool("Run", false);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown("space") && canJump)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            animator.SetBool("Jump", true);
            canJump = false;
            rigidbody2D.velocity = Vector2.up * jumpheight;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rigidbody2D.velocity = Vector2.up * jumpheight;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void Die()
    {
        if(health <= 0f)
        {
            gameController.RestartLevel();
        }
    }
}