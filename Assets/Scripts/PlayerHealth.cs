using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxLife;
    public int currentlife;
    public Image lifeBar;

    private Animator anim;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentlife = maxLife;
        anim = GetComponent<Animator>();
        UpdateLifeBar();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        currentlife -= damage;
        UpdateLifeBar();

        if(currentlife <= 0)
        {
            Death();
        }
    }

    private void UpdateLifeBar()
    {
        if(lifeBar != null)
        {
            lifeBar.fillAmount = (float)currentlife / maxLife;
        }
    }

    private void Death()
    {
        isDead = true;
        anim.SetTrigger("death");
    }
}
