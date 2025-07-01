using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform shootUp;
    public Transform shootDown;
    public Transform shootLeft;
    public Transform shootRight;
    public float cooldownTime;
    private float lastShootTime = -Mathf.Infinity;

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
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShootTime + cooldownTime)
        {
            lastShootTime = Time.time;

            anim.SetTrigger("attack");

            Vector2 dir = lastDirection;
            Transform shootPoint = shootDown;

            if(dir == Vector2.up)
            {
                shootPoint = shootUp;
            }
            else if(dir == Vector2.down)
            {
                shootPoint = shootDown;
            }
            else if(dir == Vector2.left)
            {
                shootPoint = shootLeft;
            }
            else if(dir == Vector2.right)
            {
                shootPoint = shootRight;
            }

            GameObject newProjectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Projectile proj = newProjectile.GetComponent<Projectile>();
            proj.Initialize(dir);
        }
    }
}
