using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
}