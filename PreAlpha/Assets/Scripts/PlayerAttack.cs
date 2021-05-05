using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float timeBetweenAttack;
    [SerializeField] float StartTimeBetweenAttack = 1f;
    Animator animator;
    SpriteRenderer playerSprite;
    [SerializeField] public Transform attackPosition;
    [SerializeField] public LayerMask whatIsEnemy;
    [SerializeField] float attackRange = 5f;
    [SerializeField] float damage = 10f;
    [SerializeField] GameObject SlashAttack;
    [SerializeField] GameObject mySlashAttack;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("left ctrl"))
        {

            if (timeBetweenAttack <= 0)
            {

                animator.Play("Attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemy);
             

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                timeBetweenAttack = StartTimeBetweenAttack;
            }


        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    public void SlashAnimation()
    {
        if (playerSprite.flipX)
        {
            mySlashAttack = (GameObject)Instantiate(SlashAttack, new Vector3(attackPosition.transform.position.x - 1.206f, attackPosition.transform.position.y, attackPosition.transform.position.z), Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        else
        {
            mySlashAttack = (GameObject)Instantiate(SlashAttack, new Vector3(attackPosition.transform.position.x, attackPosition.transform.position.y, attackPosition.transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        
        Destroy(mySlashAttack, 0.1f);
    }
}
