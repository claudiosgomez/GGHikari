using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float timeBetweenAttack;
    [SerializeField] float StartTimeBetweenAttack = 1f;
    Animator animator;

    [SerializeField] public Transform attackPosition;
    [SerializeField] public LayerMask whatIsEnemy;
    [SerializeField] float attackRange = 5f;
    [SerializeField] float damage = 10f;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetKeyDown("left ctrl"))
            {
            Debug.Log("Left ctrl");
            if (timeBetweenAttack <= 0)
            {
                Debug.Log("Time");
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
        Gizmos.DrawWireSphere(attackPosition.position,attackRange);
    }
}
