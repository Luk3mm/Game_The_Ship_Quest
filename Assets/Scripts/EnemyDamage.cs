using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int maxLife;
    private int currentLife;

    private Animator anim;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (isDead) 
        {
            return;
        }

        currentLife -= damage;

        if(currentLife <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        anim.SetTrigger("death");

        Destroy(gameObject, 1.2f);
    }
}
