using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private bool isWalk = false;

    private Rigidbody2D rig;
    private Animator anim;

    private Vector2 lastDirection;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        lastDirection = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(x, y).normalized;

        rig.velocity = moveDirection * speed;

        isWalk = moveDirection != Vector2.zero;

        if (isWalk)
        {
            lastDirection = moveDirection;
        }

        anim.SetFloat("axisX", lastDirection.x);
        anim.SetFloat("axisY", lastDirection.y);

        anim.SetBool("walk", isWalk);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
    }
}
