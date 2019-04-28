using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            //Debug.Log("hit enemy");
            Enemy enemy = collision.GetComponentInParent<Enemy>();
            enemy.TakeDamage(Character.Instance.AttackDamage);
        }
    }
}
