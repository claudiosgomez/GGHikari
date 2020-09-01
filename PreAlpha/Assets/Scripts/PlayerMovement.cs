using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement Variables
    [SerializeField] float walkspeed = 2f;
    [SerializeField] float jumpheight = 2f;
    bool canJump;

    //Player Variables
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;
    [SerializeField] float health = 5f;

    //Game Variables
    GameController gameController;

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
    }

    private void Move()
    {
        if (!Input.GetKey("right") && !Input.GetKey("left") || (Input.GetKey("right") && Input.GetKey("left")))
        {
            animator.SetBool("Run", false);
        }
        else if (Input.GetKey("left"))
        {
            spriteRenderer.flipX = true;
            if (!animator.GetBool("Jump")) { animator.SetBool("Run", true); }
            transform.Translate(Vector2.left * walkspeed * Time.deltaTime);
        }
        else if (Input.GetKey("right"))
        {
            spriteRenderer.flipX = false;
            if (!animator.GetBool("Jump")) { animator.SetBool("Run", true); }
            transform.Translate(Vector2.right * walkspeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown("space") && canJump)
        {
            animator.SetBool("Jump", true);
            canJump = false;
            rigidbody2D.velocity = Vector2.up * jumpheight;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Ground")
        {
            animator.SetBool("Jump", false);
            canJump = true;
        }
        if (other.transform.tag == "Spike")
        {
            health = 0f;
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
