using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int maxLife;
    private int currentLife;

    private Animator anim;
    public bool isDead = false;

    [Header("Feedback Damage Settings")]
    public float durationHurt;

    private SpriteRenderer spriteRenderer;
    private Color colorHurt = Color.red;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentLife = maxLife;
        originalColor = spriteRenderer.color;
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
        StartCoroutine(HurtFeedback());

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

    IEnumerator HurtFeedback()
    {
        spriteRenderer.color = colorHurt;
        yield return new WaitForSeconds(durationHurt);
        spriteRenderer.color = originalColor;
    }
}
