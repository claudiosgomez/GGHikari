using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement Variables
    [SerializeField] float walkspeed = 2f;
    [SerializeField] float jumpheight = 2f;
    bool canJump = false;
    private float jumpTimeCounter;
    [SerializeField] float jumpTime;
    private bool isJumping;
    [SerializeField] Transform Feet;
    public float checkRadius;
    

    //Player Variables
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;
    [SerializeField] float health = 5f;


    //Game Variables
    GameController gameController;
    [SerializeField] LayerMask whatIsGround;

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
        Attack();
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

    private void Attack()
    {
        /*if (Input.GetKeyDown("left ctrl"))
        {
            animator.Play("Attack");
            Debug.Log("Attack!");
        }*/
    }

    private void Die()
    {
        if(health <= 0f)
        {
            gameController.RestartLevel();
        }
    }
}