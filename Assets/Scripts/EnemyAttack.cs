using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage;
    public float attackInterval;

    private float lastAttackTime;
    private Enemy enemy;

    [HideInInspector]
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(enemy != null && enemy.CurrentState() == "follow")
            {
                if(Time.time >= lastAttackTime + attackInterval)
                {
                    PlayerHealth playerLife = collision.GetComponent<PlayerHealth>();

                    if(playerLife != null)
                    {
                        playerLife.TakeDamage(damage);
                        lastAttackTime = Time.time;
                    }
                }
            }
        }
    }
}
